package Helpers;

import Entities.VehicleData;
import com.fasterxml.jackson.databind.ObjectMapper;
import org.apache.kafka.common.serialization.Deserializer;

public class VehicleDataDeserializer implements Deserializer<VehicleData>{
    
    private static ObjectMapper objectMapper = new ObjectMapper();
    
    @Override
    public VehicleData deserialize(String string, byte[] bytes) {
        try
        {
            return objectMapper.readValue(bytes, VehicleData.class);
        }
        catch(Exception e) { 
            e.printStackTrace();
        }
        return null;
    }
    
}
