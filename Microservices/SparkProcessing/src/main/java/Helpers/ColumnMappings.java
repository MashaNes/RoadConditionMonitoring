package Helpers;

import java.util.HashMap;
import java.util.Map;

public class ColumnMappings {
    private Map<String, String> generalTrafficMap;
    private Map<String, String> locationTrafficMap;
    
    public ColumnMappings(){
        generalTrafficMap = new HashMap<>();
        generalTrafficMap.put("paramName", "paramname");
        generalTrafficMap.put("value", "value");
        
        locationTrafficMap = new HashMap<>();
        locationTrafficMap.put("paramName", "paramname");
        locationTrafficMap.put("value", "value");
        locationTrafficMap.put("latitude", "latitude");
        locationTrafficMap.put("longitude", "longitude");
    }
    
    public Map<String, String> getGeneralTrafficMap(){
        return this.generalTrafficMap;
    }
    
    public Map<String, String> getLocationTrafficMap(){
        return this.locationTrafficMap;
    }
}
