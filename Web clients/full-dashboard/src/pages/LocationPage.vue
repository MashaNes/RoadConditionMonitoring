<template>
    <div class="main-page">
        <div class="radius">
            <select v-model="searchOption" class="selekt" @change="clearSearch"> 
                <option :value="index" v-for="(opcija, index) in options" :key="index+'o'">
                     {{opcija}} 
                </option>
            </select>
            <label class="labelica"> Radius (m) </label>
            <input type="number" class="broj" v-model="radius" @change="redrawMap" />
            <label class="labelica"> Traffic </label>
            <input type="checkbox" v-model="traffic" @change="clearSearch"/>
            <div v-if="searchOption == 3 || searchOption == 4">
                <label class="labelica"> Year </label>
                <input type="number" class="broj" v-model="date.year"/>
                <label class="labelica"> Month </label>
                <input type="number" class="broj" v-model="date.month"/>
                <label class="labelica"> Day </label>
                <input type="number" class="broj" v-model="date.day"/>
                <label class="labelica" v-if="searchOption == 3"> Hour </label>
                <input type="number" class="broj" v-model="date.hour" v-if="searchOption == 3"/>
            </div>
            <button class="button btn-primary dugme" :disabled="!isButtonAvailable" @click="search"> Search </button>
        </div>
        <div class="mapa">
            <Spinner v-if="!isDataLoaded" />
            <GmapMap v-if="isDataLoaded"
                     @click="mark"
                     :center="{lat:43.32050626745787, lng:21.90057819947256}"
                     :zoom="14"
                     style="width: 100%; height: 700px">
                <GmapMarker  v-model="location"
                            :position="location"/>
                <GmapCircle :center="location"
                            :radius="parseFloat(radius)"
                            :visible="true"
                            :options="{fillColor:'blue',fillOpacity:0.1}" />
                <GmapMarker :key="index + 'n'"
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
                            v-for="(m, index) in EnvironmentMarkersAverage"
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
                <GmapMarker :key="index"
                            v-for="(m, index) in AllMarkers"
                            :position="{lat:m.Latitude, lng:m.Longitude}"
                            :icon="{ url: require('../assets/environment.png')}"
                            @mouseover="showEnvironmentInfo = index;"
                            @mouseout="showEnvironmentInfo = null">
                    <gmap-info-window :opened="showEnvironmentInfo == index">
                        <div class="info-window">
                            <span class="stavka"> 
                                <label class="naziv"> Reference number: </label>
                                {{index + 1}}
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
        <div class="tabele" v-if="searchOption == 5 && isDataLoaded && wasSearched">
            <table class="table table-dark tabela1">
                <thead>
                    <tr>
                        <th scope="col">#</th>
                        <th scope="col">Station name</th>
                        <th scope="col">Timestamp</th>
                        <th scope="col">Air temperature</th>
                        <th scope="col">Road temperature</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="(marker, i) in AllMarkersTemperatures" :key="i+'tab'">
                        <th scope="row">{{marker.Id}}</th>
                        <td>{{marker.StationName}}</td>
                        <td>{{marker.Timestamp | showTime}}</td>
                        <td>{{marker.AirTemperature}}</td>
                        <td>{{marker.RoadTemperature}}</td>
                    </tr>
                </tbody>
            </table>
            <table class="table table-dark tabela2">
                <thead>
                    <tr>
                        <th scope="col">#</th>
                        <th scope="col">Station name</th>
                        <th scope="col">Timestamp</th>
                        <th scope="col">CO</th>
                        <th scope="col">NMHC</th>
                        <th scope="col">Benzene</th>
                        <th scope="col">NOx</th>
                        <th scope="col">NO2</th>
                        <th scope="col">Relative humidity</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="(marker, i) in AllMarkersAirQualities" :key="i+'tab'">
                        <th scope="row">{{marker.Id}}</th>
                        <td>{{marker.StationName}}</td>
                        <td>{{marker.Timestamp | showTime}}</td>
                        <td>{{marker.CO}}</td>
                        <td>{{marker.NMHC}}</td>
                        <td>{{marker.Benzene}}</td>
                        <td>{{marker.NOx}}</td>
                        <td>{{marker.NO2}}</td>
                        <td>{{marker.RelativeHumidity}}</td>
                    </tr>
                </tbody>
            </table>
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
            traffic: false,
            searchOption: 6,
            location: {lat:43.32050626745787, lng:21.90057819947256},
            date: {year: 0, month: 0, day: 0, hour: 0},
            radius: "100",
            circle: null,
            showEnvironmentInfo : null,
            showTrafficInfo : null,
            wasSearched: false
        }
    },
    computed:
    {
        options()
        {
            return [
                "Newest data", 
                "Current average per hour", 
                "Current average per day", 
                "Average per selected hour",
                "Average per selected day",
                "All",
                "None"
            ]
        },
        isButtonAvailable()
        {
            return this.searchOption < 6 || this.traffic
        },
        isDataLoaded()
        {
            return this.$store.state.is_data_loaded
        },
        EnvironmentMarkersNewest()
        {
            if(this.searchOption != 0 || !this.isDataLoaded || !this.wasSearched)
                return []
            return this.$store.state.filter_loc_newest
        },
        EnvironmentMarkersAverage()
        {
            if(this.searchOption < 1 || this.searchOption > 4 || !this.isDataLoaded || !this.wasSearched)
                return []
            return this.$store.state.filter_loc_average
        },
        TrafficMarkers()
        {
            if(!this.traffic || !this.isDataLoaded || !this.wasSearched)
                return []
            return this.$store.state.loc_traffic
        },
        AllMarkers()
        {
            if(this.searchOption != 5 || !this.isDataLoaded || !this.wasSearched)
                return []
            return this.$store.state.filter_loc_all
        },
        AllMarkersTemperatures()
        {
            var arr = []
            this.AllMarkers.forEach((el, ind) => {
                el.Temperatures.forEach(temp =>
                {             
                    arr.push({
                        Id: ind + 1,
                        StationName: temp.StationName,
                        Timestamp: temp.Timestamp,
                        AirTemperature: temp.AirTemperature,
                        RoadTemperature: temp.RoadTemperature
                    })
                })
            });
            return arr;
        },
        AllMarkersAirQualities()
        {
            var arr = []
            this.AllMarkers.forEach((el, ind) => {
                el.AirQualities.forEach(air =>
                {                   
                    arr.push({
                        Id: ind + 1,
                        StationName: air.StationName,
                        Timestamp: air.Timestamp,
                        CO: air.CO,
                        NMHC: air.NMHC,
                        Benzene: air.Benzene,
                        NOx: air.NOx,
                        NO2: air.NO2,
                        RelativeHumidity: air.RelativeHumidity
                    })
                })
            });
            return arr;
        }
    },
    methods:
    {
        clearSearch()
        {
            this.wasSearched = false
        },
        mark(event)
        {
            this.location.lat = event.latLng.lat()
            this.location.lng = event.latLng.lng()
        },
        redrawMap()
        {
            this.$store.state.is_data_loaded = false
            this.$store.state.is_data_loaded = true
        },
        search()
        {
            this.wasSearched = true
            var locDTO = {
                Latitude : this.location.lat,
                Longitude : this.location.lng,
                RadiusMeters : parseFloat(this.radius)
            }

            if(this.searchOption == 0)
                this.$store.dispatch("getLocNewest", locDTO)
            else if(this.searchOption == 1)
                this.$store.dispatch("getLocAverageH", locDTO)
            else if(this.searchOption == 2)
                this.$store.dispatch("getLocAverageDay", locDTO)
            else if(this.searchOption == 5)
                this.$store.dispatch("getLocAll", locDTO)
            else if(this.searchOption == 3 || this.searchOption == 4)
            {
                var avgDTO = {
                    LocationInfo: locDTO,
                    PerHour: this.searchOption == 3 ? true : false,
                    Timestamp: this.date.year + "-" + this.convertToTwoChar(this.date.month) + "-"
                                + this.convertToTwoChar(this.date.day) + "T"
                                + (this.searchOption == 3 ? this.convertToTwoChar(this.date.hour) : "00")
                                + ":00:00"
                }
                this.$store.dispatch("getLocAverage", avgDTO)
            }

            if(this.traffic)
                this.$store.dispatch("getLocTraffic", locDTO)
        },
        convertToTwoChar(val)
        {
            var int = parseInt(val)
            if(int < 10)
                return "0" + val
            return val
        }
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

    .mapa
    {
        width: 96%;
        height: 700px;
    }

    .radius
    {
        display: flex;
        flex-direction: row;
        width: 90%;
        margin-bottom:20px;
        padding-left:20px;
        align-items: center;
        justify-content: flex-start;
        background-color: white;
        padding-top: 10px;
        padding-bottom: 10px;
        border-radius: 10px;
    }

    .labelica
    {
        margin-bottom: 0px;
        margin-right: 10px;
        margin-left: 15px;
    }

    .broj
    {
        width: 80px;
    }

    .selekt
    {
        padding:5px;
    }

    .dugme
    {
        margin-left: 30px;
    }

    .tabele
    {
        width: 96%;
        padding-top: 30px;
        display: flex;
        flex-direction: row;
        align-items: flex-start;
        justify-content: space-evenly;
    }

    .table-dark th
    {
        color: white;
    }

    .tabela1
    {
        width:45%;
    }

    .tabela2
    {
        width:52%;
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