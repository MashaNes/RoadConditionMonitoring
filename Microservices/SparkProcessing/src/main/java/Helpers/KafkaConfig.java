package Helpers;

import java.util.HashMap;
import java.util.HashSet;
import java.util.Map;
import java.util.Set;
import org.apache.kafka.clients.consumer.ConsumerConfig;

public class KafkaConfig {
    
    private Map<String, Object> kafkaParams;
    private Set<String> topicsSet;
    
    public KafkaConfig(String brokers, String groupId, String topic, Class keyDeserializer, Class valueDeserializer, String offsetReset){
        this.kafkaParams = new HashMap<>();
        kafkaParams.put(ConsumerConfig.BOOTSTRAP_SERVERS_CONFIG, brokers);
        kafkaParams.put(ConsumerConfig.GROUP_ID_CONFIG, groupId);
        kafkaParams.put(ConsumerConfig.KEY_DESERIALIZER_CLASS_CONFIG, keyDeserializer);
        kafkaParams.put(ConsumerConfig.VALUE_DESERIALIZER_CLASS_CONFIG, valueDeserializer);
        kafkaParams.put(ConsumerConfig.AUTO_OFFSET_RESET_CONFIG, offsetReset);
        
        this.topicsSet = new HashSet<>();
        topicsSet.add(topic);
    }
    
    public Map<String, Object> getKafkaParams(){
        return this.kafkaParams;
    }
    
    public Set<String> getTopicsSet(){
        return this.topicsSet;
    }
}
