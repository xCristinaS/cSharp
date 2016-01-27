/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package jfx_horario.jefatura;

import java.net.URL;
import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Date;
import java.util.ResourceBundle;
import java.util.logging.Level;
import java.util.logging.Logger;

import javafx.beans.value.ChangeListener;
import javafx.beans.value.ObservableValue;
import javafx.collections.FXCollections;
import javafx.collections.ObservableList;
import javafx.fxml.FXML;
import javafx.fxml.Initializable;
import javafx.scene.control.ComboBox;
import javafx.scene.control.ListView;
import javafx.scene.control.RadioButton;
import javafx.scene.control.TableColumn;
import javafx.scene.control.TableView;
import javafx.scene.control.Toggle;
import javafx.scene.control.ToggleGroup;
import javafx.scene.control.cell.PropertyValueFactory;
import javafx.stage.Window;
import misClases.BddConnection;
import misClases.Horario;
import misClases.Constantes;
import misClases.Tramos;

/**
 * @author Cristina
 */
public class JefaturaController implements Initializable {

    @FXML
    private ListView lstHorario;
    @FXML
    private ComboBox comboProfes;
    @FXML
    private ToggleGroup rbGrupo;
    @FXML
    private TableView<Horario> tHorario;

    private ArrayList<String> myListaProfes = new ArrayList<String>();
    private ArrayList<Horario> datosCol = new ArrayList<>();

    @Override
    public void initialize(URL url, ResourceBundle rb) {
        initViews();
    }

    private void initViews() {
        configComboProfes();
        configRadioButtons();
        configTableHorario();
        tHorario.visibleProperty().setValue(false);
    }

    private void configRadioButtons() {
        rbGrupo.selectedToggleProperty().addListener(new ChangeListener<Toggle>() {
            @Override
            public void changed(ObservableValue<? extends Toggle> observable, Toggle oldValue, Toggle newValue) {
                if (((RadioButton) newValue).getText().equals(Constantes.H_SEMANAL)) { // Si el radioButton seleccionado es el horario semanal
                    lstHorario.visibleProperty().setValue(false); // oculto el listView del horario del día
                    tHorario.visibleProperty().setValue(true); // muestro el tableView del horario semanal
                } else { // si no
                    lstHorario.visibleProperty().setValue(true); // muestro el horario del día
                    tHorario.visibleProperty().setValue(false); // oculto el horario semanal
                }
            }
        });
    }

    private void configComboProfes() {
        cargarProfes();
        comboProfes.getSelectionModel().selectedIndexProperty().addListener(new ChangeListener<Number>() {
            @Override
            public void changed(ObservableValue<? extends Number> observable, Number oldValue, Number newValue) {
                cargarHorarioDiaProfe(myListaProfes.get(newValue.intValue()).substring(0, 3)); // cargo el horario diario del profesor.
                if (!datosCol.isEmpty())
                    for (Horario h : datosCol) // limpio las columnas del horario del profesor anterior.
                        h.vaciar();
                cargarHorarioSemanalProfe(myListaProfes.get(newValue.intValue()).substring(0, 3)); // cargo el horario semanal del profesor.
            }
        });
    }

    private void cargarProfes() {
        Connection conexion = BddConnection.newConexionMySQL("horario");
        String select = "Select codProf, nombre from profesor;";
        PreparedStatement sentencia;
        ResultSet result;
        ObservableList<String> listaChoiceBox = FXCollections.observableArrayList();
        try {
            sentencia = conexion.prepareStatement(select);
            result = sentencia.executeQuery();
            while (result.next()) {
                myListaProfes.add(result.getString(1) + " - " + result.getString(2)); // agrego los nombres a mi lista
            }
            listaChoiceBox.addAll(myListaProfes); // agrego la lista de nombres a la lista que gestiona el choiceBox
            comboProfes.setItems(listaChoiceBox);
        } catch (SQLException ex) {
            Logger.getLogger(JefaturaController.class.getName()).log(Level.SEVERE, null, ex);
        }
    }

    private void cargarHorarioDiaProfe(String profe) {
        Connection conexion = BddConnection.newConexionMySQL("horario");
        SimpleDateFormat formato = new SimpleDateFormat("EEE");
        String select = "select codTramo, h.codCurso, h.codOe, h.codAsignatura, a.nombre from horario h, reparto r, asignatura a where h.codAsignatura = a.codAsignatura and r.codOe = h.codOe and r.codcurso = h.codcurso and r.CodAsignatura = h.CodAsignatura and codProf = ? and codTramo like '" + ((formato.format(new Date())).charAt(0) + "").toUpperCase() + "%' order by codtramo;";
        PreparedStatement sentencia;
        ResultSet result;
        lstHorario.getItems().clear(); // limpio la lista con el contenido del profesor anterior
        try {
            sentencia = conexion.prepareStatement(select);
            sentencia.setString(1, profe);
            result = sentencia.executeQuery();
            while (result.next()) { // voy agregando cada registro a la lista
                lstHorario.getItems().add(String.format("Tramo horario: %s - Curso: %s %s - Código asignatura: %s - Nombre asingnatura: %s", dameTramo(result.getString(1).charAt(1)), result.getString(2), result.getString(3), result.getString(4), result.getString(5)));
            }
        } catch (SQLException ex) {
            Logger.getLogger(JefaturaController.class.getName()).log(Level.SEVERE, null, ex);
        }
    }

