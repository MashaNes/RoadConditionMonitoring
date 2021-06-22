<template>
    <div class="main-page">
        <div class="radius">
            <select v-model="environmentSelect" class="selekt" @change="onSelectChange"> 
                <option :value="1"> Newest data </option>
                <option :value="2"> Average data per hour </option>
                <option :value="3"> Average data per day </option>
            </select>
        </div>
        <div class="radius">
            <label class="labela"> Filters: </label>        
            <label class="labelica"> Environment </label>
            <input type="checkbox" v-model="environment"/>        
            <label class="labelica"> Traffic </label>
            <input type="checkbox" v-model="traffic"/>
        </div>
        <div class="mapa">
            <Spinner v-if="!isDataLoaded" />
            <GmapMap v-if="isDataLoaded"
                     :center="{lat:43.32050626745787, lng:21.90057819947256}"
                     :zoom="14"
                     style="width: 100%; height: 700px">
                <GmapMarker :key="index"
                            v-for="(m, index) in EnvironmentMarkersNewest"
                            :position="{lat:m.Latitude, lng:m.Longitude}"
                            :icon="{ url: require('../assets/environment.png')}"
                            @mouseover="showEnvironmentInfo = index;"
                            @mouseout="showEnvironmentInfo = null">
                    <gmap-info-window :opened="showEnvironmentInfo == index">
                        <div class="info-window">
                            <div v-if="m.Temperature">
                                <label class="sekcija"> Temperature </label>
                                <span class="stavka"> 
                                    <label class="naziv"> Station name: </label>
                                    {{m.Temperature.StationName}}
                                </span>
                                <span class="stavka"> 
                                    <label class="naziv"> Timestamp: </label>
                                    {{m.Temperature.Timestamp | showTime}}
                                </span>
                                <span class="stavka"> 
                                    <label class="naziv"> Air temeprature: </label>
                                    {{m.Temperature.AirTemperature | round}} °C
                                </span>
                                <span class="stavka"> 
                                    <label class="naziv"> Road temperature: </label>
                                    {{m.Temperature.RoadTemperature | round}} °C
                                </span>
                            </div>
                            <div v-if="m.AirQuality">                       
                                <label class="sekcija"> Air quality </label>
                                <span class="stavka"> 
                                    <label class="naziv"> Station name: </label>
                                    {{m.AirQuality.StationName}}
                                </span>
                                <span class="stavka"> 
                                    <label class="naziv"> Timestamp: </label>
                                    {{m.AirQuality.Timestamp | showTime}}
                                </span>
                                <span class="stavka"> 
                                    <label class="naziv"> CO: </label>
                                    {{m.AirQuality.CO}} mg/m^3
                                </span>
                                <span class="stavka"> 
                                    <label class="naziv"> Non Metanic HydroCarbons: </label>
                                    {{m.AirQuality.NMHC}} microg/m^3
                                </span>
                                <span class="stavka"> 
                                    <label class="naziv"> Benzene: </label>
                                    {{m.AirQuality.Benzene}} microg/m^3
                                </span>
                                <span class="stavka"> 
                                    <label class="naziv"> NOx: </label>
                                    {{m.AirQuality.NOx}} ppb
                                </span>
                                <span class="stavka"> 
                                    <label class="naziv"> NO2: </label>
                                    {{m.AirQuality.NO2}} microg/m^3
                                </span>
                                <span class="stavka"> 
                                    <label class="naziv"> Relative Humidity: </label>
                                    {{m.AirQuality.RelativeHumidity}} %
                                </span>
                            </div>
                        </div>
                    </gmap-info-window>
                </GmapMarker>
                <GmapMarker :key="index + 'h'"
                            v-for="(m, index) in EnvironmentMarkersAverageH"
                            :position="{lat:m.Latitude, lng:m.Longitude}"
                            :icon="{ url: require('../assets/environment.png')}"
                            @mouseover="showEnvironmentInfo = index;"
                            @mouseout="showEnvironmentInfo = null">
                    <gmap-info-window :opened="showEnvironmentInfo == index">
                        <div class="info-window">
                            <div v-if="m.AverageTemperature">
                                <label class="sekcija"> Temperature </label>
                                <span class="stavka"> 
                                    <label class="naziv"> Average air temeprature: </label>
                                    {{m.AverageTemperature.AverageAirTemperature | round}} °C
                                </span>
                                <span class="stavka"> 
                                    <label class="naziv"> Average road temperature: </label>
                                    {{m.AverageTemperature.AverageRoadTemperature | round}} °C
                                </span>
                                <span class="stavka"> 
                                    <label class="naziv"> Based on: </label>
                                    {{m.AverageTemperature.DataCount}} values
                                </span>
                            </div>
                            <div v-if="m.AverageAirQuality">                       
                                <label class="sekcija"> Air quality </label>
                                <span class="stavka"> 
                                    <label class="naziv"> Average CO: </label>
                                    {{m.AverageAirQuality.AverageCO | round}} mg/m^3
                                </span>
                                <span class="stavka"> 
                                    <label class="naziv"> Average Non Metanic HydroCarbons: </label>
                                    {{m.AverageAirQuality.AverageNMHC | round}} microg/m^3
                                </span>
                                <span class="stavka"> 
                                    <label class="naziv"> Average Benzene: </label>
                                    {{m.AverageAirQuality.AverageBenzene | round}} microg/m^3
                                </span>
                                <span class="stavka"> 
                                    <label class="naziv"> Average NOx: </label>
                                    {{m.AverageAirQuality.AverageNOx | round}} ppb
                                </span>
                                <span class="stavka"> 
                                    <label class="naziv"> Average NO2: </label>
                                    {{m.AverageAirQuality.AverageNO2 | round}} microg/m^3
                                </span>
                                <span class="stavka"> 
                                    <label class="naziv"> Average Relative Humidity: </label>
                                    {{m.AverageAirQuality.AverageRelativeHumidity | round}} %
                                </span>
                                <span class="stavka"> 
                                    <label class="naziv"> Based on: </label>
                                    {{m.AverageAirQuality.DataCount}} values
                                </span>
                            </div>
                        </div>
                    </gmap-info-window>
                </GmapMarker>
                <GmapMarker :key="index + 'd'"
                            v-for="(m, index) in EnvironmentMarkersAverageDay"
                            :position="{lat:m.Latitude, lng:m.Longitude}"
                            :icon="{ url: require('../assets/environment.png')}"
                            @mouseover="showEnvironmentInfo = index;"
                            @mouseout="showEnvironmentInfo = null">
                    <gmap-info-window :opened="showEnvironmentInfo == index">
                        <div class="info-window">
                            <div v-if="m.AverageTemperature">
                                <label class="sekcija"> Temperature </label>
                                <span class="stavka"> 
                                    <label class="naziv"> Average air temeprature: </label>
                                    {{m.AverageTemperature.AverageAirTemperature}} °C
                                </span>
                                <span class="stavka"> 
                                    <label class="naziv"> Average road temperature: </label>
                                    {{m.AverageTemperature.AverageRoadTemperature}} °C
                                </span>
                                <span class="stavka"> 
                                    <label class="naziv"> Based on: </label>
                                    {{m.AverageTemperature.DataCount}} values
                                </span>
                            </div>
                            <div v-if="m.AverageAirQuality">                       
                                <label class="sekcija"> Air quality </label>
                                <span class="stavka"> 
                                    <label class="naziv"> Average CO: </label>
                                    {{m.AverageAirQuality.AverageCO}} mg/m^3
                                </span>
                                <span class="stavka"> 
                                    <label class="naziv"> Average Non Metanic HydroCarbons: </label>
                                    {{m.AverageAirQuality.AverageNMHC}} microg/m^3
                                </span>
                                <span class="stavka"> 
                                    <label class="naziv"> Average Benzene: </label>
                                    {{m.AverageAirQuality.AverageBenzene}} microg/m^3
                                </span>
                                <span class="stavka"> 
                                    <label class="naziv"> Average NOx: </label>
                                    {{m.AverageAirQuality.AverageNOx}} ppb
                                </span>
                                <span class="stavka"> 
                                    <label class="naziv"> Average NO2: </label>
                                    {{m.AverageAirQuality.AverageNO2}} microg/m^3
                                </span>
                                <span class="stavka"> 
                                    <label class="naziv"> Average Relative Humidity: </label>
                                    {{m.AverageAirQuality.AverageRelativeHumidity}} %
                                </span>
                                <span class="stavka"> 
                                    <label class="naziv"> Based on: </label>
                                    {{m.AverageAirQuality.DataCount}} values
                                </span>
                            </div>
                        </div>
                    </gmap-info-window>
                </GmapMarker>
                <GmapMarker :key="index + 't'"
                            v-for="(m, index) in TrafficMarkers"
                            :position="{lat:m.Latitude, lng:m.Longitude}"
                            :icon="{ url: require('../assets/traffic.png')}"
                            @mouseover="showTrafficInfo = index;"
                            @mouseout="showTrafficInfo = null">
                    <gmap-info-window :opened="showTrafficInfo == index">
                        <div class="info-window">
                            <span class="stavka"> 
                                <label class="naziv"> Vehicle count: </label>
                                {{m.TrafficData.VehicleNumber}}
                            </span>
                            <span class="stavka"> 
                                <label class="naziv"> Average speed:  </label>
                                {{m.TrafficData.AverageSpeed}}  km/h
                            </span>
                        </div>
                    </gmap-info-window>
                </GmapMarker>
                <GmapCircle v-for="(pin, index) in TrafficMarkers"
                            :key="index + 'c'"
                            :center="{lat:pin.Latitude, lng:pin.Longitude}"
                            :radius="pin.Radius"
                            :visible="true"
                            :options="{fillColor:'red',fillOpacity:0.2}">
                </GmapCircle>
            </GmapMap>
        </div>
        <div class="radius statistika" v-if="isDataLoaded && GeneralTrafficData">
            Total number of vehicles is {{GeneralTrafficData.VehicleNumber}} and their average speed is {{GeneralTrafficData.AverageSpeed}}
        </div>
    </div>
