# Vehicle dashboard
![vehicle dashboard image](https://github.com/MashaNes/RoadConditionMonitoring/blob/main/Web%20clients/Screenshots/Vehicle%20dashboard/full_app.png)
 <br/>
 This dashboard provides information regarding the surroundings of a single vehicle. 
 <br/> 
 <br/>
![vehicle id](https://github.com/MashaNes/RoadConditionMonitoring/blob/main/Web%20clients/Screenshots/Vehicle%20dashboard/vehicle_id.png)
 <br/>
 The user enters the id of the vehicle they wish to track on the home page of the app and they are taken to a separate web page where all the information is available. 
 The information presented on the page refreshes every 5 seconds. The information consists of:  
 &ensp;&ensp;&ensp;* The location of the vehicle - shown on the map with a car pin  
 &ensp;&ensp;&ensp;* The location of the environmental sensors containing data about the road and air temperature and air quality - shown on the map with leaf pins  
 &ensp;&ensp;&ensp;* The location and coverage area of traffic related data - shown on the map with traffic pins and surrounding circles  
 <br/> 
 <br/>
 ![vehicle data](https://github.com/MashaNes/RoadConditionMonitoring/blob/main/Web%20clients/Screenshots/Vehicle%20dashboard/vehicle_data.png)
 <br/>
 By hovering over the vehicle location pin, you get the information about the vehicle's last known speed and the timestamp stating when the information was acquired.  
 <br/> 
 <br/>
 ![environment data](https://github.com/MashaNes/RoadConditionMonitoring/blob/main/Web%20clients/Screenshots/Vehicle%20dashboard/environment_data.png)
 <br/>
 The environmental pins show the location of sensor collecting temperature and air quality data. Information from close sensors is aggregated. Depending on the information available,
 by hovering over a leaf pin, you get a Temperature or Air quality section, or both, like shown above.  
 <br/> 
 <br/>
 ![traffic data](https://github.com/MashaNes/RoadConditionMonitoring/blob/main/Web%20clients/Screenshots/Vehicle%20dashboard/traffic_data.png)
 <br/>
 The traffic pins show the available information about the state of thraffic at certain points of interest. Every point of interest has a radius presented on the map as a surrounding circle.
 By hovering over the traffic pin, you get current information about the number of vehicles in the marked area and their average speed.  
 <br/> 
 <br/>
 ![controls](https://github.com/MashaNes/RoadConditionMonitoring/blob/main/Web%20clients/Screenshots/Vehicle%20dashboard/controls.png)
 <br/>
 The environmental and traffic data are collected from an area of a specified radius. The user can control the value of the radius through a control at the top of the page.
 Additionally, the user can control which information do they want the map to display through the checkboxes in the above image. An example of a filtered map is bellow.  
 <br/>
 ![filtered map](https://github.com/MashaNes/RoadConditionMonitoring/blob/main/Web%20clients/Screenshots/Vehicle%20dashboard/filter.png)
 <br/>
 # Full dashboard
 ![full dashboard image](https://github.com/MashaNes/RoadConditionMonitoring/blob/main/Web%20clients/Screenshots/Full%20dashboard/homepage.png)
 <br/>
 This dashboard provides live full data representation and data filtering based on location and timeframe parameters.
 The navbar contains three items:  
 &ensp;&ensp;&ensp;* RoadMonitor - leeds to the homepage of the app where a user can get live data from their group of interest   
 &ensp;&ensp;&ensp;* Location filtering - leeds to a page where a user can filter data based on the location where it was collected from  
 &ensp;&ensp;&ensp;* Timeframe filtering - leeds to a page where a user can filter data based on when it was collected  
 <br/>
 ### Home page
 ![homepage](https://github.com/MashaNes/RoadConditionMonitoring/blob/main/Web%20clients/Screenshots/Full%20dashboard/homepage_with_general.png)
 <br/>
 The homepage initially shows newest available data from all environmental sensors and all available location based traffic data. Also, beneath the map, information about the total number of vehicles and their average speed is shown. 
 All data on this page is being automatically refreshed every time the system gains new information.  
 <br/>
 <br/>
 ![homepage search](https://github.com/MashaNes/RoadConditionMonitoring/blob/main/Web%20clients/Screenshots/Full%20dashboard/homepage_search.png)
 <br/>
 At the top of the page, the user can select which type of environmental data do they want displayed on the map. The average data is for the current day/hour.  
 <br/>
 <br/>
 ![filter](https://github.com/MashaNes/RoadConditionMonitoring/blob/main/Web%20clients/Screenshots/Full%20dashboard/homepage_filter.png)
 <br/>
 Also, the user can filter which data do they want the map to show, for a clearer view.  
 <br/>
 ### Location filtering page
 ![location filter page](https://github.com/MashaNes/RoadConditionMonitoring/blob/main/Web%20clients/Screenshots/Full%20dashboard/location_filter.png)
 <br/>
 On this page, the user can filter information by the location where it was acquired. By clicking on the map they define a reference point, and by entering the desired radius in the search bar
 at the top of the page, they specify the area of interest. After selecting the other search parameters and clicking "Search", they get data exclusively collected from the area of interest.  
 <br/>
 <br/>
 ![search_options](https://github.com/MashaNes/RoadConditionMonitoring/blob/main/Web%20clients/Screenshots/Full%20dashboard/search_options.png)
 <br/>
 The user can select whether or not they want to get traffic data and which type of environmental data from the selected area do they want to get.  
 <br/>
 <br/>
 ![selected hour/day](https://github.com/MashaNes/RoadConditionMonitoring/blob/main/Web%20clients/Screenshots/Full%20dashboard/search_per_hour.png)
 <br/>
 If they choose an option to get average data from an houd/day which is not current, they are prompted to enter the desired day/hpur.  
 <br/>
 <br/>
 ![reference number](https://github.com/MashaNes/RoadConditionMonitoring/blob/main/Web%20clients/Screenshots/Full%20dashboard/reference_number.png)
 <br/>
 If they select the "All" option, that means that each marker on the map represents multiple instances of data collected from it. The markers show reference numbers and the data is displayed
 in two tables beneath the map, where each row contains a number in the first column referencing a pin on the map where it was collected.  
 <br/>
 ![tables](https://github.com/MashaNes/RoadConditionMonitoring/blob/main/Web%20clients/Screenshots/Full%20dashboard/tables.png)
 <br/>
 ### Timeframe filtering page
 ![timeframe page](https://github.com/MashaNes/RoadConditionMonitoring/blob/main/Web%20clients/Screenshots/Full%20dashboard/time_search_options.png)
 <br/>
 This page is used for non-geographically, solely timeframe related searches. Search options are shown above. If the "Timeframe" option is chosen, the user is prompted to
 select the date and time from the picker (like it is shown bellow), and enter the number of second framing the period of interest. The results are displayed on the map in the similar manner like previously described.  
 <br/>
 ![select date](https://github.com/MashaNes/RoadConditionMonitoring/blob/main/Web%20clients/Screenshots/Full%20dashboard/timeframe_search.png)  
  