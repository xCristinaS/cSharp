/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package misClases;

import javafx.beans.property.SimpleStringProperty;

/**
 *
 * @author Cristina
 */
public class Horario {
    private SimpleStringProperty tramo;
    private SimpleStringProperty lunes;
    private SimpleStringProperty martes;
    private SimpleStringProperty miercoles;
    private SimpleStringProperty jueves;
    private SimpleStringProperty viernes;
    
    public Horario(String tramo){
        this(tramo, " ", " ", " ", " ", " ");
    }
    
    public Horario(String tramo, String lunes, String martes, String miercoles, String jueves, String viernes){
        this.tramo = new SimpleStringProperty(tramo);
        this.lunes = new SimpleStringProperty(lunes);
        this.martes = new SimpleStringProperty(martes);
        this.miercoles = new SimpleStringProperty(miercoles);
        this.jueves = new SimpleStringProperty(jueves);
        this.viernes = new SimpleStringProperty(viernes);
    }

    public String getTramo() {
        return tramo.get();
    }

    public void setTramo(String tramo) {
        this.tramo.set(tramo);
    }

    public String getLunes() {
        return lunes.get();
    }

    public void setLunes(String lunes) {
        this.lunes.set(lunes);
    }

    public String getMartes() {
        return martes.get();
    }

    public void setMartes(String martes) {
        this.martes.set(martes);
    }

    public String getMiercoles() {
        return miercoles.get();
    }

    public void setMiercoles(String miercoles) {
        this.miercoles.set(miercoles);
    }
    
        public String getJueves() {
        return jueves.get();
    }

    public void setJueves(String jueves) {
        this.miercoles.set(jueves);
    }
    
        public String getViernes() {
        return miercoles.get();
    }

    public void setViernes(String viernes) {
        this.miercoles.set(viernes);
    }
}
