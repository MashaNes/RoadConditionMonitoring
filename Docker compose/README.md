# Docker compose
 "docker-compose.yml" file should be placed in a directory above the cloned repository. In that directory a "Docker images" subdirectory should be created, with the following structure:  
 <br/>
 "Docker images"  
 {  
	&ensp;&ensp;&ensp;python-sensor-air  
	&ensp;&ensp;&ensp;{  
		&ensp;&ensp;&ensp;&ensp;&ensp;&ensp;// Contents of the "Sensor devices/Air quality sensor" folder from the repository plus a "air-quality-nis.csv" file containing data with the following format:  
			&ensp;&ensp;&ensp;&ensp;&ensp;&ensp;&ensp;&ensp;&ensp;Sensor name, Latitude, Longitude, CO (mg/m^3), Non Metanic HydroCarbons (microg/m^3), Benzene (microg/m^3), NOx (ppb), NO2 (microg/m^3), Relative Humidity (%)  
		&ensp;&ensp;&ensp;&ensp;&ensp;&ensp;// Dataset used (and adapted): https://www.kaggle.com/citrahsagala/airquality  
	&ensp;&ensp;&ensp;},  
	&ensp;&ensp;&ensp;python-sensor-combined 
	&ensp;&ensp;&ensp;{  
		&ensp;&ensp;&ensp;&ensp;&ensp;&ensp;// Contents of the "Sensor devices/Air quality and temperature sensor" folder from the repository plus a "nis-complete.csv" file containing data with the following format:  
			&ensp;&ensp;&ensp;&ensp;&ensp;&ensp;&ensp;&ensp;&ensp;Station name, RecordId, RoadSurfaceTemperature, AirTemperature, Latitude, Longitude, CO (mg/m^3), Non Metanic HydroCarbons (microg/m^3), Benzene (microg/m^3), NOx (ppb), NO2 (microg/m^3), Relative Humidity (%)  
		&ensp;&ensp;&ensp;&ensp;&ensp;&ensp;// Datasets used (adapted and combined): https://www.kaggle.com/citrahsagala/airquality and https://www.kaggle.com/city-of-seattle/seattle-road-weather-information-stations  
	&ensp;&ensp;&ensp;},  
	&ensp;&ensp;&ensp;python-sensor-temp  
	&ensp;&ensp;&ensp;{  
		&ensp;&ensp;&ensp;&ensp;&ensp;&ensp;python-sensor-temp-1,  
		&ensp;&ensp;&ensp;&ensp;&ensp;&ensp;python-sensor-temp-2,  
		&ensp;&ensp;&ensp;&ensp;&ensp;&ensp;...  
		&ensp;&ensp;&ensp;&ensp;&ensp;&ensp;python-sensor-temp-n  
		<br/>
		&ensp;&ensp;&ensp;&ensp;&ensp;&ensp;// Each subfolder should contain the contents of the "Sensor devices/Temperature sensor" folder from the repository plus a"road-weather-information-station.csv" file with the following format:  
			&ensp;&ensp;&ensp;&ensp;&ensp;&ensp;&ensp;&ensp;&ensp;StationName, StationLocation, DateTime, RecordId, RoadSurfaceTemperature, AirTemperature  
		&ensp;&ensp;&ensp;&ensp;&ensp;&ensp;// Dataset used: https://www.kaggle.com/city-of-seattle/seattle-road-weather-information-stations  
	&ensp;&ensp;&ensp;}  
	&ensp;&ensp;&ensp;python-sensor-vehicle  
	&ensp;&ensp;&ensp;{  
	&ensp;&ensp;&ensp;&ensp;&ensp;&ensp;// Contents of the "Sensor devices/Vehicle sensor" folder from the repository plus a "nis-trajectories.csv" file containing data with the following format:  
	&ensp;&ensp;&ensp;&ensp;&ensp;&ensp;&ensp;&ensp;&ensp;timestep_time, vehicle_angle, vehicle_id, vehicle_lane, vehicle_pos, vehicle_slope, vehicle_speed, vehicle_type, vehicle_x, vehicle_y  
	&ensp;&ensp;&ensp;&ensp;&ensp;&ensp;// SUMO simulator used (https://www.eclipse.org/sumo/), with FCD output (https://sumo.dlr.de/docs/Simulation/Output/FCDOutput.html)  
	&ensp;&ensp;&ensp;}  
 }  
 <br/>
 Kafka instance located on localhost:29092 should have topics "Temperature" and "AirQuality" with no key and string value and "VehicleLocation" with string key and string value  
 &ensp;&ensp;&ensp;Connection can be initialized through Kafka GUI tool - Offset Explorer (https://kafkatool.com/download.html)  
 &ensp;&ensp;&ensp;&ensp;&ensp;&ensp; - Zookeeper Port: 22181  
 &ensp;&ensp;&ensp;&ensp;&ensp;&ensp; - Zookeeper Host: localhost  
 &ensp;&ensp;&ensp;&ensp;&ensp;&ensp; - Advanced/Bootstrap servers: localhost:29092  
 <br/>
 cassandra1 should have the script from repo/Cassandra/Temperature.txt and repo/Cassandra/Traffic.txt executed in cqlsh  
 cassandra2 should have the script from repo/Cassandra/AirQuality.txt and repo/Cassandra/VehicleLocation.txt executed in cqlsh  
 It would be best for each microservice to have a seperate cassandra instance, but due to the lack of available RAM it is possible to bundle them up together (like here)  
 <br/>
 Run "docker compose build" and "docker compose up" in the directory where "docker-compose.yml" is located  
 Edit the ip address in repo/Microservices/SparkProcessing/src/main/java/SparkProcessing.java to fit your private ip address and follow the instructions at repo/Microservices/SparkProcessing/start.txt to start the Spark instance  
 