package Entities;

import java.io.Serializable;

public class LocationTraffic implements Serializable {
    private String paramName;
    private double latitude;
    private double longitude;
    private double radius;
    private double value;
    
    public LocationTraffic(String ParamName, double Latitude, double Longitude, double Radius, double Value){
        this.paramName = ParamName;
        this.latitude = Latitude;
        this.longitude = Longitude;
        this.radius = Radius;
        this.value = Value;
    }
    
    public String getParamName(){
        return this.paramName;
    }
    
    public double getLatitude(){
        return this.latitude;
    }
    
    public double getLongitude(){
        return this.longitude;
    }
    
    public double getRadius(){
        return this.radius;
    }
    
    public double getValue(){
        return this.value;
    }
    
    public void setParamName(String paramName){
        this.paramName = paramName;
    }
    
    public void setLatitude(double latitude) {
        this.latitude = latitude;
    }
    
    public void setLongitude(double longitude) {
        this.longitude = longitude;
    }
    
    public void setRadius(double radius) {
        this.radius = radius;
    }
    
    public void setValue(double value) {
        this.value = value;
    }
    
    @Override
    public String toString(){
        return "LocationTraffic [ParamName=" + paramName +", Latitude" + latitude + ", Longitude" + longitude + ", Radius=" + radius +", Value=" + value + "]";
    }
}
