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
        general_traffic_data: null,
        filter_loc_newest: [],
        filter_loc_average: [],
        filter_loc_all: [],
        filter_time_average: [],
        filter_time_frame: [],
        loc_traffic: [],
        backend_host: "192.168.0.26",
        backend_port: "49164"
    },
    getters: {

    },
    mutations: {

    },
    actions: {
        getNewest()
        {
            this.state.is_data_loaded = false
            fetch("http://"+this.state.backend_host+":"+this.state.backend_port+"/api/monitoring-subscription/get-newest", {
                method: "GET",
                headers: {
                  "Content-type" : "application/json"
                }
              }).then(response => {
                if(response.ok) {
                  response.json().then(data => {
                    this.state.environment_data_list_newest = data
                    this.state.is_data_loaded = true
                  })
                }
                else {                  
                    this.state.is_data_loaded = true
                    console.log(response)
                }
              })
        },
        getAverageH()
        {
            this.state.is_data_loaded = false
            fetch("http://"+this.state.backend_host+":"+this.state.backend_port+"/api/monitoring-subscription/get-average-per-hour", {
                method: "GET",
                headers: {
                  "Content-type" : "application/json"
                }
              }).then(response => {
                if(response.ok) {
                  response.json().then(data => {
                    this.state.environment_data_list_average_h = data
                    this.state.is_data_loaded = true
                  })
                }
                else {                  
                    this.state.is_data_loaded = true
                    console.log(response)
                }
              })
        },
        getAverageDay()
        {
            this.state.is_data_loaded = false
            fetch("http://"+this.state.backend_host+":"+this.state.backend_port+"/api/monitoring-subscription/get-average-per-day", {
                method: "GET",
                headers: {
                  "Content-type" : "application/json"
                }
              }).then(response => {
                if(response.ok) {
                  response.json().then(data => {
                    this.state.environment_data_list_average_day = data
                    this.state.is_data_loaded = true
                  })
                }
                else {                  
                    this.state.is_data_loaded = true
                    console.log(response)
                }
              })
        },
        getTraffic()
        {
            this.state.is_data_loaded = false
            fetch("http://"+this.state.backend_host+":"+this.state.backend_port+"/api/monitoring-subscription/get-traffic-data", {
                method: "GET",
                headers: {
                  "Content-type" : "application/json"
                }
              }).then(response => {
                if(response.ok) {
                  response.json().then(data => {
                    this.state.location_traffic_data_list = data.LocationTrafficData
                    this.state.general_traffic_data = data.GlobalTrafficData
                    this.state.is_data_loaded = true
                  })
                }
                else {                  
                    this.state.is_data_loaded = true
                    console.log(response)
                }
              })
        },
        getLocNewest({commit}, payload)
        {
            this.state.is_data_loaded = false
            fetch("http://"+this.state.backend_host+":"+this.state.backend_port+"/api/monitoring-location/get-newest", {
                method: 'POST',
                mode: "cors",
                headers: {
                    "Content-type" : "application/json",
                },
                body: JSON.stringify(payload)
              }).then(response => {
                if(response.ok) {
                  response.json().then(data => {
                    this.state.filter_loc_newest = data
                    this.state.is_data_loaded = true
                  })
                }
                else {                  
                    this.state.is_data_loaded = true
                    console.log(response)
                }
              })
        },
        getLocAverageH({commit}, payload)
        {
            this.state.is_data_loaded = false
            fetch("http://"+this.state.backend_host+":"+this.state.backend_port+"/api/monitoring-location/get-average-h", {
                method: 'POST',
                mode: "cors",
                headers: {
                    "Content-type" : "application/json",
                },
                body: JSON.stringify(payload)
              }).then(response => {
                if(response.ok) {
                  response.json().then(data => {
                    this.state.filter_loc_average = data
                    this.state.is_data_loaded = true
                  })
                }
                else {                  
                    this.state.is_data_loaded = true
                    console.log(response)
                }
              })
        },
        getLocAverageDay({commit}, payload)
        {
            this.state.is_data_loaded = false
            fetch("http://"+this.state.backend_host+":"+this.state.backend_port+"/api/monitoring-location/get-average-day", {
                method: 'POST',
                mode: "cors",
                headers: {
                    "Content-type" : "application/json",
                },
                body: JSON.stringify(payload)
              }).then(response => {
                if(response.ok) {
                  response.json().then(data => {
                    this.state.filter_loc_average = data
                    this.state.is_data_loaded = true
                  })
                }
                else {                  
                    this.state.is_data_loaded = true
                    console.log(response)
                }
              })
        },
        getLocAverage({commit}, payload)
        {
            this.state.is_data_loaded = false
            fetch("http://"+this.state.backend_host+":"+this.state.backend_port+"/api/monitoring-location/get-average", {
                method: 'POST',
                mode: "cors",
                headers: {
                    "Content-type" : "application/json",
                },
                body: JSON.stringify(payload)
              }).then(response => {
                if(response.ok) {
                  response.json().then(data => {
                    this.state.filter_loc_average = data
                    this.state.is_data_loaded = true
                  })
                }
                else {                  
                    this.state.is_data_loaded = true
                    console.log(response)
                }
              })
        },
        getLocTraffic({commit}, payload)
        {
            this.state.is_data_loaded = false
            fetch("http://"+this.state.backend_host+":"+this.state.backend_port+"/api/monitoring-location/get-traffic-data", {
                method: 'POST',
                headers: {
                    "Content-type" : "application/json",
                },
                body: JSON.stringify(payload)
              }).then(response => {
                if(response.ok) {
                  response.json().then(data => {
                    this.state.loc_traffic = data
                    this.state.is_data_loaded = true
                  })
                }
                else {                  
                    this.state.is_data_loaded = true
                    console.log(response)
                }
              })
        },
        getLocAll({commit}, payload)
        {
            this.state.is_data_loaded = false
            fetch("http://"+this.state.backend_host+":"+this.state.backend_port+"/api/monitoring-location/get-all", {
                method: 'POST',
                mode: "cors",
                headers: {
                    "Content-type" : "application/json",
                },
                body: JSON.stringify(payload)
              }).then(response => {
                if(response.ok) {
                  response.json().then(data => {
                    this.state.filter_loc_all = data
                    this.state.is_data_loaded = true
                  })
                }
                else {                  
                    this.state.is_data_loaded = true
                    console.log(response)
                }
              })
        },
        getTimeframe({commit}, payload)
        {
            this.state.is_data_loaded = false
            fetch("http://"+this.state.backend_host+":"+this.state.backend_port+"/api/monitoring-time/get-timeframe", {
                method: 'POST',
                mode: "cors",
                headers: {
                    "Content-type" : "application/json",
                },
                body: JSON.stringify(payload)
              }).then(response => {
                if(response.ok) {
                  response.json().then(data => {
                    this.state.filter_time_frame = data
                    this.state.is_data_loaded = true
                  })
                }
                else {                  
                    this.state.is_data_loaded = true
                    console.log(response)
                }
              })
        },
        getDay({commit}, payload)
        {
            this.state.is_data_loaded = false
            fetch("http://"+this.state.backend_host+":"+this.state.backend_port+"/api/monitoring-time/get-average-day/"
                + payload.year + "/" + payload.month + "/" + payload.day, {
                method: 'GET',
                headers: {
                    "Content-type" : "application/json",
                }
              }).then(response => {
                if(response.ok) {
                  response.json().then(data => {
                    this.state.filter_time_average = data
                    this.state.is_data_loaded = true
                  })
                }
                else {                  
                    this.state.is_data_loaded = true
                    console.log(response)
                }
              })
        },
        getHour({commit}, payload)
        {
            this.state.is_data_loaded = false
            fetch("http://"+this.state.backend_host+":"+this.state.backend_port+"/api/monitoring-time/get-average-h/"
                + payload.year + "/" + payload.month + "/" + payload.day + "/" + payload.hour, {
                method: 'GET',
                headers: {
                    "Content-type" : "application/json",
                }
              }).then(response => {
                if(response.ok) {
                  response.json().then(data => {
                    this.state.filter_time_average = data
                    this.state.is_data_loaded = true
                  })
                }
                else {                  
                    this.state.is_data_loaded = true
                    console.log(response)
                }
              })
        }
    },
    modules: {

    }
})