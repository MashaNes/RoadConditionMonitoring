package POI;

public class GeoDistanceCalculator {
    
    //Haversine Formula - meters
    public static double GetDistance(double lat1, double long1, double lat2, double long2) {
        final int r = 6371;
	Double latDistance = Math.toRadians(lat2 - lat1);
	Double lonDistance = Math.toRadians(long2 - long1);
	Double a = Math.sin(latDistance / 2) 
                   * Math.sin(latDistance / 2) + Math.cos(Math.toRadians(lat1))
                   * Math.cos(Math.toRadians(lat2)) * Math.sin(lonDistance / 2)
                   * Math.sin(lonDistance / 2);
	Double c = 2 * Math.atan2(Math.sqrt(a), Math.sqrt(1 - a));
	double distance = r * c;	
	return distance * 1000;
    }
    
    public static boolean isInPOI(double Latitude, double Longitude, PointOfInterest poi) {
        return GetDistance(Latitude, Longitude, poi.getLatitude(), poi.getLongitude()) < poi.getRadius();
    }
}
