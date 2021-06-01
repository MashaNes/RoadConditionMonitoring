from datetime import datetime
import time
import requests
import random
import pandas

data_microservice_location = "acquisitiongateway"
data_microservice_port = "80"


def two_digit_representation(value):
    if value < 10:
        return "0" + str(value)
    else:
        return str(value)


def convert_date_to_string(date):
    return str(date.year) + "-" + two_digit_representation(date.month) + "-" + two_digit_representation(date.day) + "T" \
           + two_digit_representation(date.hour) + ":" + two_digit_representation(date.minute) + ":" + \
           two_digit_representation(date.second)


class AirTempData:
    def __init__(self, station_name, lat, long, record_id, air_temp, road_temp,
                 CO_level, NMHC_level, Benzene_level, NOx_level, NO2_level, humidity):
        self.stationName = station_name
        self.latitude = lat
        self.longitude = long
        self.CO = CO_level
        self.NMHC = NMHC_level
        self.Benzene = Benzene_level
        self.NOx = NOx_level
        self.NO2 = NO2_level
        self.relativeHumidity = humidity
        self.recordId = int(record_id)
        self.roadTemperature = road_temp
        self.airTemperature = air_temp
        timestamp = datetime.fromtimestamp(time.time())
        self.timestamp = convert_date_to_string(timestamp)


class DataAcquisition:
    def __init__(self, metadata):
        self.metadata = metadata

    def send_data(self, record_data):
        url = "http://" + data_microservice_location + ":" + data_microservice_port \
              + "/api/" + self.metadata.get_metadata()["sensorType"] + "/add-data"
        values = record_data.__dict__
        headers = {"Content-type": "application/json"}
        r = requests.post(url, json=values, headers=headers)
        print(r.status_code)
        print("Sending")

    def acquisition(self):
        filename = "nis-complete.csv"
        n = sum(1 for line in open(filename)) - 1
        s = 1

        time.sleep(50)

        while True:
            skip = sorted(random.sample(range(1, n + 1), n - s))
            df = pandas.read_csv(filename, skiprows=skip)
            air_data = AirTempData(df.StationName.values[0], float(df.Latitude.values[0]), float(df.Longitude.values[0]),
                                   df.RecordId.values[0], df.AirTemperature.values[0], df.RoadSurfaceTemperature.values[0],
                                   float(df.CO.values[0]), float(df.NMHC.values[0]), float(df.Benzene.values[0]),
                                   float(df.NOx.values[0]), float(df.NO2.values[0]), float(df.RelativeHumidity.values[0]))
            self.send_data(air_data)
            time.sleep(self.metadata.get_metadata()["sampleTime"])
