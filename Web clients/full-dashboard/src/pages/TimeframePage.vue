<template>
    <div class="main-page">
        <div class="radius">
            <select v-model="searchOption" class="selekt" @change="clearSearch"> 
                <option :value="index" v-for="(opcija, index) in options" :key="index+'o'">
                     {{opcija}} 
                </option>
            </select>
            <div v-if="searchOption == 1 || searchOption == 2">
                <label class="labelica"> Year </label>
                <input type="number" class="broj" v-model="year"/>
                <label class="labelica"> Month </label>
                <input type="number" class="broj" v-model="month"/>
                <label class="labelica"> Day </label>
                <input type="number" class="broj" v-model="day"/>
                <label class="labelica" v-if="searchOption == 2"> Hour </label>
                <input type="number" class="broj" v-model="hour" v-if="searchOption == 2"/>
            </div>
            <div v-if="searchOption == 0" class="datum">              
                <label class="labelica"> Date and time </label>
                <Datetime class="desno" :type="'datetime'" v-model="date"/>               
                <label class="labelica"> Seconds </label>
                <input type="number" :min="0" :step="1" class="broj" v-model="seconds" />
            </div>
            <button class="button btn-primary dugme" :disabled="!isButtonAvailable" @click="search"> Search </button>
        </div>
        <div class="mapa">
            <Spinner v-if="!isDataLoaded" />
            <GmapMap v-if="isDataLoaded"
                     :center="{lat:43.32050626745787, lng:21.90057819947256}"
                     :zoom="14"
                     style="width: 100%; height: 700px">
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
            </GmapMap>
        </div>
        <div class="tabele" v-if="searchOption == 0 && isDataLoaded && wasSearched">
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
import { Datetime } from 'vue-datetime';
import 'vue-datetime/dist/vue-datetime.css'
import Spinner from "@/components/Spinner.vue"

export default({
    components:
    {
        Datetime,
        Spinner
    },
    data(){
        return{
            year: 0,
            month: 0,
            day: 0,
            hour: 0,
            seconds: 0,
            date: this.convertDateToString(new Date(Date.now())),
            wasSearched: false,
            searchOption: -1,
            showEnvironmentInfo: null
        }
    },
    methods:
    {
        convertDateToString(date)
        {
            var year = date.getFullYear() + ''
            var month = this.twoCharString(date.getMonth() + 1)
            var day = this.twoCharString(date.getDate())
            var hours = this.twoCharString(date.getHours())
            var minutes = this.twoCharString(date.getMinutes())
            return year + "-" + month + "-" + day + "T" + hours + ":" + minutes + ":00"
        },
        twoCharString(number)
        {
            if(number > 9)
                return number + ''
            return "0" + number
        },
        clearSearch()
        {
            this.wasSearched = false
        },
        search()
        {
            var payload
            this.wasSearched = true
            if(this.searchOption == 0)
            {
                payload = {
                    Seconds: this.seconds,
                    ReferentTime: this.date
                }
                this.$store.dispatch("getTimeframe", payload)
            }
            else if(this.searchOption == 1)
            {
                payload = {
                    year: this.year,
                    month: this.month,
                    day: this.day
                }
                this.$store.dispatch("getDay", payload)
            }
            else if(this.searchOption == 2)
            {
                payload = {
                    year: this.year,
                    month: this.month,
                    day: this.day,
                    hour: this.hour
                }
                this.$store.dispatch("getHour", payload)
            }
        }
    },
    computed:
    {
        options()
        {
            return ["Timeframe", "Average per some day", "Average per some hour"]
        },
        isButtonAvailable()
        {
            return this.searchOption >= 0 && this.searchOption < 3
        },
        isDataLoaded()
        {
            return this.$store.state.is_data_loaded
        },
        EnvironmentMarkersAverage()
        {
            if(this.searchOption < 1 || !this.isDataLoaded || !this.wasSearched)
                return []
            return this.$store.state.filter_time_average
        },
        AllMarkers()
        {
            if(this.searchOption != 0 || !this.isDataLoaded || !this.wasSearched)
                return []
            return this.$store.state.filter_time_frame
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

    .datum
    {
        display: flex;
        flex-direction: row;
        align-items: center;
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
</style>