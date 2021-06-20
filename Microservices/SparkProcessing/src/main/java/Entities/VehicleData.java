package Entities;

import com.fasterxml.jackson.annotation.JsonFormat;
import java.io.Serializable;

public class VehicleData implements Serializable{    
    @JsonFormat(shape = JsonFormat.Shape.NUMBER_INT)
    private int VehicleId;  
    @JsonFormat(shape = JsonFormat.Shape.NUMBER_FLOAT)
    private double VehicleSpeed;   
    @JsonFormat(shape = JsonFormat.Shape.NUMBER_FLOAT)
    private double Latitude;
    @JsonFormat(shape = JsonFormat.Shape.NUMBER_FLOAT)
    private double Longitude;
    @JsonFormat(shape = JsonFormat.Shape.STRING)
    private String Timestamp;
    
    public int getVehicleId() {
        return VehicleId;
    }
    
    public double getVehicleSpeed(){
        return VehicleSpeed;
    }
    
    public double getLatitude(){
        return Latitude;
    }
    
    public double getLongitude(){
        return Longitude;
    }
    
    public String getTimestamp(){
        return Timestamp;
    }
    
    @Override
    public String toString(){
        return "Vehicle [VehicleId=" + VehicleId + ", VehicleSpeed=" + VehicleSpeed + ", Latitude=" + Latitude + ", Longitude=" + Longitude
                + ", Timestamp=" + Timestamp + "]";
    }
}