</template>

<script>
import Spinner from "@/components/Spinner"

export default({
    components:
    {
        Spinner
    },
    data(){
        return{
            radius: 300,
            showEnvironmentInfo: null,
            showTrafficInfo: null,
            environment: true,
            traffic: true,
            environmentSelect: 1
        }
    },
    computed:
    {
        isDataLoaded()
        {
            return this.$store.state.is_data_loaded
        },
        EnvironmentMarkersNewest()
        {
            if(!this.environment || this.environmentSelect != 1)
                return []
            return this.$store.state.environment_data_list_newest
        },
        EnvironmentMarkersAverageH()
        {
            if(!this.environment || this.environmentSelect != 2)
                return []
            return this.$store.state.environment_data_list_average_h
        },
        EnvironmentMarkersAverageDay()
        {
            if(!this.environment || this.environmentSelect != 3)
                return []
            return this.$store.state.environment_data_list_average_day
        },
        TrafficMarkers()
        {
            if(!this.traffic)
                return []
            return this.$store.state.location_traffic_data_list
        },
        GeneralTrafficData()
        {
            return this.$store.state.general_traffic_data
        }
    },
    methods:
    {
        onSelectChange()
        {
            if(this.environmentSelect == 1 && !this.$store.state.environment_data_list_newest)
                this.$store.dispatch("getNewest")
            if(this.environmentSelect == 2 && !this.$store.state.environment_data_list_average_h)
                this.$store.dispatch("getAverageH")
            if(this.environmentSelect == 3 && !this.$store.state.environment_data_list_average_day)
                this.$store.dispatch("getAverageDay")
        },
        subscribeToEvents() 
        {
            this.$roadMonitorHub.JoinNewestGroup()
            this.$roadMonitorHub.JoinAverageHGroup()
            this.$roadMonitorHub.JoinAverageDayGroup()
            this.$roadMonitorHub.JoinTrafficGroup()
            this.$roadMonitorHub.$on('new_newest', this.onNewNewestData)
            this.$roadMonitorHub.$on('new_averageH', this.onNewHData)
            this.$roadMonitorHub.$on('new_averageDay', this.onNewDayData)
            this.$roadMonitorHub.$on('new_traffic', this.onNewTrafficData)
            window.addEventListener('beforeunload', this.leavePage)
        },
        onNewNewestData(data)
        {
            this.$store.state.environment_data_list_newest = data.map(x => {
                return{             
                    AirQuality: x.airQuality ? {
                        Benzene: x.airQuality.benzene,
                        CO: x.airQuality.co,
                        NO2: x.airQuality.nO2,
                        NOx: x.airQuality.nOx,
                        NMHC: x.airQuality.nmhc,
                        RelativeHumidity: x.airQuality.relativeHumidity,
                        StationName: x.airQuality.stationName,
                        Timestamp: x.airQuality.timestamp
                    } : null,
                    Latitude: x.latitude,
                    Longitude: x.longitude,
                    Temperature: x.temperature ? {
                        AirTemperature: x.temperature.airTemperature,
                        RoadTemperature: x.temperature.roadTemperature,
                        StationName: x.temperature.stationName,
                        Timestamp: x.temperature.timestamp
                    } : null
                }
            })
        },
        onNewHData(data)
        {
            this.$store.state.environment_data_list_average_h = data.map(x => {
                return{             
                    AverageAirQuality: x.averageAirQuality ? {
                        AverageBenzene: x.averageAirQuality.averageBenzene,
                        AverageCO: x.averageAirQuality.averageCO,
                        AverageNO2: x.averageAirQuality.averageNO2,
                        AverageNOx: x.averageAirQuality.averageNOx,
                        AverageNMHC: x.averageAirQuality.averageNMHC,
                        AverageRelativeHumidity: x.averageAirQuality.averageRelativeHumidity,
                        DataCount: x.averageAirQuality.dataCount
                    } : null,
                    Latitude: x.latitude,
                    Longitude: x.longitude,
                    AverageTemperature: x.averageTemperature ? {
                        AverageAirTemperature: x.averageTemperature.averageAirTemperature,
                        AverageRoadTemperature: x.averageTemperature.averageRoadTemperature,
                        DataCount: x.averageTemperature.dataCount
                    } : null
                }
            })
        },
        onNewDayData(data)
        {
            this.$store.state.environment_data_list_average_day = data.map(x => {
                return{             
                    AverageAirQuality: x.averageAirQuality ? {
                        AverageBenzene: x.averageAirQuality.averageBenzene,
                        AverageCO: x.averageAirQuality.averageCO,
                        AverageNO2: x.averageAirQuality.averageNO2,
                        AverageNOx: x.averageAirQuality.averageNOx,
                        AverageNMHC: x.averageAirQuality.averageNMHC,
                        AverageRelativeHumidity: x.averageAirQuality.averageRelativeHumidity,
                        DataCount: x.averageAirQuality.dataCount
                    } : null,
                    Latitude: x.latitude,
                    Longitude: x.longitude,
                    AverageTemperature: x.averageTemperature ? {
                        AverageAirTemperature: x.averageTemperature.averageAirTemperature,
                        AverageRoadTemperature: x.averageTemperature.averageRoadTemperature,
                        DataCount: x.averageTemperature.dataCount
                    } : null
                }
            })
        },
        onNewTrafficData(data)
        {
            this.$store.state.location_traffic_data_list = data.locationTrafficData.map(x => {
                return{
                    Latitude: x.latitude,
                    Longitude: x.longitude,
                    Radius: x.radius,
                    TrafficData: {
                        AverageSpeed: x.trafficData.averageSpeed,
                        VehicleNumber: x.trafficData.vehicleNumber
                    }
                }
            })
            this.$store.state.general_traffic_data = {
                AverageSpeed: data.globalTrafficData.averageSpeed,
                VehicleNumber: data.globalTrafficData.vehicleNumber
            }
        },
        clearSignalRSubscription() {
            this.$roadMonitorHub.LeaveNewestGroup()
            this.$roadMonitorHub.LeaveAverageHGroup()
            this.$roadMonitorHub.LeaveAverageDayGroup()
            this.$roadMonitorHub.LeaveTrafficGroup()
            this.$roadMonitorHub.$off('new_newest')
            this.$roadMonitorHub.$off('new_averageH')
            this.$roadMonitorHub.$off('new_averageDay')
            this.$roadMonitorHub.$off('new_traffic')
            window.removeEventListener('beforeunload', this.leavePage)
        },
        leavePage(event) {
            event.preventDefault()
            this.clearSignalRSubscription()
            event.returnValue = ''
        }
    },
    created()
    {
        this.$store.dispatch("getNewest")
        this.$store.dispatch("getTraffic")
        this.subscribeToEvents()
    },
    beforeDestroy() {
      this.clearSignalRSubscription()
    }
})
</script>