    private String dameTramo(char hora) {
        switch (hora) {
            case '1':
                return Tramos.PRIMERA.getTramo_H();
            case '2':
                return Tramos.SEGUNDA.getTramo_H();
            case '3':
                return Tramos.TERCERA.getTramo_H();
            case '4':
                return Tramos.CUARTA.getTramo_H();
            case '5':
                return Tramos.QUINTA.getTramo_H();
            case '6':
                return Tramos.SEXTA.getTramo_H();
            default:
                return "";
        }
    }

    private void configTableHorario() {
        ((TableColumn) tHorario.getColumns().get(0)).setCellValueFactory(new PropertyValueFactory<Horario, String>("tramo"));
        ((TableColumn) tHorario.getColumns().get(1)).setCellValueFactory(new PropertyValueFactory<Horario, String>("lunes"));
        ((TableColumn) tHorario.getColumns().get(2)).setCellValueFactory(new PropertyValueFactory<Horario, String>("martes"));
        ((TableColumn) tHorario.getColumns().get(3)).setCellValueFactory(new PropertyValueFactory<Horario, String>("miercoles"));
        ((TableColumn) tHorario.getColumns().get(4)).setCellValueFactory(new PropertyValueFactory<Horario, String>("jueves"));
        ((TableColumn) tHorario.getColumns().get(5)).setCellValueFactory(new PropertyValueFactory<Horario, String>("viernes"));
        tHorario.setItems(FXCollections.observableArrayList(datosCol));
        tHorario.setEditable(true);
        agregarTramosATable();
    }

    private void agregarTramosATable() {
        datosCol.add(new Horario(Tramos.PRIMERA.getTramo_H()));
        datosCol.add(new Horario(Tramos.SEGUNDA.getTramo_H()));
        datosCol.add(new Horario(Tramos.TERCERA.getTramo_H()));
        datosCol.add(new Horario(Tramos.CUARTA.getTramo_H()));
        datosCol.add(new Horario(Tramos.QUINTA.getTramo_H()));
        datosCol.add(new Horario(Tramos.SEXTA.getTramo_H()));
    }

    private void cargarHorarioSemanalProfe(String profe) {
        Connection conexion = BddConnection.newConexionMySQL("horario");
        PreparedStatement sentencia;
        ResultSet result;
        String select = "select codTramo, h.codCurso, h.codOe, h.codAsignatura from horario h, reparto r, asignatura a where h.codAsignatura = a.codAsignatura and "
                + " r.codOe = h.codOe and r.codcurso = h.codcurso and r.CodAsignatura = h.CodAsignatura and codProf = ? order by substring(codTramo, 1, 2) like'L%' desc, "
                + "substring(codTramo, 1, 2) like'M%' desc, substring(codTramo, 1, 2) like'X%' desc, codtramo;";

        try {
            sentencia = conexion.prepareStatement(select);
            sentencia.setString(1, profe);
            result = sentencia.executeQuery();
            while (result.next())
                montarAsignaturaEnColumna(result.getString(1), result.getString(2) + " - " + result.getString(3), result.getString(4));

            result.close();
            sentencia.close();
            conexion.close();
        } catch (SQLException ex) {
            Logger.getLogger(JefaturaController.class.getName()).log(Level.SEVERE, null, ex);
        }
    }

    private void montarAsignaturaEnColumna(String tramo, String curso, String codAsignatura) {
        char columna = tramo.charAt(0);
        int hora = Integer.parseInt(String.valueOf(tramo.charAt(1)));
        Horario h = datosCol.get(hora - 1);
        switch (columna) {
            case 'L':
                h.setLunes(String.format("%s (%s)", codAsignatura, curso));
                break;
            case 'M':
                h.setMartes(String.format("%s (%s)", codAsignatura, curso));
                break;
            case 'X':
                h.setMiercoles(String.format("%s (%s)", codAsignatura, curso));
                break;
            case 'J':
                h.setJueves(String.format("%s (%s)", codAsignatura, curso));
                break;
            case 'V':
                h.setViernes(String.format("%s (%s)", codAsignatura, curso));
                break;
            default:
                break;
        }
        tHorario.getItems().setAll(datosCol);
    }
}
