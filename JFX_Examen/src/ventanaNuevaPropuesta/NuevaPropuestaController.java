package ventanaNuevaPropuesta;

import javafx.event.EventHandler;
import javafx.fxml.FXML;
import javafx.fxml.Initializable;
import javafx.scene.control.*;
import javafx.scene.input.MouseEvent;
import misClases.BddConnection;

import java.net.URL;
import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ResourceBundle;

public class NuevaPropuestaController implements Initializable{

    @FXML Label btnProponer, btnCancelar;
    @FXML TextField txtTitulo;
    @FXML TextArea txtDescripcion;

    private String usuario;

    public NuevaPropuestaController(String usuario){
        this.usuario = usuario;
    }
    @Override
    public void initialize(URL location, ResourceBundle resources) {
        configBtnProponer();
        configBtnCancelar();
    }

    private void configBtnCancelar() {
        btnCancelar.setOnMouseClicked(new EventHandler<MouseEvent>() {
            @Override
            public void handle(MouseEvent event) {
                txtTitulo.setText("");
                txtDescripcion.setText("");
            }
        });
    }

    private void configBtnProponer() {
        btnProponer.setOnMouseClicked(new EventHandler<MouseEvent>() {
            @Override
            public void handle(MouseEvent event) {
                if (!txtTitulo.getText().equals("") && !txtDescripcion.getText().equals("") && !tituloYaAsignado()){
                    String insert = String.format("insert into propuesta (titulo, descripcion, usuario) values ('%s', '%s', '%s');", txtTitulo.getText(), txtDescripcion.getText(), usuario);
                    BddConnection.ejecutarInsert_Update_Or_Delete(insert);
                    txtTitulo.setText("");
                    txtDescripcion.setText("");
                } else if (tituloYaAsignado()) {
                    Alert errorDialog = new Alert(Alert.AlertType.ERROR);
                    errorDialog.setTitle("¡Error!");
                    errorDialog.setHeaderText("Error al tratar de insertar la propuesta");
                    errorDialog.setContentText("El titulo de la propuesta ya está en uso.");
                    errorDialog.showAndWait();
                } else {
                    Alert errorDialog = new Alert(Alert.AlertType.ERROR);
                    errorDialog.setTitle("¡Error!");
                    errorDialog.setHeaderText("Error al tratar de insertar la propuesta");
                    errorDialog.setContentText("Asegurese de que los campos titulo y descripción están cumplimentados.");
                    errorDialog.showAndWait();
                }
            }
        });
    }

    private boolean tituloYaAsignado(){
        boolean r = false;
        String select = String.format("select count(*) from propuesta where titulo = '%s'", txtTitulo.getText());
        Connection conexion = BddConnection.newConexionMySQL("cristina_examen");
        PreparedStatement sentencia;
        ResultSet result;

        try {
            sentencia = conexion.prepareStatement(select);
            result = sentencia.executeQuery();
            if (result.next() && result.getInt(1) != 0)
                r = true;

            result.close();
            sentencia.close();
            conexion.close();
        } catch (SQLException e) {
            e.printStackTrace();
        }
        return r;
    }
}
