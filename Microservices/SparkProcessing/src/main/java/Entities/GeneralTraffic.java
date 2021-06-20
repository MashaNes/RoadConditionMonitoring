package Entities;

import com.fasterxml.jackson.annotation.JsonFormat;
import java.io.Serializable;

public class GeneralTraffic implements Serializable {
    @JsonFormat(shape = JsonFormat.Shape.STRING)
    private String paramName;
    @JsonFormat(shape = JsonFormat.Shape.NUMBER_FLOAT)
    private double value;
    
    public GeneralTraffic(String ParamName, double Value){
        this.paramName = ParamName;
        this.value = Value;
    }
    
    public String getParamName(){
        return this.paramName;
    }
    
    public double getValue(){
        return this.value;
    }
    
    public void setParamName(String paramName){
        this.paramName = paramName;
    }
    
    public void setValue(double value){
        this.value = value;
    }
    
    @Override
    public String toString(){
        return "GeneralTraffic [ParamName=" + paramName + ", Value=" + value + "]";
    }    
}
