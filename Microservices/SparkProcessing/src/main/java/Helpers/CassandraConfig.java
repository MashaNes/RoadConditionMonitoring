package Helpers;

public class CassandraConfig {
    private String keyspace;
    private String generalTable;
    private String locationTable;
    
    public CassandraConfig(String keyspace, String generalTable, String locationTable){
        this.keyspace = keyspace;
        this.generalTable = generalTable;
        this.locationTable = locationTable;
    }
    
    public String getKeyspace() {
        return this.keyspace;
    }
    
    public String getGeneralTable() {
        return this.generalTable;
    }
    
    public String getLocationTable() {
        return this.locationTable;
    }
}
