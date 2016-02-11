/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package jfx_horario.jefatura;

import com.sun.xml.internal.bind.v2.runtime.reflect.opt.Const;
import javafx.beans.value.ChangeListener;
import javafx.beans.value.ObservableValue;
import javafx.collections.FXCollections;
import javafx.collections.ObservableList;
import javafx.event.ActionEvent;
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
import jfx_horario.insertUpdate.InsertUpdateFormController;
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
import java.util.Optional;
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

    private ArrayList<String> myListaProfes = new ArrayList<String>(), myListaClasesDia = new ArrayList<String>();
    private ArrayList<Horario> datosCol = new ArrayList<>();
    private static Horario registroDePartidaDragDrop;
    private static char diaMovido;
    private double posX, posY;
    private static int filaSelectedRightButton, columnSelectedRightButton;
    private static String insertUpdateCodAsig, inserUpdateCodCurso, inserUpdateCodOe;
    private static boolean datosRecogidos;

    @Override
    public void initialize(URL url, ResourceBundle rb) {
        initViews();
    }

    private void initViews() {
        tHorario.setFixedCellSize(36);
        configImgSalir();
        configComboProfes();
        configRadioButtons();
        configDragAndDropTabla();
        configTableHorario();
        configContextMenuTable();
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
                    cargarHorarioDiaProfe(myListaProfes.get(comboProfes.getSelectionModel().getSelectedIndex()).substring(0,3));
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
                refrescarTabla();
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
        myListaClasesDia.clear();
        try {
            sentencia = conexion.prepareStatement(select);
            sentencia.setString(1, profe);
            result = sentencia.executeQuery();
            while (result.next()) { // voy agregando cada registro a la lista
                myListaClasesDia.add(String.format("Tramo horario: %s - Curso: %s %s - Código asignatura: %s - Nombre asingnatura: %s", dameTramo(result.getString(1).charAt(1)), result.getString(2), result.getString(3), result.getString(4), result.getString(5)));
            }
            lstHorario.getItems().setAll(myListaClasesDia);
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

    private void configDragAndDropTabla() {
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
                int columna, fila;
                Dragboard db = event.getDragboard();
                boolean success = false;
                Horario registroFila;
                String dragContent;

                if (db.hasString()) {
                    dragContent = db.getString();
                    columna = obtenerColumna(event.getSceneX());
                    fila = obtenerFila(event.getSceneY());
                    if (!dragContent.equals("") && fila >= 0 && columna > 0) { // entro si la fila se corresponde con algún registro y no con la cabecera de la tabla y la columna no es la del tramo horario.
                        registroFila = datosCol.get(fila);
                        if (celdaValidaToDrop(columna, registroFila, dragContent)) {
                            agregarRegistroABDD(registroFila, obtenerDiaSegunColumna(columna));
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
                    eliminarRegistroDeBDD(JefaturaController.registroDePartidaDragDrop, diaMovido);
                    eliminarRegistroDeLista(JefaturaController.registroDePartidaDragDrop, diaMovido);
                    refrescarTabla();
                }
                event.consume();
            }
        });
    }

    private int obtenerColumna(double posicionX) {
        return (int) (posicionX - 23) / Constantes.ANCHO_CELDA; // le tengo que restar 23 por el margen, para que me pille bien el número de columna.
    }

    private int obtenerFila(double posicionY) {
        final double HEIGHT_CONTENIDO_TABLA = tHorario.getFixedCellSize() * tHorario.getColumns().size();
        final double ALTO_CABECERA = tHorario.getHeight() - HEIGHT_CONTENIDO_TABLA + 4;
        final double ALTO_CELDA = tHorario.getFixedCellSize();
        return (int) ((posicionY - ALTO_CABECERA) / ALTO_CELDA) - 3; // Le resto 3 porque hay una especie de desfase extraño, para q la fila empiece a contar en 0.
    }

    private char obtenerDiaSegunColumna(int columna) {
        char diaColumna;
        if (columna == 1)
            diaColumna = 'L';
        else if (columna == 2)
            diaColumna = 'M';
        else if (columna == 3)
            diaColumna = 'X';
        else if (columna == 4)
            diaColumna = 'J';
        else
            diaColumna = 'V';
        return diaColumna;
    }

    private boolean celdaValidaToDrop(int columna, Horario registro, String dragContent) {
        boolean resp = false;
        switch (columna) {
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

    private void eliminarRegistroDeBDD(Horario registro, char diaTramo) {
        String delete, contenidoRegistro = obtenerContenidoDeRegistro(registro, diaTramo), codOe, codCurso, codAsignatura, codTramo;
        codOe = contenidoRegistro.substring(contenidoRegistro.indexOf("-") + 2, contenidoRegistro.length() - 1);
        codCurso = contenidoRegistro.substring(contenidoRegistro.indexOf("(") + 1, contenidoRegistro.indexOf("-"));
        codAsignatura = contenidoRegistro.substring(0, contenidoRegistro.indexOf("("));
        codTramo = diaTramo + getHoraDeTramo(registro.getTramo());
        delete = "delete from horario where CodOe='" + codOe + "' and CodCurso='" + codCurso + "' and CodAsignatura='" + codAsignatura + "' and CodTramo ='" + codTramo + "';";
        ejecutarOrdenSQL(delete);
    }

    private void eliminarRegistroDeLista(Horario registro, char diaTramo) {
        switch (diaTramo) {
            case 'L':
                registro.setLunes("");
                break;
            case 'M':
                registro.setMartes("");
                break;
            case 'X':
                registro.setMiercoles("");
                break;
            case 'J':
                registro.setJueves("");
                break;
            case 'V':
                registro.setViernes("");
                break;
        }
    }

    private void agregarRegistroABDD(Horario nuevoRegistro, char diaTramo) {
        String insert, contenidoRegistro = obtenerContenidoDeRegistro(nuevoRegistro, diaTramo), codOe, codCurso, codAsignatura, codTramo;
        Connection conexion = BddConnection.newConexionMySQL("horario");
        PreparedStatement sentencia;
        codOe = contenidoRegistro.substring(contenidoRegistro.indexOf("-") + 2, contenidoRegistro.length() - 1);
        codCurso = contenidoRegistro.substring(contenidoRegistro.indexOf("(") + 1, contenidoRegistro.indexOf("-"));
        codAsignatura = contenidoRegistro.substring(0, contenidoRegistro.indexOf("("));
        codTramo = diaTramo + getHoraDeTramo(nuevoRegistro.getTramo());
        insert = "insert into horario values('" + codTramo + "', '" + codOe + "', '" + codCurso + "', '" + codAsignatura + "');";
        try {
            sentencia = conexion.prepareStatement(insert);
            sentencia.execute();
            sentencia.close();
            conexion.close();
        } catch (SQLException e) {
            e.printStackTrace();
        }
    }

    private String obtenerContenidoDeRegistro(Horario registro, char dia) {
        String contenidoRegistro = "";
        switch (dia) {
            case 'L':
                contenidoRegistro = registro.getLunes();
                break;
            case 'M':
                contenidoRegistro = registro.getMartes();
                break;
            case 'X':
                contenidoRegistro = registro.getMiercoles();
                break;
            case 'J':
                contenidoRegistro = registro.getJueves();
                break;
            case 'V':
                contenidoRegistro = registro.getViernes();
                break;
        }
        return contenidoRegistro;
    }

    private String getHoraDeTramo(String tramo) {
        int resp = 0;
        if (tramo.equals(Tramos.PRIMERA.getTramo_H()))
            resp = 1;
        else if (tramo.equals(Tramos.SEGUNDA.getTramo_H()))
            resp = 2;
        else if (tramo.equals(Tramos.TERCERA.getTramo_H()))
            resp = 3;
        else if (tramo.equals(Tramos.CUARTA.getTramo_H()))
            resp = 4;
        else if (tramo.equals(Tramos.QUINTA.getTramo_H()))
            resp = 5;
        else if (tramo.equals(Tramos.SEXTA.getTramo_H()))
            resp = 6;

        return String.valueOf(resp);
    }

    private void configContextMenuTable() {
        ContextMenu contextMenu = new ContextMenu();
        MenuItem insertar = new MenuItem("Insertar");
        MenuItem actualizar = new MenuItem("Actualizar");
        MenuItem eliminar = new MenuItem("Eliminar");

        tHorario.setOnMouseClicked(new EventHandler<MouseEvent>() {
            @Override
            public void handle(MouseEvent event) {
                if (event.getButton() == MouseButton.SECONDARY) {
                    JefaturaController.filaSelectedRightButton = obtenerFila(event.getSceneY());
                    JefaturaController.columnSelectedRightButton = obtenerColumna(event.getSceneX());
                    contextMenu.getItems().removeAll(insertar, eliminar, actualizar);
                    if (celdaValidaToDrop(columnSelectedRightButton, datosCol.get(filaSelectedRightButton), ""))
                        contextMenu.getItems().setAll(insertar);
                    else
                        contextMenu.getItems().setAll(actualizar,eliminar);
                }
            }
        });

        insertar.setOnAction(new EventHandler<ActionEvent>() {
            @Override
            public void handle(ActionEvent event) {
                try {
                    if (celdaValidaToDrop(columnSelectedRightButton, datosCol.get(filaSelectedRightButton), "")) {
                        lanzarVentanaInsertUpdate("Insertar");
                        if (datosRecogidos) {
                            insertarNuevoRegistro();
                            insertarOActualizarRegistroEnTabla(datosCol.get(filaSelectedRightButton), obtenerDiaSegunColumna(columnSelectedRightButton), String.format("%s (%s - %s)", insertUpdateCodAsig, inserUpdateCodCurso, inserUpdateCodOe));
                            refrescarTabla();
                        }
                        datosRecogidos = false;
                    }
                } catch (IOException e) {
                    e.printStackTrace();
                }
            }
        });

        actualizar.setOnAction(new EventHandler<ActionEvent>() {
            @Override
            public void handle(ActionEvent event) {
                try {
                    if (!celdaValidaToDrop(columnSelectedRightButton, datosCol.get(filaSelectedRightButton), "")) {
                        lanzarVentanaInsertUpdate("Actualizar");
                        if (datosRecogidos) {
                            actualizarRegistro();
                            insertarOActualizarRegistroEnTabla(datosCol.get(filaSelectedRightButton), obtenerDiaSegunColumna(columnSelectedRightButton), String.format("%s (%s - %s)", insertUpdateCodAsig, inserUpdateCodCurso, inserUpdateCodOe));
                            refrescarTabla();
                        }
                        datosRecogidos = false;
                    }
                } catch (IOException e) {
                    e.printStackTrace();
                }
            }
        });

        eliminar.setOnAction(new EventHandler<ActionEvent>() {
            @Override
            public void handle(ActionEvent event) {
                if (!celdaValidaToDrop(columnSelectedRightButton, datosCol.get(filaSelectedRightButton), "")) {
                    Alert alert = new Alert(Alert.AlertType.CONFIRMATION);
                    alert.setTitle("¡Advertencia!");
                    alert.setHeaderText("Se va a eliminar un registro");
                    alert.setContentText("¿Está seguro de que desea eliminar el registro definitivamente?");
                    Optional<ButtonType> result = alert.showAndWait();
                    if (result.get() == ButtonType.OK) {
                        eliminarRegistroDeBDD(datosCol.get(filaSelectedRightButton), obtenerDiaSegunColumna(columnSelectedRightButton));
                        eliminarRegistroDeLista(datosCol.get(filaSelectedRightButton), obtenerDiaSegunColumna(columnSelectedRightButton));
                        refrescarTabla();
                    }
                }
            }
        });
        contextMenu.getItems().addAll(insertar, actualizar, eliminar);
        tHorario.setContextMenu(contextMenu);
    }

    private void lanzarVentanaInsertUpdate(String titulo) throws IOException {
        Parent root = null;
        Stage stage;
        FXMLLoader loader = new FXMLLoader(getClass().getResource(("../insertUpdate/insertUpdate.fxml")));
        loader.setController(new InsertUpdateFormController(myListaProfes.get(comboProfes.getSelectionModel().getSelectedIndex()).substring(0, 3)));
        root = loader.load();
        stage = new Stage();
        stage.setTitle(titulo);
        stage.setScene(new Scene(root));
        stage.setResizable(false);
        configDragDropWindow(root, stage);
        stage.showAndWait();
    }

    private void refrescarTabla() {
        tHorario.getItems().removeAll(datosCol);
        tHorario.getItems().setAll(datosCol);
    }

    public static void callBack_RecogerDatosFormUpdateInsert(String codAsignatura, String codCurso, String codOe) {
        insertUpdateCodAsig = codAsignatura;
        inserUpdateCodCurso = codCurso;
        inserUpdateCodOe = codOe;
        datosRecogidos = true;
    }

    public void insertarNuevoRegistro() {
        String codTramo = obtenerDiaSegunColumna(columnSelectedRightButton) + getHoraDeTramo(datosCol.get(filaSelectedRightButton).getTramo());
        String insert = "insert into horario values ('" + codTramo + "', '" + inserUpdateCodOe + "', '" + inserUpdateCodCurso + "', '" + insertUpdateCodAsig + "');";
        ejecutarOrdenSQL(insert);
    }

    public void actualizarRegistro() {
        String codTramo = obtenerDiaSegunColumna(columnSelectedRightButton) + getHoraDeTramo(datosCol.get(filaSelectedRightButton).getTramo());
        String contenidoRegistro = obtenerContenidoDeRegistro(datosCol.get(filaSelectedRightButton), obtenerDiaSegunColumna(columnSelectedRightButton)), codOe, codCurso, codAsignatura;
        codOe = contenidoRegistro.substring(contenidoRegistro.indexOf("-") + 2, contenidoRegistro.length() - 1);
        codCurso = contenidoRegistro.substring(contenidoRegistro.indexOf("(") + 1, contenidoRegistro.indexOf("-"));
        codAsignatura = contenidoRegistro.substring(0, contenidoRegistro.indexOf("("));
        String update = String.format("update horario set codOe = '%s', codCurso='%s', codAsignatura='%s' where codTramo = '%s' and codOe = '%s' and codCurso= '%s' and codAsignatura = '%s'",
                inserUpdateCodOe, inserUpdateCodCurso, insertUpdateCodAsig, codTramo, codOe, codCurso, codAsignatura);
        ejecutarOrdenSQL(update);
    }

    private void ejecutarOrdenSQL(String orden) {
        Connection conexion = BddConnection.newConexionMySQL("horario");
        try {
            PreparedStatement sentencia = conexion.prepareStatement(orden);
            sentencia.execute();
            sentencia.close();
            conexion.close();
        } catch (SQLException e) {
            e.printStackTrace();
        }
    }

    private void insertarOActualizarRegistroEnTabla(Horario registro, char diaTramo, String contenido) {
        switch (diaTramo) {
            case 'L':
                registro.setLunes(contenido);
                break;
            case 'M':
                registro.setMartes(contenido);
                ;
                break;
            case 'X':
                registro.setMiercoles(contenido);
                break;
            case 'J':
                registro.setJueves(contenido);
                break;
            case 'V':
                registro.setViernes(contenido);
                break;
        }
    }
}
