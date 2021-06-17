from datetime import datetime
import time
import pandas
import requests

vehicle_gateway_location = "vehiclegateway"
vehicle_gateway_port = "80"


def two_digit_representation(value):
    if value < 10:
        return "0" + str(value)
    else:
        return str(value)


def convert_date_to_string(date):
    return str(date.year) + "-" + two_digit_representation(date.month) + "-" + two_digit_representation(date.day) + "T" \
           + two_digit_representation(date.hour) + ":" + two_digit_representation(date.minute) + ":" + \
           two_digit_representation(date.second)


class VehicleData:
    def __init__(self, vehicle_id, vehicle_speed, latitude, longitude):
        self.VehicleId = vehicle_id
        self.VehicleSpeed = vehicle_speed
        self.Latitude = latitude
        self.Longitude = longitude
        timestamp = datetime.fromtimestamp(time.time())
        self.Timestamp = convert_date_to_string(timestamp)

    def print(self):
        print("VehicleId: {}".format(self.VehicleId))
        print("     Speed: {}".format(self.VehicleSpeed))
        print("     Latitude: {}".format(self.Latitude))
        print("     Longitude: {}".format(self.Longitude))
        print("     Timestamp: {}".format(self.Timestamp))
        print("")


def send_data(data_to_send):
    url = "http://" + vehicle_gateway_location + ":" + vehicle_gateway_port \
          + "/api/trajectory_MS/add-data"
    values = data_to_send.__dict__
    headers = {"Content-type": "application/json"}
    r = requests.post(url, json=values, headers=headers)
    print(r.status_code)
    print("Sending")
    vehicle_data.print()


filename = "nis-trajectories.csv"
time.sleep(30)
time_ind = 0
time_boundary = 4149
chunk_size = 15
next_step_data = []
skip_rows = 0

while True:
    index_to_delete = 0
    if len(next_step_data) > 0:
        for (i, data) in enumerate(next_step_data):
            if data[0] == time_ind:
                vehicle_data = VehicleData(data[2], data[6], data[8], data[9])
                send_data(vehicle_data)
                index_to_delete = i
            else:
                break
        next_step_data = next_step_data[index_to_delete+1:]

    while len(next_step_data) == 0:
        df = pandas.read_csv(filename, skiprows=skip_rows, nrows=chunk_size)
        skip_rows += chunk_size
        next_step_data = df.values
        index_to_delete = 0
        for (i, data) in enumerate(next_step_data):
            if data[0] == time_ind:
                vehicle_data = VehicleData(data[2], data[6], data[8], data[9])
                send_data(vehicle_data)
                index_to_delete = i
            else:
                break
        next_step_data = next_step_data[index_to_delete+1:]

    time_ind += 1
    if time_ind >= time_boundary:
        time.sleep(60)
        time_ind = 0
        skip_rows = 0
    time.sleep(10)
