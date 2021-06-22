# RoadConditionMonitoring
 This repository holds a microservice and IoTS oriented system for monitoring road conditions.  
 <br/>
 The architecture of the system is shown bellow:  
 <br/>
![architecture image](https://github.com/MashaNes/RoadConditionMonitoring/blob/main/Architecture.png)  
<br/>
There are three types of environment sensors - air and road temeprature sensor (Farenheit), air quality sensor and a combined sensor measuring both the temeprature of the road and air in Ferenheit and 
the air quality. 
There is also a set of sensors connected to vehicles, providing the system with information about the location and speed of all connected vehicles.  
<br/>
<b><ins>Acqusition gateway</ins></b> collects data from environment sensors, converts the temepratures to Celsius and splits all acquired data to streams of air quality related data and temperature related data. Those streams are fed to the corresponding topics in Kafka.  
<br/>
<b><ins>Vehicle gateway</ins></b> collects data from vehicle related sensors, converts the speed to km/h and sends the data to a corresponding Kafka topic.  
<br/>
<b><ins>Temperature microservice</ins></b> and <b><ins>Air quality microservice</ins></b> consume the temperature and air quality related data respectively.
Each microservice stores the consumed data in order to contain information about newest data for all locations, average data per hour and per day for all locations and also all consumed data.
Both microservices offer APIs for filtering stored data based on location and time related parameters.  
<br/>
<b><ins>Vehicle location microservice</ins></b> consumes all data from the VehicleLocation topic from Kafka and keeps record of the last known location for each vehicle. It provides an API for reading that information.  
<br/>
<b><ins>Spark processing</ins></b> also comsumes all data from the VehicleLocation Kafka topic and uses it to calculate a set of values for a 5 minute long timeframe, recomputed every minute.
It computes the number of vehicles on the road, the average speed of all vehicles on the road , and looks at a set of points of interest and calculates the number of 
vehicles and the average speed for an area surrounding those points, giving an insight into the current traffic situation. All that data is stored in the database.  
<br/>
<b><ins>Traffic microservice</ins></b> has access to the database where the Spark instance is writing into and provides an API for filtering that data.  
<br/>
<b><ins>Monitoring gateway</ins></b> is an entry point for clients wanting general road condition information. It provides an API for filtering data based on many parameters both regarding the environmental factors
and regarding traffic. It also offers a SignalR connection to clients wishing to periodically get packages of fresh data regarding the road conditions.  
<br/>
<b><ins>Vehicle monitoring gateway</ins></b> is an entry point for clients wanting the information about road conditions in an area surrounding a certain vehicle.
It provides an API for getting relevant information based on a provided vehicle id and a radius of the area of interest.  
<br/>
The visual representation and functionality of the web dashboards can be seen in the "Web clients" folder  
