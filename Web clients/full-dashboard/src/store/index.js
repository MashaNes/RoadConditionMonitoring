import Vue from 'vue'
import Vuex from 'vuex'
import router from "../router/index"

Vue.use(Vuex)

export default new Vuex.Store({
    state: {
        current_vehicle: 0,
        first_enter: true,
        is_data_loaded: true,
        environment_data_list_newest:null,
        environment_data_list_average_h: null,
        environment_data_list_average_day: null,
        location_traffic_data_list: [],
        backend_host: "192.168.0.26",
        backend_port: "49166"
    },
    getters: {

    },
    mutations: {

    },
    actions: {
        getVehicleData({commit}, payload)
        {
            this.state.first_enter = false
            this.state.is_data_loaded = false
            console.log("getting data")
            console.log(payload)
            
            fetch("http://"+this.state.backend_host+":"+this.state.backend_port+"/api/vehicle/get-info/" + payload.id + "/" + payload.radius, {
                method: "GET",
                headers: {
                  "Content-type" : "application/json"
                }
              }).then(response => {
                if(response.ok) {
                  response.json().then(data => {
                    console.log(data)
                    this.state.vehicle_data = data.VehicleData
                    this.state.location_data_list = data.LocationDataList
                    this.state.location_traffic_data_list = data.LocationTrafficDataList
                    this.state.is_data_loaded = true
                  })
                }
                else {      
                    this.state.vehicle_data = null
                    this.state.location_data_list = []
                    this.state.location_traffic_data_list = []         
                    this.state.is_data_loaded = true
                    console.log(response)
                }
              })
        },
        getNewest()
        {
            this.state.is_data_loaded = false
            this.state.environment_data_list_newest = [
                {
                    "Latitude": 43.3185803428779,
                    "Longitude": 21.89078418880797,
                    "Temperature": {
                        "StationName": "Trg vojske",
                        "Timestamp": "2021-06-20T22:43:41+00:00",
                        "AirTemperature": 16.444444444444446,
                        "RoadTemperature": 14.522222222222222
                    },
                    "AirQuality": null
                },
                {
                    "Latitude": 43.32093329043693,
                    "Longitude": 21.89875614869634,
                    "Temperature": {
                        "StationName": "Glavna posta",
                        "Timestamp": "2021-06-20T22:42:18+00:00",
                        "AirTemperature": 15.933333333333334,
                        "RoadTemperature": 22.672222222222224
                    },
                    "AirQuality": {
                        "StationName": "Glavna posta",
                        "Timestamp": "2021-06-20T22:43:47+00:00",
                        "CO": 0.5,
                        "NMHC": 31,
                        "Benzene": 1.6,
                        "NOx": 19,
                        "NO2": 32,
                        "RelativeHumidity": 43.4
                    }
                },
                {
                    "Latitude": 43.31303889779021,
                    "Longitude": 21.898442506301464,
                    "Temperature": {
                        "StationName": "Palilulska rampa",
                        "Timestamp": "2021-06-20T22:44:22+00:00",
                        "AirTemperature": 24.916666666666664,
                        "RoadTemperature": 24.916666666666664
                    },
                    "AirQuality": {
                        "StationName": "Palilulska rampa",
                        "Timestamp": "2021-06-20T22:44:22+00:00",
                        "CO": 3,
                        "NMHC": 306,
                        "Benzene": 12.9,
                        "NOx": 196,
                        "NO2": 133,
                        "RelativeHumidity": 40.1
                    }
                },
                {
                    "Latitude": 43.311984639826775,
                    "Longitude": 21.926425198510245,
                    "Temperature": {
                        "StationName": "Trosarina",
                        "Timestamp": "2021-06-20T22:44:22+00:00",
                        "AirTemperature": 22.038888888888888,
                        "RoadTemperature": 32.05555555555556
                    },
                    "AirQuality": {
                        "StationName": "Trosarina",
                        "Timestamp": "2021-06-20T22:44:22+00:00",
                        "CO": 1.8,
                        "NMHC": 137,
                        "Benzene": 8.8,
                        "NOx": 99,
                        "NO2": 100,
                        "RelativeHumidity": 40.6
                    }
                },
                {
                    "Latitude": 43.319655483853545,
                    "Longitude": 21.929050922399245,
                    "Temperature": {
                        "StationName": "Branka Miljkovica",
                        "Timestamp": "2021-06-20T22:38:10+00:00",
                        "AirTemperature": 22.616666666666664,
                        "RoadTemperature": 22.616666666666664
                    },
                    "AirQuality": {
                        "StationName": "Branka Miljkovica",
                        "Timestamp": "2021-06-20T22:38:10+00:00",
                        "CO": 1.5,
                        "NMHC": 93,
                        "Benzene": 5.3,
                        "NOx": 79,
                        "NO2": 89,
                        "RelativeHumidity": 40.8
                    }
                },
                {
                    "Latitude": 43.32075705011648,
                    "Longitude": 21.902446068081712,
                    "Temperature": null,
                    "AirQuality": {
                        "StationName": "Gimnazija Bora Stankovic",
                        "Timestamp": "2021-06-20T22:37:46+00:00",
                        "CO": 2.8,
                        "NMHC": 322,
                        "Benzene": 11.8,
                        "NOx": 121,
                        "NO2": 103,
                        "RelativeHumidity": 50.8
                    }
                },
                {
                    "Latitude": 43.30804690664374,
                    "Longitude": 21.907602295718902,
                    "Temperature": null,
                    "AirQuality": {
                        "StationName": "Mokranjceva",
                        "Timestamp": "2021-06-20T22:41:47+00:00",
                        "CO": 2.7,
                        "NMHC": 391,
                        "Benzene": 14.7,
                        "NOx": 152,
                        "NO2": 126,
                        "RelativeHumidity": 30.5
                    }
                },
                {
                    "Latitude": 43.31653054295112,
                    "Longitude": 21.93560141001697,
                    "Temperature": null,
                    "AirQuality": {
                        "StationName": "Bulevar Medijana",
                        "Timestamp": "2021-06-20T22:39:47+00:00",
                        "CO": 2.7,
                        "NMHC": 456,
                        "Benzene": 14,
                        "NOx": 143,
                        "NO2": 116,
                        "RelativeHumidity": 40.6
                    }
                }
            ]
            this.state.is_data_loaded = true
        },
        getAverageH()
        {
            this.state.is_data_loaded = false
            this.state.environment_data_list_average_h = [
                {
                    "Latitude": 43.311984639826775,
                    "Longitude": 21.926425198510245,
                    "AverageTemperature": {
                        "AverageAirTemperature": 23.958333333333332,
                        "AverageRoadTemperature": 35.19444444444444,
                        "DataCount": 2
                    },
                    "AverageAirQuality": {
                        "AverageCO": 1.35,
                        "AverageNMHC": 92.5,
                        "AverageBenzene": 5.95,
                        "AverageNOx": 70.5,
                        "AverageNO2": 73,
                        "AverageRelativeHumidity": 29.8,
                        "DataCount": 2
                    }
                },
                {
                    "Latitude": 43.31303889779021,
                    "Longitude": 21.898442506301464,
                    "AverageTemperature": {
                        "AverageAirTemperature": 22.32962962962963,
                        "AverageRoadTemperature": 22.32962962962963,
                        "DataCount": 3
                    },
                    "AverageAirQuality": {
                        "AverageCO": 2.5666666666666664,
                        "AverageNMHC": 354,
                        "AverageBenzene": 11.366666666666667,
                        "AverageNOx": 144.33333333333331,
                        "AverageNO2": 105.33333333333334,
                        "AverageRelativeHumidity": 56.23333333333333,
                        "DataCount": 3
                    }
                },
                {
                    "Latitude": 43.3185803428779,
                    "Longitude": 21.89078418880797,
                    "AverageTemperature": {
                        "AverageAirTemperature": 17.198888888888888,
                        "AverageRoadTemperature": 17.369999999999997,
                        "DataCount": 5
                    },
                    "AverageAirQuality": null
                },
                {
                    "Latitude": 43.319655483853545,
                    "Longitude": 21.929050922399245,
                    "AverageTemperature": {
                        "AverageAirTemperature": 22.616666666666664,
                        "AverageRoadTemperature": 22.616666666666664,
                        "DataCount": 1
                    },
                    "AverageAirQuality": {
                        "AverageCO": 1.5,
                        "AverageNMHC": 93,
                        "AverageBenzene": 5.3,
                        "AverageNOx": 79,
                        "AverageNO2": 89,
                        "AverageRelativeHumidity": 40.8,
                        "DataCount": 1
                    }
                },
                {
                    "Latitude": 43.32093329043693,
                    "Longitude": 21.89875614869634,
                    "AverageTemperature": {
                        "AverageAirTemperature": 17.87222222222222,
                        "AverageRoadTemperature": 24.266666666666666,
                        "DataCount": 2
                    },
                    "AverageAirQuality": {
                        "AverageCO": 1.0666666666666667,
                        "AverageNMHC": 62.333333333333336,
                        "AverageBenzene": 4.333333333333333,
                        "AverageNOx": 74.99999999999999,
                        "AverageNO2": 71,
                        "AverageRelativeHumidity": 47.733333333333334,
                        "DataCount": 3
                    }
                },
                {
                    "Latitude": 43.30804690664374,
                    "Longitude": 21.907602295718902,
                    "AverageTemperature": null,
                    "AverageAirQuality": {
                        "AverageCO": 2.7,
                        "AverageNMHC": 391,
                        "AverageBenzene": 14.7,
                        "AverageNOx": 152,
                        "AverageNO2": 126,
                        "AverageRelativeHumidity": 30.5,
                        "DataCount": 1
                    }
                },
                {
                    "Latitude": 43.31653054295112,
                    "Longitude": 21.93560141001697,
                    "AverageTemperature": null,
                    "AverageAirQuality": {
                        "AverageCO": 4.65,
                        "AverageNMHC": 472,
                        "AverageBenzene": 23.3,
                        "AverageNOx": 241.5,
                        "AverageNO2": 143,
                        "AverageRelativeHumidity": 45.55,
                        "DataCount": 2
                    }
                },
                {
                    "Latitude": 43.32075705011648,
                    "Longitude": 21.902446068081712,
                    "AverageTemperature": null,
                    "AverageAirQuality": {
                        "AverageCO": 2.8,
                        "AverageNMHC": 322,
                        "AverageBenzene": 11.8,
                        "AverageNOx": 121,
                        "AverageNO2": 103,
                        "AverageRelativeHumidity": 50.8,
                        "DataCount": 1
                    }
                }
            ]
            this.state.is_data_loaded = true
        },
        getAverageDay()
        {
            this.state.is_data_loaded = false
            this.state.environment_data_list_average_day = [
                {
                    "Latitude": 43.311984639826775,
                    "Longitude": 21.926425198510245,
                    "AverageTemperature": {
                        "AverageAirTemperature": 23.958333333333332,
                        "AverageRoadTemperature": 35.19444444444444,
                        "DataCount": 2
                    },
                    "AverageAirQuality": {
                        "AverageCO": 1.35,
                        "AverageNMHC": 92.5,
                        "AverageBenzene": 5.95,
                        "AverageNOx": 70.5,
                        "AverageNO2": 73,
                        "AverageRelativeHumidity": 29.8,
                        "DataCount": 2
                    }
                },
                {
                    "Latitude": 43.31303889779021,
                    "Longitude": 21.898442506301464,
                    "AverageTemperature": {
                        "AverageAirTemperature": 22.32962962962963,
                        "AverageRoadTemperature": 22.32962962962963,
                        "DataCount": 3
                    },
                    "AverageAirQuality": {
                        "AverageCO": 2.5666666666666664,
                        "AverageNMHC": 354,
                        "AverageBenzene": 11.366666666666667,
                        "AverageNOx": 144.33333333333331,
                        "AverageNO2": 105.33333333333334,
                        "AverageRelativeHumidity": 56.23333333333333,
                        "DataCount": 3
                    }
                },
                {
                    "Latitude": 43.3185803428779,
                    "Longitude": 21.89078418880797,
                    "AverageTemperature": {
                        "AverageAirTemperature": 17.198888888888888,
                        "AverageRoadTemperature": 17.369999999999997,
                        "DataCount": 5
                    },
                    "AverageAirQuality": null
                },
                {
                    "Latitude": 43.319655483853545,
                    "Longitude": 21.929050922399245,
                    "AverageTemperature": {
                        "AverageAirTemperature": 22.616666666666664,
                        "AverageRoadTemperature": 22.616666666666664,
                        "DataCount": 1
                    },
                    "AverageAirQuality": {
                        "AverageCO": 1.5,
                        "AverageNMHC": 93,
                        "AverageBenzene": 5.3,
                        "AverageNOx": 79,
                        "AverageNO2": 89,
                        "AverageRelativeHumidity": 40.8,
                        "DataCount": 1
                    }
                },
                {
                    "Latitude": 43.32093329043693,
                    "Longitude": 21.89875614869634,
                    "AverageTemperature": {
                        "AverageAirTemperature": 17.87222222222222,
                        "AverageRoadTemperature": 24.266666666666666,
                        "DataCount": 2
                    },
                    "AverageAirQuality": {
                        "AverageCO": 1.0666666666666667,
                        "AverageNMHC": 62.333333333333336,
                        "AverageBenzene": 4.333333333333333,
                        "AverageNOx": 74.99999999999999,
                        "AverageNO2": 71,
                        "AverageRelativeHumidity": 47.733333333333334,
                        "DataCount": 3
                    }
                },
                {
                    "Latitude": 43.30804690664374,
                    "Longitude": 21.907602295718902,
                    "AverageTemperature": null,
                    "AverageAirQuality": {
                        "AverageCO": 2.7,
                        "AverageNMHC": 391,
                        "AverageBenzene": 14.7,
                        "AverageNOx": 152,
                        "AverageNO2": 126,
                        "AverageRelativeHumidity": 30.5,
                        "DataCount": 1
                    }
                },
                {
                    "Latitude": 43.31653054295112,
                    "Longitude": 21.93560141001697,
                    "AverageTemperature": null,
                    "AverageAirQuality": {
                        "AverageCO": 4.65,
                        "AverageNMHC": 472,
                        "AverageBenzene": 23.3,
                        "AverageNOx": 241.5,
                        "AverageNO2": 143,
                        "AverageRelativeHumidity": 45.55,
                        "DataCount": 2
                    }
                },
                {
                    "Latitude": 43.32075705011648,
                    "Longitude": 21.902446068081712,
                    "AverageTemperature": null,
                    "AverageAirQuality": {
                        "AverageCO": 2.8,
                        "AverageNMHC": 322,
                        "AverageBenzene": 11.8,
                        "AverageNOx": 121,
                        "AverageNO2": 103,
                        "AverageRelativeHumidity": 50.8,
                        "DataCount": 1
                    }
                }
            ]
            this.state.is_data_loaded = true
        },
        getTraffic()
        {
            this.state.is_data_loaded = false
            this.state.location_traffic_data_list = [
                {
                    "Latitude": 43.31819537151684,
                    "Longitude": 21.890834822332234,
                    "Radius": 300,
                    "TrafficData":{
                        "VehicleNumber": 15,
                        "AverageSpeed": 25.253
                    }
                },
                {
                    "Latitude": 43.31842954558688,
                    "Longitude": 21.899353517471642,
                    "Radius": 150,
                    "TrafficData":{
                        "VehicleNumber": 30,
                        "AverageSpeed": 10.25
                    }
                },
                {
                    "Latitude": 43.311596073555826,
                    "Longitude": 21.927664677400305,
                    "Radius": 200,
                    "TrafficData":{
                        "VehicleNumber": 7,
                        "AverageSpeed": 22.256
                    }
                },
                {
                    "Latitude": 43.31307870141127,
                    "Longitude": 21.89837420985546,
                    "Radius": 130,
                    "TrafficData":{
                        "VehicleNumber": 50,
                        "AverageSpeed": 7.324
                    }
                },
                {
                    "Latitude": 43.31586130799601,
                    "Longitude": 21.88300421009964,
                    "Radius": 250,
                    "TrafficData":{
                        "VehicleNumber": 12,
                        "AverageSpeed": 39.3675
                    }
                }
            ]
            this.state.is_data_loaded = true
        }
    },
    modules: {

    }
})