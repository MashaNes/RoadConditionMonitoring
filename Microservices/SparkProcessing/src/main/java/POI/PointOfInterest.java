package POI;

import java.io.Serializable;

public class PointOfInterest implements Serializable{
    private double Latitude;
    private double Longitude;
    private double Radius; //meters
    
    public PointOfInterest(double Longitude, double Latitude, double Radius){
        this.Latitude = Latitude;
        this.Longitude = Longitude;
        this.Radius = Radius;
    }
    
    public double getLatitude(){
        return this.Latitude;
    }
    
    public double getLongitude() {
        return this.Longitude;
    }
    
    public double getRadius() {
        return this.Radius;
    }
    
    @Override
    public String toString(){
        return "Point of interest [Latitude=" + Latitude + ", Longitude=" + Longitude + ", Radius=" + Radius + "]";
    }
}
