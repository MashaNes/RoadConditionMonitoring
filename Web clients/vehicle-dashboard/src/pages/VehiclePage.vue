<template>
    <div class="main-page">
        <div class="radius">
            <label class="labela"> Radius of area of interest (m) </label>
            <input type="number" min="0" v-model="radius"/>
        </div>
        <div class="radius">
            <label class="labela"> Filters: </label>        
            <label class="labelica"> Environment </label>
            <input type="checkbox" v-model="environment"/>        
            <label class="labelica"> Traffic </label>
            <input type="checkbox" v-model="traffic"/>
        </div>
        <div class="mapa">
            <label class="no-data" v-if="isDataLoaded && !vehicleData"> No data is currently available for this vehicle </label>
            <Spinner v-if="!isDataLoaded && isFirstEnter" />
            <GmapMap v-if="(isDataLoaded || !isFirstEnter) && vehicleData"
                     :center="{lat:43.32050626745787, lng:21.90057819947256}"
                     :zoom="14"
                     style="width: 100%; height: 700px">
                <GmapMarker :position="{lat:vehicleData.Latitude, lng:vehicleData.Longitude}"
                            :icon="{ url: require('../assets/car.png')}"
                            @mouseover="showVehicleInfo = true;"
                            @mouseout="showVehicleInfo = false">
                    <gmap-info-window :opened="showVehicleInfo">
                        <div class="info-window">
                            <span class="stavka"> 
                                <label class="naziv"> Timestamp:  </label>
                                {{vehicleData.Timestamp | showTime}} 
                            </span>
                        </div>
                    </gmap-info-window>
                </GmapMarker>
                <GmapMarker :key="index"
                            v-for="(m, index) in EnvironmentMarkers"
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
                                    {{m.Temperature.AirTemperature}} °C
                                </span>
                                <span class="stavka"> 
                                    <label class="naziv"> Road temperature: </label>
                                    {{m.Temperature.RoadTemperature}} °C
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
    </div>
</template>

<script>
import Spinner from "@/components/Spinner.vue"

export default({
    components:
    {
        Spinner
    },
    data(){
        return{
            radius: 300,
            showVehicleInfo: false,
            showEnvironmentInfo: null,
            showTrafficInfo: null,
            environment: true,
            traffic: true,
            intervalFunc: null
        }
    },
    computed:
    {
        vehicleId()
        {
            return this.$store.state.current_vehicle
        },
        vehicleData()
        {
            return this.$store.state.vehicle_data
        },
        isDataLoaded()
        {
            return this.$store.state.is_data_loaded
        },
        isFirstEnter()
        {
            return this.$store.state.first_enter
        },
        EnvironmentMarkers()
        {
            if(!this.environment)
                return []
            return this.$store.state.location_data_list
        },
        TrafficMarkers()
        {
            if(!this.traffic)
                return []
            return this.$store.state.location_traffic_data_list
        }
    },
    methods:
    {
        getData()
        {
            this.$store.dispatch("getVehicleData", 
            {
                id: this.vehicleId,
                radius: this.radius
            })
        },
        leavePage(event)
        {
            event.preventDefault()
            clearInterval(this.intervalFunc)
            event.returnValue = ''
        }
    },
    created()
    {
        this.getData()
        window.addEventListener('beforeunload', this.leavePage)
        this.intervalFunc = setInterval(this.getData, 5000)
    },
    beforeDestroy()
    {
        clearInterval(this.intervalFunc)       
        this.$store.state.first_enter = true
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
</style>