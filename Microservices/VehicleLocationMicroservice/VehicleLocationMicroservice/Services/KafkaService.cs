using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using VehicleLocationMicroservice.Contracts;
using Confluent.Kafka;
using VehicleLocationMicroservice.Entities;
using System.Text.Json;

namespace VehicleLocationMicroservice.Services
{
    public class KafkaService : BackgroundService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICassandraService _cassandraService;

        public KafkaService(IUnitOfWork unitOfWork, ICassandraService cassandraService)
        {
            this._unitOfWork = unitOfWork;
            this._cassandraService = cassandraService;

            _unitOfWork.KafkaConsumer.Subscribe(_unitOfWork.KafkaTopic);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.Register(() => CloseConsumer());

            while (!stoppingToken.IsCancellationRequested)
            {
				Console.WriteLine("Try get data");
                ConsumeResult<string, string> data = _unitOfWork.KafkaConsumer.Consume(TimeSpan.FromSeconds(1));
                if (data is not null)
                {
					Console.WriteLine("Getting data " + data.Message.Value);
                    VehicleData newData = JsonSerializer.Deserialize<VehicleData>(data.Message.Value);
                    Console.WriteLine("Deserialized");
                    Console.WriteLine(newData.ToString());
                    AddData(newData);
					Console.WriteLine("Inserted into database");
                    _unitOfWork.KafkaConsumer.StoreOffset(data);
                }

                await Task.Delay(TimeSpan.FromSeconds(2), stoppingToken);
            }

            CloseConsumer();
        }

        private void AddData(VehicleData newData)
        {
            Console.WriteLine("Entered 'AddData'");
            _cassandraService.AddData(newData);
        }

        private void CloseConsumer()
        {
            _unitOfWork.KafkaConsumer.Close();
        }
    }
}
