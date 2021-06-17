using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cassandra;
using VehicleLocationMicroservice.Entities;
using VehicleLocationMicroservice.Contracts;

namespace VehicleLocationMicroservice.Services
{
    public class CassandraService :  ICassandraService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CassandraService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public void AddData(VehicleData data)
        {
            Console.WriteLine("Entered 'AddData' cassandra");
            RowSet rows = _unitOfWork.CassandraSession.Execute(SelectWhereQuery(_unitOfWork.TableData, "VehicleId", data.VehicleId));
            bool empty = true;
            foreach(Row row in rows)
            {
                empty = false;
                Console.WriteLine("Has elements");
                Console.Out.Flush();
                VehicleData oldData = ConvertCassandraRow(row);
                Console.WriteLine(oldData.ToString());
                Console.Out.Flush();
                if (data.Timestamp > oldData.Timestamp)
                    _unitOfWork.CassandraSession.Execute(UpdateQuery(_unitOfWork.TableData, data));
            }
            if (empty)
            {
                Console.WriteLine("First occurance");
                Console.Out.Flush();
                _unitOfWork.CassandraSession.Execute(InsertIntoQuery(_unitOfWork.TableData, data));
            }
        }

        public VehicleData GetForId(int id)
        {
            return ConvertCassandraRow(_unitOfWork.CassandraSession.Execute(SelectWhereQuery(_unitOfWork.TableData, "VehicleId", id)).FirstOrDefault());
        }

        public List<VehicleData> GetAll()
        {
            return _unitOfWork.CassandraSession.Execute(SelectAllQuery(_unitOfWork.TableData)).Select(x => ConvertCassandraRow(x)).ToList();
        }

        private VehicleData ConvertCassandraRow(Row instance)
        {
            VehicleData data = new VehicleData();
            Console.WriteLine(instance.ToString());
            Console.Out.Flush();
            data.Timestamp = DateTime.Parse(instance["Timestamp"].ToString());
            Console.WriteLine(data.Timestamp.ToString());
            data.Latitude = (double)instance["Latitude"];
            Console.WriteLine(data.Latitude);
            data.Longitude = (double)instance["Longitude"];
            Console.WriteLine(data.Longitude);
            Console.Out.Flush();
            data.VehicleId = (int)instance["VehicleId"];
            Console.WriteLine(data.VehicleId);
            Console.Out.Flush();
            return data;
        }

        private string InsertIntoQuery(string table, VehicleData data)
        {
            string command = "insert into " + table + " (\"Timestamp\", \"VehicleId\", \"Latitude\", \"Longitude\") " +
                             "values ('" + ConvertDateToString(data.Timestamp) + "', " + data.VehicleId + ", " + data.Latitude + ", " + data.Longitude + ");";
            Console.WriteLine(command);
            Console.Out.Flush();
            return command;
        }

        private string UpdateQuery(string table, VehicleData data)
        {
            Console.WriteLine("Entered update");
            Console.Out.Flush();
            string command = "update " + table + " set \"Timestamp\"='" + ConvertDateToString(data.Timestamp) 
                           + "', \"Latitude\"=" + data.Latitude + ", \"Longitude\"=" + data.Longitude 
                           + " where \"VehicleId\"=" + data.VehicleId + ";";
            Console.WriteLine(command);
            return command;
        }

        private string SelectWhereQuery(string table, string parameter, int value)
        {
            string command = "select * from " + table + " where \"" + parameter + "\"=" + value + " ALLOW FILTERING;";
            Console.WriteLine(command);
            Console.Out.Flush();
            return command;
        }

        private string SelectAllQuery(string table)
        {
            return "select * from " + table + ";";
        }

        private String ConvertDateToString(DateTime date)
        {
            String result = date.Year + "-" + TwoCharacterString(date.Month) + "-" + TwoCharacterString(date.Day) + "T"
                          + TwoCharacterString(date.Hour) + ":" + TwoCharacterString(date.Minute) + ":" + TwoCharacterString(date.Second);
            return result;
        }

        private String TwoCharacterString(int number)
        {
            if (number < 10)
                return "0" + number;
            else return number.ToString();
        }
    }
}
