package POI;

import java.util.ArrayList;
import java.util.List;

public class POIListNis {
    private List<PointOfInterest> pointsOfInterest;
    
    public POIListNis() {
        pointsOfInterest = new ArrayList<>();
        pointsOfInterest.add(new PointOfInterest(21.890834822332234, 43.31819537151684, 300)); //Trg vojske
        pointsOfInterest.add(new PointOfInterest(21.899353517471642, 43.31842954558688, 150)); //Dusanov Bazar
        pointsOfInterest.add(new PointOfInterest(21.925046422092148, 43.32949464086066, 200)); //Knjazevacka
        pointsOfInterest.add(new PointOfInterest(21.923540883346845, 43.32184406443057, 175)); //Duvaniste
        pointsOfInterest.add(new PointOfInterest(21.927664677400305, 43.311596073555826, 200)); //Trosarina
        pointsOfInterest.add(new PointOfInterest(21.89837420985546, 43.31307870141127, 130)); //Palilulska rampa
        pointsOfInterest.add(new PointOfInterest(21.88300421009964, 43.31586130799601, 250)); //Apelacioni sud
        pointsOfInterest.add(new PointOfInterest(21.898686849499292, 43.32078720561783, 150)); //Glavna posta
        pointsOfInterest.add(new PointOfInterest(21.916133208615616, 43.320654543037996, 160)); //Bulevarska pijaca
        pointsOfInterest.add(new PointOfInterest(21.894836762456187, 43.3232483863597, 200)); //Banovina
    }
    
    public List<PointOfInterest> getPointsOfInterest(){
        return this.pointsOfInterest;
    }
}
