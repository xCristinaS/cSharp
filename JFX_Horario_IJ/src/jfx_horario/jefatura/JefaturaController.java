/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package jfx_horario.jefatura;

import javafx.beans.value.ChangeListener;
import javafx.beans.value.ObservableValue;
import javafx.collections.FXCollections;
import javafx.collections.ObservableList;
import javafx.event.EventHandler;
import javafx.fxml.FXML;
import javafx.fxml.FXMLLoader;
import javafx.fxml.Initializable;
import javafx.scene.Parent;
import javafx.scene.Scene;
import javafx.scene.control.*;
import javafx.scene.control.cell.PropertyValueFactory;
import javafx.scene.image.Image;
import javafx.scene.image.ImageView;
import javafx.scene.input.*;
import javafx.scene.layout.Pane;
import javafx.scene.paint.Color;
import javafx.stage.Stage;
import javafx.util.Callback;
import misClases.BddConnection;
import misClases.Constantes;
import misClases.Horario;
import misClases.Tramos;

import java.io.IOException;
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
    @FXML
    private ImageView imgSalir;

    private ArrayList<String> myListaProfes = new ArrayList<String>();
    private ArrayList<Horario> datosCol = new ArrayList<>();
    private static Horario registroDePartidaDragDrop;
    private static char diaMovido;
    private double posX, posY;

    @Override
    public void initialize(URL url, ResourceBundle rb) {
        initViews();
    }

    private void initViews() {
        tHorario.setFixedCellSize(36);
        configImgSalir();
        configComboProfes();
        configRadioButtons();
        configDragAndDropColumnas();
        configTableHorario();
        comboProfes.getSelectionModel().select(0);
        lstHorario.visibleProperty().setValue(false);
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
        String select = "Select codProf, nombre from profesor where tipo = 0;";
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
            result.close();
            sentencia.close();
            conexion.close();
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
            result.close();
            sentencia.close();
            conexion.close();
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
        // Configuro el color del texto de la celda de la columna "hora"
        ((TableColumn) tHorario.getColumns().get(0)).setCellFactory(new Callback<TableColumn, TableCell>() {
            @Override
            public TableCell call(TableColumn param) {
                return new TableCell<Horario, String>() {
                    @Override
                    public void updateItem(String item, boolean empty) {
                        super.updateItem(item, empty);
                        if (!isEmpty()) {
                            this.setTextFill(Color.DARKBLUE);
                            setText(item);
                        }
                    }
                };
            }
        });
        // Objecto callback con el que configuro el color del texto de la celda de la tabla (de lunes a viernes).
        Callback<TableColumn, TableCell> myCallback = new Callback<TableColumn, TableCell>() {
            @Override
            public TableCell call(TableColumn param) {
                return new TableCell<Horario, String>() {
                    @Override
                    public void updateItem(String item, boolean empty) {
                        super.updateItem(item, empty);
                        if (!isEmpty()) {
                            if (item.contains("2") && item.contains("DAM"))
                                this.setTextFill(Color.DARKRED);
                            else if (item.contains("1") && item.contains("DAM"))
                                this.setTextFill(Color.DARKGOLDENROD);
                            else if (item.contains("1") && item.contains("SMR"))
                                this.setTextFill(Color.DARKGREEN);
                            else
                                this.setTextFill(Color.DARKVIOLET);
                            setText(item);
                        }
                    }
                };
            }
        };
        ((TableColumn) tHorario.getColumns().get(1)).setCellFactory(myCallback);
        ((TableColumn) tHorario.getColumns().get(2)).setCellFactory(myCallback);
        ((TableColumn) tHorario.getColumns().get(3)).setCellFactory(myCallback);
        ((TableColumn) tHorario.getColumns().get(4)).setCellFactory(myCallback);
        ((TableColumn) tHorario.getColumns().get(5)).setCellFactory(myCallback);
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

    private void configImgSalir() {
        imgSalir.setImage(new Image("@../../imagenes/logout.png"));
        imgSalir.setOnMouseClicked(new EventHandler<MouseEvent>() {
            @Override
            public void handle(MouseEvent event) {
                Pane root;
                try {
                    root = FXMLLoader.load(getClass().getResource("../login/login.fxml"));
                    String tituloWindow = "Login";
                    Stage stage = new Stage();
                    stage.setTitle(tituloWindow);
                    stage.setScene(new Scene(root));
                    stage.setResizable(false);
                    configDragDropWindow(root, stage);
                    stage.show();
                    ((Stage) imgSalir.getScene().getWindow()).close();
                } catch (IOException e) {
                    e.printStackTrace();
                }
            }
        });
    }

    private void configDragDropWindow(Parent root, Stage stage) {
        root.setOnMousePressed(event -> {
            posX = event.getX();
            posY = event.getY();
        });

        root.setOnMouseDragged(event -> {
            stage.setX(event.getScreenX() - posX);
            stage.setY(event.getScreenY() - posY);
        });
    }

    private void configDragAndDropColumnas() {
        // comienza el drag and drop:
        tHorario.setOnDragDetected(new EventHandler<MouseEvent>() {
            @Override
            public void handle(MouseEvent event) {
                Dragboard db = tHorario.startDragAndDrop(TransferMode.MOVE);
                ClipboardContent content = new ClipboardContent();

                TablePosition t = tHorario.getSelectionModel().getSelectedCells().get(0);
                TableColumn columna = tHorario.getColumns().get(t.getColumn());
                int numeroFila = t.getRow();
                Horario registro = datosCol.get(numeroFila);

                JefaturaController.registroDePartidaDragDrop = registro;

                switch (columna.getText()) {
                    case "Hora":
                        //content.putString(registro.getTramo());
                        content.putString("");
                        break;
                    case "Lunes":
                        content.putString(registro.getLunes());
                        diaMovido = 'L';
                        break;
                    case "Martes":
                        content.putString(registro.getMartes());
                        diaMovido = 'M';
                        break;
                    case "Miercoles":
                        content.putString(registro.getMiercoles());
                        diaMovido = 'X';
                        break;
                    case "Jueves":
                        content.putString(registro.getJueves());
                        diaMovido = 'J';
                        break;
                    case "Viernes":
                        content.putString(registro.getViernes());
                        diaMovido = 'V';
                        break;
                }
                db.setContent(content);
                event.consume();
            }
        });

        tHorario.setOnDragOver(new EventHandler<DragEvent>() {
            public void handle(DragEvent event) {
                if (event.getDragboard().hasString())
                    event.acceptTransferModes(TransferMode.MOVE);

                event.consume();
            }
        });

        tHorario.setOnDragDropped(new EventHandler<DragEvent>() {
            public void handle(DragEvent event) {
                final double HEIGHT_CONTENIDO_TABLA = tHorario.getFixedCellSize() * tHorario.getColumns().size();
                final double ALTO_CABECERA = tHorario.getHeight() - HEIGHT_CONTENIDO_TABLA + 4;
                final double ALTO_CELDA = tHorario.getFixedCellSize();

                int columna, fila;
                Dragboard db = event.getDragboard();
                boolean success = false;
                Horario registroFila;
                String dragContent;

                if (db.hasString()) {
                    dragContent = db.getString();
                    columna = (int) (event.getSceneX() - 23) / Constantes.ANCHO_CELDA; // le tengo que restar 23 por el margen, para que me pille bien el número de columna.
                    fila = (int) ((event.getSceneY() - ALTO_CABECERA) / ALTO_CELDA) - 3; // Le resto 3 porque hay una especie de desfase extraño, para q la fila empiece a contar en 0.
                    System.out.println(String.format("fila: %d - columna: %d", fila, columna));
                    if (!dragContent.equals("") && fila >= 0 && columna > 0) { // entro si la fila se corresponde con algún registro y no con la cabecera de la tabla y la columna no es la del tramo horario.
                        registroFila = datosCol.get(fila);
                        if (celdaValidaToDrop(columna, registroFila, dragContent)) {
                            success = true;
                        }
                    }
                }
                event.setDropCompleted(success);
                event.consume();
            }
        });

        tHorario.setOnDragDone(new EventHandler<DragEvent>() {
            public void handle(DragEvent event) {
                if (event.getTransferMode() == TransferMode.MOVE) {
                    switch (diaMovido){
                        case 'L':
                            JefaturaController.registroDePartidaDragDrop.setLunes("");
                            break;
                        case 'M':
                            JefaturaController.registroDePartidaDragDrop.setMartes("");
                            break;
                        case 'X':
                            JefaturaController.registroDePartidaDragDrop.setMiercoles("");
                            break;
                        case 'J':
                            JefaturaController.registroDePartidaDragDrop.setJueves("");
                            break;
                        case 'V':
                            JefaturaController.registroDePartidaDragDrop.setViernes("");
                            break;
                    }

                    tHorario.getItems().removeAll(datosCol);
                    tHorario.getItems().setAll(datosCol);
                }
                event.consume();
            }
        });
    }

    private boolean celdaValidaToDrop(int columna, Horario registro, String dragContent){
        boolean resp = false;
        switch(columna){
            case 1:
                if (registro.getLunes().equals("")) {
                    registro.setLunes(dragContent);
                    resp = true;
                }
                break;
            case 2:
                if (registro.getMartes().equals("")) {
                    registro.setMartes(dragContent);
                    resp = true;
                }
                break;
            case 3:
                if (registro.getMiercoles().equals("")) {
                    registro.setMiercoles(dragContent);
                    resp = true;
                }
                break;
            case 4:
                if (registro.getJueves().equals("")) {
                    registro.setJueves(dragContent);
                    resp = true;
                }
                break;
            case 5:
                if (registro.getViernes().equals("")) {
                    registro.setViernes(dragContent);
                    resp = true;
                }
                break;
        }
        return resp;
    }
}
