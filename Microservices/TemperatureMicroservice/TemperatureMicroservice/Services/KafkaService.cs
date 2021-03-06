using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TemperatureMicroservice.Contracts;
using TemperatureMicroservice.Entities;
using Confluent.Kafka;
using System.Text.Json;

namespace TemperatureMicroservice.Services
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

            while(!stoppingToken.IsCancellationRequested)
            {
				Console.WriteLine("Try get data");
                ConsumeResult<Null, string> data = _unitOfWork.KafkaConsumer.Consume(TimeSpan.FromSeconds(1));
                if(data is not null)
                {
					Console.WriteLine("Get data");
                    RoadAndAirTempData newData = (RoadAndAirTempData)JsonSerializer.Deserialize(data.Message.Value, typeof(RoadAndAirTempData));
                    AddData(newData);					
					Console.WriteLine("Added data to database");
                    _unitOfWork.KafkaConsumer.StoreOffset(data);
                }

                await Task.Delay(TimeSpan.FromSeconds(2), stoppingToken);
            }

            CloseConsumer();
        }

        private void AddData(RoadAndAirTempData newData)
        {
            _unitOfWork.CassandraSession.Execute(_cassandraService.InsertRoadAndAirTempDataQuery(_unitOfWork.TableAllData, newData));

            bool found = false;

            var DataNewest = _unitOfWork.CassandraSession.Execute(_cassandraService.SelectAllQuery(_unitOfWork.TableNewestData));
            foreach (var instance in DataNewest)
            {
                RoadAndAirTempData roadData = _cassandraService.ConvertCassandraTempRow(instance);
                if (_geolocationService.CalculateDistance(newData.Latitude, newData.Longitude, roadData.Latitude, roadData.Longitude) <= _geolocationService.MaxDistance)
                {
                    found = true;
                    if (newData.Timestamp > roadData.Timestamp)
                        _unitOfWork.CassandraSession.Execute(_cassandraService.UpdateNewestQuery(_unitOfWork.TableNewestData, newData, roadData.Latitude, roadData.Longitude));
                }
            }
            if (!found)
                _unitOfWork.CassandraSession.Execute(_cassandraService.InsertRoadAndAirTempDataQuery(_unitOfWork.TableNewestData, newData));

            newData.Timestamp = new DateTime(newData.Timestamp.Year, newData.Timestamp.Month, newData.Timestamp.Day).AddHours(newData.Timestamp.Hour);
            UpdateAverage(_unitOfWork.TableAverageH, newData);

            newData.Timestamp = new DateTime(newData.Timestamp.Year, newData.Timestamp.Month, newData.Timestamp.Day);
            UpdateAverage(_unitOfWork.TableAverageDay, newData);
        }

        private void UpdateAverage(string table, RoadAndAirTempData newData)
        {
            bool found = false;
            var data = _unitOfWork.CassandraSession.Execute(_cassandraService.SelectAllQuery(table));
            foreach (var instance in data)
            {
                AverageTempData averageData = _cassandraService.ConvertCassandraAverageRow(instance);
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
                _unitOfWork.CassandraSession.Execute(_cassandraService.InsertAverageTempDataQuery(table, newData, _geolocationService.MaxDistance));
        }

        private void CloseConsumer()
        {
            _unitOfWork.KafkaConsumer.Close();
        }
    }
}
