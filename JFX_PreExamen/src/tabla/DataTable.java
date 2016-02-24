package tabla;

import javafx.beans.property.SimpleStringProperty;

/**
 * Created by Cristina on 24/02/2016.
 */
public class DataTable {

    private SimpleStringProperty columna1;
    private SimpleStringProperty columna2;

    public DataTable(String columna1, String columna2){
        this.columna1 = new SimpleStringProperty(columna1);
        this.columna2 = new SimpleStringProperty(columna2);
    }

    public String getColumn1(){
        return columna1.get();
    }

    public void setColumna1(String columna1){
        this.columna1.set(columna1);
    }

    public String getColumn2(){
        return columna2.get();
    }

    public void setColumna2(String columna2){
        this.columna2.set(columna2);
    }

    @Override
    public String toString() {
        return "DataTable{" +
                "columna1=" + columna1 +
                ", columna2=" + columna2 +
                '}';
    }

    public String getColumna1() {
        return columna1.get();
    }

    public SimpleStringProperty columna1Property() {
        return columna1;
    }

    public String getColumna2() {
        return columna2.get();
    }

    public SimpleStringProperty columna2Property() {
        return columna2;
    }
}
