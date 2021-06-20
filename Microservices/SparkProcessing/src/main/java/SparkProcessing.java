import Helpers.*;
import Entities.*;
import POI.*;
import com.datastax.spark.connector.japi.CassandraJavaUtil;
import static com.datastax.spark.connector.japi.CassandraStreamingJavaUtil.javaFunctions;
import org.apache.kafka.clients.consumer.ConsumerRecord;
import org.apache.kafka.common.serialization.StringDeserializer;
import org.apache.spark.SparkConf;
import org.apache.spark.streaming.Durations;
import org.apache.spark.streaming.api.java.*;
import org.apache.spark.streaming.kafka010.*;
import scala.Tuple2;


public class SparkProcessing {
    
    public static void main(String[] args)throws Exception{ 
            
        //Configs
        KafkaConfig kafkaConfig = new KafkaConfig(
                "192.168.0.26:9092",
                "spark_processing",
                "VehicleLocation",
                StringDeserializer.class,
                VehicleDataDeserializer.class,
                "earliest"
        );
        CassandraConfig cassandraConfig = new CassandraConfig(
                "traffic_data", 
                "general_traffic", 
                "location_traffic");
        ColumnMappings columnMappings = new ColumnMappings();
        
        
        //Initializing Spark
        SparkConf sparkConf = new SparkConf()
                              .setAppName("JavaDirectKafkaWordCount")
                              .set("spark.cassandra.connection.host", "192.168.0.26")
                              .set("spark.cassandra.connection.port", "9042")
                              .set("spark.cassandra.connection.keep_alive_ms", "10000");
        JavaStreamingContext jssc = new JavaStreamingContext(sparkConf, Durations.seconds(10));
        
        
        //Input stream from Kafka - (key, value) pairs
        JavaInputDStream<ConsumerRecord<String, VehicleData>> messages = KafkaUtils.createDirectStream(
                jssc,
                LocationStrategies.PreferConsistent(),
                ConsumerStrategies.Subscribe(kafkaConfig.getTopicsSet(), kafkaConfig.getKafkaParams()));
        messages.map(record -> (record.value().toString())).print();
        
        //Stream containing only message values
        JavaDStream<VehicleData> nonFilteredDataStream = messages.map(message -> message.value());
        
        
        //General data processing 
        //Streams containing speed data (total speed and info count) for 5 min windows calculated every minute
        JavaDStream<Double> SpeedStream = nonFilteredDataStream.map(data -> data.getVehicleSpeed());
        JavaDStream<GeneralTraffic> TotalSpeedStream = SpeedStream.reduceByWindow((a, b) -> a + b, Durations.seconds(300), Durations.seconds(60))
                                                                  .map(speed -> new GeneralTraffic("speed_total", speed));
            //Write total speed to Cassandra
        javaFunctions(TotalSpeedStream).writerBuilder(
                cassandraConfig.getKeyspace(), 
                cassandraConfig.getGeneralTable(), 
                CassandraJavaUtil.mapToRow(GeneralTraffic.class, columnMappings.getGeneralTrafficMap()))
                .saveToCassandra();
        JavaDStream<GeneralTraffic> CountSpeedStream = SpeedStream.count().reduceByWindow((a, b) -> a + b, Durations.seconds(300), Durations.seconds(60))
                                                                          .map(count -> new GeneralTraffic("speed_count", count));
            //Write speed info count to Cassandra
        javaFunctions(CountSpeedStream).writerBuilder(
                cassandraConfig.getKeyspace(), 
                cassandraConfig.getGeneralTable(), 
                CassandraJavaUtil.mapToRow(GeneralTraffic.class, columnMappings.getGeneralTrafficMap()))
                .saveToCassandra();
        
        //Stream containing unique elements for each VehicleId in a 5 min window, caltulated every minute
        JavaPairDStream<Integer,VehicleData> DataPairStream =  
                nonFilteredDataStream.mapToPair(data -> new Tuple2<Integer, VehicleData>(data.getVehicleId(), data))
                                     .reduceByKeyAndWindow((a, b) -> a, Durations.seconds(300), Durations.seconds(60));
        
        //Stream counting unique vehicles in each 5 minute long window, with window stride equal to 1 minute
        JavaDStream<GeneralTraffic> VehicleNumberStream = DataPairStream.count().map(number -> new GeneralTraffic("vehicle_number", number));
            //Write total number of vehicles to Cassandra
        javaFunctions(VehicleNumberStream).writerBuilder(
                cassandraConfig.getKeyspace(), 
                cassandraConfig.getGeneralTable(), 
                CassandraJavaUtil.mapToRow(GeneralTraffic.class, columnMappings.getGeneralTrafficMap()))
                .saveToCassandra();
        
        
        //Location related data processing
        POIListNis poiList = new POIListNis();
        
        //Get information for every point of interest from the list
        for(PointOfInterest poi : poiList.getPointsOfInterest())
        {
            //Stream containing only data for vehicles in the specified radius of the point of interest
            JavaDStream<VehicleData> DataInPOIStream = 
                    nonFilteredDataStream.filter(data -> GeoDistanceCalculator.isInPOI(data.getLatitude(), data.getLongitude(), poi));
            
            //Stream containing only speeds of filtered vehicles
            JavaDStream<Double> SpeedPOIStream = DataInPOIStream.map(data -> data.getVehicleSpeed());
            
            //Stream containing total speed of all vehicles in POI in the last 5 minutes, calculated every minute
            JavaDStream<LocationTraffic> TotalPOISpeedStream = 
                    SpeedPOIStream.reduceByWindow((a, b) -> a + b, Durations.seconds(300), Durations.seconds(60))
                                  .map(speed -> new LocationTraffic("speed_total", poi.getLatitude(), poi.getLongitude(), speed));
                //Write total speed in POI to Cassandra
            javaFunctions(TotalPOISpeedStream).writerBuilder(
                cassandraConfig.getKeyspace(), 
                cassandraConfig.getLocationTable(), 
                CassandraJavaUtil.mapToRow(LocationTraffic.class, columnMappings.getLocationTrafficMap()))
                .saveToCassandra();
            
            //Stream containing total speed info count for all vehicles in POI in the last 5 minutes, calculated every minute
            JavaDStream<LocationTraffic> CountPOISpeedStream = 
                    SpeedPOIStream.count().reduceByWindow((a, b) -> a + b, Durations.seconds(300), Durations.seconds(60))
                                          .map(count -> new LocationTraffic("speed_count", poi.getLatitude(), poi.getLongitude(), count));
                //Write total speed info count for POI to Cassandra
            javaFunctions(CountPOISpeedStream).writerBuilder(
                cassandraConfig.getKeyspace(), 
                cassandraConfig.getLocationTable(), 
                CassandraJavaUtil.mapToRow(LocationTraffic.class, columnMappings.getLocationTrafficMap()))
                .saveToCassandra();
            
            //Stream containing only one element for each vehicle id for a 5 minute window, with a 1 minute stride
            JavaPairDStream<Integer,VehicleData> DataPairPOIStream =  
                    DataInPOIStream.mapToPair(data -> new Tuple2<Integer, VehicleData>(data.getVehicleId(), data))
                                   .reduceByKeyAndWindow((a, b) -> a, Durations.seconds(300), Durations.seconds(60));
            
            //Stream contining number of vehicles in POI for a 5 minute window with a 1 minute stride
            JavaDStream<LocationTraffic> VehicleNumberPOIStream = 
                    DataPairPOIStream.count()
                                     .map(number -> new LocationTraffic("vehicle_number", poi.getLatitude(), poi.getLongitude(), number));
                //Write total number of vehicles in POI to Cassandra
            javaFunctions(VehicleNumberPOIStream).writerBuilder(
                cassandraConfig.getKeyspace(), 
                cassandraConfig.getLocationTable(), 
                CassandraJavaUtil.mapToRow(LocationTraffic.class, columnMappings.getLocationTrafficMap()))
                .saveToCassandra();
        }
        
        
        //Start streaming processing
        jssc.start();
        jssc.awaitTermination();
    }
}
