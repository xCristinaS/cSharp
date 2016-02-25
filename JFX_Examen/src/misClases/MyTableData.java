package misClases;

import javafx.beans.property.SimpleIntegerProperty;
import javafx.beans.property.SimpleStringProperty;

/**
 * Created by Cristina on 25/02/2016.
 */
public class MyTableData {

    SimpleStringProperty tituloPropuesta;
    SimpleIntegerProperty numeroVotos;

    public MyTableData(String titulo, int numeroVotos){
        tituloPropuesta = new SimpleStringProperty(titulo);
        this.numeroVotos = new SimpleIntegerProperty(numeroVotos);
    }

    public String getTituloPropuesta() {
        return tituloPropuesta.get();
    }

    public SimpleStringProperty tituloPropuestaProperty() {
        return tituloPropuesta;
    }

    public void setTituloPropuesta(String tituloPropuesta) {
        this.tituloPropuesta.set(tituloPropuesta);
    }

    public int getNumeroVotos() {
        return numeroVotos.get();
    }

    public SimpleIntegerProperty numeroVotosProperty() {
        return numeroVotos;
    }

    public void setNumeroVotos(int numeroVotos) {
        this.numeroVotos.set(numeroVotos);
    }
}
