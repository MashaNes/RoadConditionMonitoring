using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AirQualityMicroservice.Contracts;
using Confluent.Kafka;
using AirQualityMicroservice.Entities;
using System.Text.Json;

namespace AirQualityMicroservice.Services
{
    public class KafkaService : BackgroundService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICassandraService _cassandraService;
        private readonly IGeolocationService _geolocationService;

        public KafkaService(IUnitOfWork unitOfWork, ICassandraService cassandraService, IGeolocationService geolocationService)
        {
            this._unitOfWork = unitOfWork;
            this._cassandraService = cassandraService;
            this._geolocationService = geolocationService;

            _unitOfWork.KafkaConsumer.Subscribe(_unitOfWork.KafkaTopic);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.Register(() => CloseConsumer());

            await Task.Delay(7000, stoppingToken);

            while (!stoppingToken.IsCancellationRequested)
            {
                ConsumeResult<Null, string> data = _unitOfWork.KafkaConsumer.Consume(TimeSpan.FromSeconds(1));
                if(data is not null)
                {
                    AirQualityData newData = (AirQualityData)JsonSerializer.Deserialize(data.Message.Value, typeof(AirQualityData));
                    AddData(newData);
                    _unitOfWork.KafkaConsumer.StoreOffset(data);
                }

                await Task.Delay(TimeSpan.FromSeconds(2), stoppingToken);
            }

            CloseConsumer();
        }

        private void AddData(AirQualityData newData)
        {
            _unitOfWork.CassandraSession.Execute(_cassandraService.InsertAirQualityDataQuery(_unitOfWork.TableAllData, newData));

            bool found = false;

            var DataNewest = _unitOfWork.CassandraSession.Execute(_cassandraService.SelectAllQuery(_unitOfWork.TableNewestData));
            foreach (var instance in DataNewest)
            {
                AirQualityData airQualityData = _cassandraService.ConvertCassandraAirQualityRow(instance);
                if (_geolocationService.CalculateDistance(newData.Latitude, newData.Longitude, airQualityData.Latitude, airQualityData.Longitude) <= _geolocationService.MaxDistance)
                {
                    found = true;
                    if (newData.Timestamp > airQualityData.Timestamp)
                        _unitOfWork.CassandraSession.Execute(_cassandraService.UpdateNewestQuery(_unitOfWork.TableNewestData, newData, airQualityData.Latitude, airQualityData.Longitude));
                }
            }
            if (!found)
                _unitOfWork.CassandraSession.Execute(_cassandraService.InsertAirQualityDataQuery(_unitOfWork.TableNewestData, newData));

            newData.Timestamp = new DateTime(newData.Timestamp.Year, newData.Timestamp.Month, newData.Timestamp.Day).AddHours(newData.Timestamp.Hour);
            UpdateAverage(_unitOfWork.TableAverageH, newData);

            newData.Timestamp = new DateTime(newData.Timestamp.Year, newData.Timestamp.Month, newData.Timestamp.Day);
            UpdateAverage(_unitOfWork.TableAverageDay, newData);
        }

        private void UpdateAverage(string table, AirQualityData newData)
        {
            bool found = false;
            var data = _unitOfWork.CassandraSession.Execute(_cassandraService.SelectAllQuery(table));
            foreach (var instance in data)
            {
                AverageAirQualityData averageData = _cassandraService.ConvertCassandraAverageRow(instance);
                if (_geolocationService.CalculateDistance(newData.Latitude, newData.Longitude, averageData.Latitude, averageData.Longitude) <= averageData.Radius)
                {
                    if (DateTime.Compare(averageData.Timestamp, newData.Timestamp) == 0)
                    {
                        found = true;
                        _unitOfWork.CassandraSession.Execute(_cassandraService.UpdateAverageQuery(table, newData, averageData));
                    }
                }
            }
            if (!found)
                _unitOfWork.CassandraSession.Execute(_cassandraService.InsertAverageAirQualityDataQuery(table, newData, _geolocationService.MaxDistance));
        }

        private void CloseConsumer()
        {
            _unitOfWork.KafkaConsumer.Close();
        }
    }
}
