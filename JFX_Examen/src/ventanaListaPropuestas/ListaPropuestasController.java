package ventanaListaPropuestas;

import javafx.beans.value.ChangeListener;
import javafx.beans.value.ObservableValue;
import javafx.event.EventHandler;
import javafx.fxml.FXML;
import javafx.fxml.Initializable;
import javafx.scene.control.Alert;
import javafx.scene.control.Label;
import javafx.scene.control.ListView;
import javafx.scene.control.TextArea;
import javafx.scene.input.MouseEvent;
import javafx.stage.Stage;
import main.Main;
import utilidad.BddConnection;
import ventanaNuevaPropuesta.NuevaPropuestaController;

import java.net.URL;
import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ResourceBundle;

public class ListaPropuestasController implements Initializable {

    @FXML
    ListView lstPropuestas;
    @FXML
    Label btnNuevaProp, btnVotarProp, btnVerVotaciones;
    @FXML
    TextArea txtDetalles;

    private String usuario;
    private String propuestaSeleccionada = "";

    public ListaPropuestasController(String usuario) {
        this.usuario = usuario;
    }

    @Override
    public void initialize(URL location, ResourceBundle resources) {
        configListView();
        cargarPropuestasEnListView();
        if (!lstPropuestas.getItems().isEmpty())
            propuestaSeleccionada = lstPropuestas.getItems().get(0).toString();
        else
            propuestaSeleccionada = "";

        if (!usuarioTieneVotosDisponibles())
            btnVotarProp.setVisible(false);

        configBotones();
        cargarDetallesProp();
    }

    private void configListView() {
        lstPropuestas.getSelectionModel().selectedIndexProperty().addListener(new ChangeListener<Number>() {
            @Override
            public void changed(ObservableValue<? extends Number> observable, Number oldValue, Number newValue) {
                if (!lstPropuestas.getItems().isEmpty()) {
                    propuestaSeleccionada = lstPropuestas.getItems().get(newValue.intValue()).toString();
                    cargarDetallesProp();
                }
            }
        });
    }

    private void cargarPropuestasEnListView() {
        String select = String.format("select titulo from propuesta where id not in (select propuesta from votaciones where usuario = '%s')", usuario);
        Connection conexion = BddConnection.newConexionMySQL("cristina_examen");
        PreparedStatement sentencia;
        ResultSet result;

        try {
            sentencia = conexion.prepareStatement(select);
            result = sentencia.executeQuery();
            lstPropuestas.getItems().clear();
            while (result.next())
                lstPropuestas.getItems().add(result.getString(1));
            result.close();
            sentencia.close();
            conexion.close();
        } catch (SQLException e) {
            e.printStackTrace();
        }
    }

    private void configBotones() {
        btnNuevaProp.setOnMouseClicked(new EventHandler<MouseEvent>() {
            @Override
            public void handle(MouseEvent event) {
                Stage stage = Main.lanzarVentana("Nueva Propuesta", getClass().getResource("../ventanaNuevaPropuesta/nueva_propuesta.fxml"), new NuevaPropuestaController(usuario));
                stage.setOnHiding(event1 -> {
                    Main.lanzarVentana("Lista de propuestas", getClass().getResource("../ventanaListaPropuestas/lista_propuestas.fxml"), new ListaPropuestasController(usuario)).show();
                });
                stage.show();
                ((Stage)btnVotarProp.getScene().getWindow()).close();
            }
        });

        btnVotarProp.setOnMouseClicked(new EventHandler<MouseEvent>() {
            @Override
            public void handle(MouseEvent event) {
                if (usuarioTieneVotosDisponibles())
                    votarPropuesta();
                else {
                    btnVotarProp.setVisible(false);
                }
            }
        });

        btnVerVotaciones.setOnMouseClicked(new EventHandler<MouseEvent>() {
            @Override
            public void handle(MouseEvent event) {
                Stage stage = Main.lanzarVentana("Estadísticas", getClass().getResource("../ventanaEstadisticas/estadisticas.fxml"), null);
                stage.setOnHiding(event1 -> {
                    Main.lanzarVentana("Lista de propuestas", getClass().getResource("../ventanaListaPropuestas/lista_propuestas.fxml"), new ListaPropuestasController(usuario)).show();
                });
                stage.show();
                ((Stage)btnVotarProp.getScene().getWindow()).close();
            }
        });
    }

    private void cargarDetallesProp() {
        String select = String.format("select * from propuesta where titulo = '%s'", propuestaSeleccionada), detalles = "";
        Connection conexion = BddConnection.newConexionMySQL("cristina_examen");
        PreparedStatement sentencia;
        ResultSet result;
        try {
            sentencia = conexion.prepareStatement(select);
            result = sentencia.executeQuery();
            if (result.next())
                detalles += String.format("Datos de la propuesta\n\nID de propuesta: %d\nRealizada por usuario: %s\n\n%s\n\n%s", result.getInt(1), result.getString(4), result.getString(2).toUpperCase(), result.getString(3));
            sentencia.close();
            result.close();
            conexion.close();
        } catch (SQLException e) {
            e.printStackTrace();
        }
        txtDetalles.setText(detalles);
    }

    private boolean usuarioTieneVotosDisponibles() {
        boolean r = false;
        String select = String.format("Select votosDisponibles from usuario where nif = '%s'", usuario);
        Connection conexion = BddConnection.newConexionMySQL("cristina_examen");
        PreparedStatement sentencia;
        ResultSet result;
        try {
            sentencia = conexion.prepareStatement(select);
            result = sentencia.executeQuery();
            if (result.next())
                if (result.getInt(1) > 0)
                    r = true;
        } catch (SQLException e) {
            e.printStackTrace();
        }
        return r;
    }

    private void votarPropuesta() {
        if (!propuestaSeleccionada.equals("")) {
            int idPropuesta = 0;
            String select = String.format("select id from propuesta where titulo = '%s'", propuestaSeleccionada);
            Connection conexion = BddConnection.newConexionMySQL("cristina_examen");
            PreparedStatement sentencia;
            ResultSet result;
            try {
                sentencia = conexion.prepareStatement(select);
                result = sentencia.executeQuery();
                if (result.next())
                    idPropuesta = result.getInt(1);

                result.close();
                sentencia.close();
                conexion.close();
            } catch (SQLException e) {
                e.printStackTrace();
            }
            String insert = String.format("insert into votaciones values(%d, '%s')", idPropuesta, usuario);
            BddConnection.ejecutarInsert_Update_Or_Delete(insert);
            String update = String.format("update usuario set votosDisponibles = votosDisponibles-1 where nif = '%s'", usuario);
            BddConnection.ejecutarInsert_Update_Or_Delete(update);
            Alert dialog = new Alert(Alert.AlertType.INFORMATION);
            dialog.setTitle("Éxito");
            dialog.setHeaderText("Operación realizada con éxito.");
            dialog.setContentText("La propuesta ha sido votada");
            dialog.show();
            cargarPropuestasEnListView();
            propuestaSeleccionada = "";
        }
    }
}