<style scoped>
    .main-page
    {
        padding:15px;
        height: 100%;
        display: flex;
        flex-direction: column;
        align-items: center;
        justify-content: center;
        padding-top: 30px;
    }

    .radius
    {
        display: flex;
        flex-direction: row;
        width: 100%;
        margin-bottom:20px;
        padding-left:20px;
        align-items: center;
        justify-content: flex-start;
    }

    .labela
    {
        font-weight: 600;
        font-size: 18px;
        margin-right: 15px;
        margin-bottom: 0px;
    }

    .labelica
    {
        margin-bottom: 0px;
        margin-right: 10px;
        margin-left: 15px;
    }

    .mapa
    {
        width: 96%;
        height: 700px;
    }

    .info-window
    {
        display: flex;
        flex-direction: column;
        align-items: flex-start;
        justify-content: space-evenly;
    }

    .no-data
    {
        font-size: 24px;
        font-style: italic;
        margin-top: 20px;
        color: grey;
    }

    .stavka
    {
        margin-bottom: 5px;
        font-size: 14px;
        display: flex;
        flex-direction: row;
    }

    .naziv
    {
        margin-bottom: 0px;
        margin-right: 5px;
        font-weight: 600;
    }

    .sekcija
    {
        margin-bottom: 7px;
        margin-top: 10px;
        font-size: 16px;
        font-weight: 700;
    }

    .selekt
    {
        padding:5px;
    }

    .statistika
    {
        margin-top: 10px;
        font-size: 20px;
    }
</style>