# Docker compose
 "docker-compose.yml" file should be placed in a directory above the cloned repository.  
 In that directory a "Docker images" subdirectory should be created, with the following structure:  
 "Docker images"  
 {  
	> python-sensor-air  
	{  
		// Contents of the "Sensor devices/Air quality sensor" folder from the repository plus a "air-quality-nis.csv" file containing data with the following format:  
			Sensor name, Latitude, Longitude, CO (mg/m^3), Non Metanic HydroCarbons (microg/m^3), Benzene (microg/m^3), NOx (ppb), NO2 (microg/m^3), Relative Humidity (%)  
		// Dataset used (and adapted): https://www.kaggle.com/shrutibhargava94/india-air-quality-data  
	{,  
	python-sensor-temp  
	{  
		python-sensor-temp-1,  
		python-sensor-temp-2,  
		python-sensor-temp-3,  
		python-sensor-temp-4,  
		python-sensor-temp-5,  
		python-sensor-temp-6,  
		python-sensor-temp-7,  
		python-sensor-temp-8  	  
		// Each subfolder should contain the contents of the "Sensor devices/Temperature sensor" folder from the repository plus a"road-weather-information-station.csv" file with the following format:  
			StationName, StationLocation, DateTime, RecordId, RoadSurfaceTemperature, AirTemperature  
		// Dataset used: https://www.kaggle.com/city-of-seattle/seattle-road-weather-information-stations  
	}  
 }  
 
