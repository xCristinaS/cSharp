/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package misClases;

/**
 *
 * @author Cristina
 */
public enum Tramos {
    PRIMERA("08:15 - 09:15"), SEGUNDA("09:15 - 10:15"), TERCERA("10:15 - 11:15"), CUARTA("11:45 - 12:45"), QUINTA("12:45 - 13:45"), SEXTA("13:45 - 14:45");
    private final String TRAMO_H;

    Tramos(String TRAMO_H) {
        this.TRAMO_H = TRAMO_H;
    }

    public String getTramo_H() {
        return TRAMO_H;
    }
}
