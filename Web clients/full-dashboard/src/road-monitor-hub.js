import { HubConnectionBuilder, LogLevel } from '@aspnet/signalr'

export default {
    install (Vue) { 
      const roadMonitorHub = new Vue()
      Vue.prototype.$roadMonitorHub = roadMonitorHub
  
      let connection = null
      let startedPromise = null
      let manuallyClosed = false
  
      Vue.prototype.startSignalR = () => {
        connection = new HubConnectionBuilder()
          .withUrl("http://192.168.0.26:49164/road-monitor-hub")
          .configureLogging(LogLevel.Debug)
          .build()
  
        
        addSignalREventListener('new_newest')
        addSignalREventListener('new_averageH')
        addSignalREventListener('new_averageDay')
        addSignalREventListener('new_traffic')
  
        function addSignalREventListener(name) {
          connection.on(name, (payload) => {
            roadMonitorHub.$emit(name, payload)
          })
        }
  
        function start () {
          startedPromise = connection.start()
            .catch(err => {
              console.error('Failed to connect with hub', err)
              return new Promise((resolve, reject) => setTimeout(() => start().then(resolve).catch(reject), 5000))
            })
          return startedPromise
        }
  
        connection.onclose(() => {
          if (!manuallyClosed) start()
        })
  
        manuallyClosed = false
        start()
      }
  
      Vue.prototype.stopSignalR = () => {
        if (!startedPromise) return
  
        manuallyClosed = true
        return startedPromise
          .then(() => connection.stop())
          .then(() => { startedPromise = null })
      }
  
      roadMonitorHub.JoinNewestGroup = () => {
        if (!startedPromise) return
  
        return startedPromise
          .then(() => connection.invoke('JoinNewestGroup'))
          .catch(console.error)
      }
  
      roadMonitorHub.JoinAverageHGroup = () => {
        if (!startedPromise) return
  
        return startedPromise
          .then(() => connection.invoke('JoinAverageHGroup'))
          .catch(console.error)
      }
  
      roadMonitorHub.JoinAverageDayGroup = () => {
        if (!startedPromise) return
  
        return startedPromise
          .then(() => connection.invoke('JoinAverageDayGroup'))
          .catch(console.error)
      }
  
      roadMonitorHub.JoinTrafficGroup = () => {
        if (!startedPromise) return
  
        return startedPromise
          .then(() => connection.invoke('JoinTrafficGroup'))
          .catch(console.error)
      }
  
      roadMonitorHub.LeaveNewestGroup = () => {
        if(!startedPromise) return
  
        return startedPromise
        .then(() => connection.invoke('LeaveNewestGroup'))
        .catch(console.error)
      }
  
      roadMonitorHub.LeaveAverageHGroup = () => {
        if(!startedPromise) return
  
        return startedPromise
        .then(() => connection.invoke('LeaveAverageHGroup'))
        .catch(console.error)
      }
  
      roadMonitorHub.LeaveAverageDayGroup = () => {
        if(!startedPromise) return
  
        return startedPromise
        .then(() => connection.invoke('LeaveAverageDayGroup'))
        .catch(console.error)
      }
  
      roadMonitorHub.LeaveTrafficGroup = () => {
        if(!startedPromise) return
  
        return startedPromise
        .then(() => connection.invoke('LeaveTrafficGroup'))
        .catch(console.error)
      }
    }
  }