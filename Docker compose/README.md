# Docker compose
 "docker-compose.yml" file should be placed in a directory above the cloned repository. In that directory a "Docker images" subdirectory should be created, with the following structure:  
 <br/>
 "Docker images"  
 {  
	&ensp;&ensp;&ensp;python-sensor-air  
	&ensp;&ensp;&ensp;{  
		&ensp;&ensp;&ensp;&ensp;&ensp;&ensp;// Contents of the "Sensor devices/Air quality sensor" folder from the repository plus a "air-quality-nis.csv" file containing data with the following format:  
			&ensp;&ensp;&ensp;&ensp;&ensp;&ensp;&ensp;&ensp;&ensp;Sensor name, Latitude, Longitude, CO (mg/m^3), Non Metanic HydroCarbons (microg/m^3), Benzene (microg/m^3), NOx (ppb), NO2 (microg/m^3), Relative Humidity (%)  
		&ensp;&ensp;&ensp;&ensp;&ensp;&ensp;// Dataset used (and adapted): https://www.kaggle.com/shrutibhargava94/india-air-quality-data  
	&ensp;&ensp;&ensp;},  
	&ensp;&ensp;&ensp;python-sensor-temp  
	&ensp;&ensp;&ensp;{  
		&ensp;&ensp;&ensp;&ensp;&ensp;&ensp;python-sensor-temp-1,  
		&ensp;&ensp;&ensp;&ensp;&ensp;&ensp;python-sensor-temp-2,  
		&ensp;&ensp;&ensp;&ensp;&ensp;&ensp;python-sensor-temp-3,  
		&ensp;&ensp;&ensp;&ensp;&ensp;&ensp;python-sensor-temp-4,  
		&ensp;&ensp;&ensp;&ensp;&ensp;&ensp;python-sensor-temp-5,  
		&ensp;&ensp;&ensp;&ensp;&ensp;&ensp;python-sensor-temp-6,  
		&ensp;&ensp;&ensp;&ensp;&ensp;&ensp;python-sensor-temp-7,  
		&ensp;&ensp;&ensp;&ensp;&ensp;&ensp;python-sensor-temp-8  
		<br/>
		&ensp;&ensp;&ensp;&ensp;&ensp;&ensp;// Each subfolder should contain the contents of the "Sensor devices/Temperature sensor" folder from the repository plus a"road-weather-information-station.csv" file with the following format:  
			&ensp;&ensp;&ensp;&ensp;&ensp;&ensp;&ensp;&ensp;&ensp;StationName, StationLocation, DateTime, RecordId, RoadSurfaceTemperature, AirTemperature  
		&ensp;&ensp;&ensp;&ensp;&ensp;&ensp;// Dataset used: https://www.kaggle.com/city-of-seattle/seattle-road-weather-information-stations  
	&ensp;&ensp;&ensp;}  
 }  
 <br/>
 Kafka instance located on localhost:29092 should have topics "Temperature" and "AirQuality" with no key and string value  
 &ensp;&ensp;&ensp;Connection can be initialized through Kafka GUI tool - Offset Explorer (https://kafkatool.com/download.html)  
 &ensp;&ensp;&ensp;&ensp;&ensp;&ensp; - Zookeeper Port 22181  
 &ensp;&ensp;&ensp;&ensp;&ensp;&ensp; - Zookeeper Host localhost  
 &ensp;&ensp;&ensp;&ensp;&ensp;&ensp; - Advanced/Bootstrap servers localhost:29092  
 <br/>
 Cassandra instance located on localhost:9043 should have the script from repo/Cassandra/Temperature.txt executed in cqlsh  
 