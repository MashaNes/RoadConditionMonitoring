# Vehicle dashboard
![vehicle dashboard image](https://github.com/MashaNes/RoadConditionMonitoring/blob/main/Web%20clients/Screenshots/Vehicle%20gateway/full_app.png)
 <br/>
 This dashboard provides information regarding the surroundings of a single vehicle. 
 <br/> 
 <br/>
![vehicle id](https://github.com/MashaNes/RoadConditionMonitoring/blob/main/Web%20clients/Screenshots/Vehicle%20gateway/vehicle_id.png)
 <br/>
 The user enters the id of the vehicle they wish to track on the home page of the app and they are taken to a separate web page where all the information is available. 
 The information presented on the page refreshes every 5 seconds. The information consists of:  
 &ensp;&ensp;&ensp;* The location of the vehicle - shown on the map with a car pin  
 &ensp;&ensp;&ensp;* The location of the environmental sensors containing data about the road and air temperature and air quality - shown on the map with leaf pins  
 &ensp;&ensp;&ensp;* The location and coverage area of traffic related data - shown on the map with traffic pins and surrounding circles  
 <br/> 
 <br/>
 ![vehicle data](https://github.com/MashaNes/RoadConditionMonitoring/blob/main/Web%20clients/Screenshots/Vehicle%20gateway/vehicle_data.png)
 <br/>
 By hovering over the vehicle location pin, you get the information about the vehicle's last known speed and the timestamp stating when the information was acquired.  
 <br/> 
 <br/>
 ![environment data](https://github.com/MashaNes/RoadConditionMonitoring/blob/main/Web%20clients/Screenshots/Vehicle%20gateway/environment_data.png)
 <br/>
 The environmental pins show the location of sensor collecting temperature and air quality data. Information from close sensors is aggregated. Depending on the information available,
 by hovering over a leaf pin, you get a Temperature or Air quality section, or both, like shown above.  
 <br/> 
 <br/>
 ![traffic data](https://github.com/MashaNes/RoadConditionMonitoring/blob/main/Web%20clients/Screenshots/Vehicle%20gateway/traffic_data.png)
 <br/>
 The traffic pins show the available information about the state of thraffic at certain points of interest. Every point of interest has a radius presented on the map as a surrounding circle.
 By hovering over the traffic pin, you get current information about the number of vehicles in the marked area and their average speed.  
 <br/> 
 <br/>
 ![controls](https://github.com/MashaNes/RoadConditionMonitoring/blob/main/Web%20clients/Screenshots/Vehicle%20gateway/controls.png)
 <br/>
 The environmental and traffic data are collected from an area of a specified radius. The user can control the value of the radius through a control at the top of the page.
 Additionally, the user can control which information do they want the map to display through the checkboxes in the above image. An example of a filtered map is bellow.  
 <br/>
 ![filtered map](https://github.com/MashaNes/RoadConditionMonitoring/blob/main/Web%20clients/Screenshots/Vehicle%20gateway/filter.png)
 