package ventanaLogin;

import javafx.event.EventHandler;
import javafx.fxml.FXML;
import javafx.fxml.Initializable;
import javafx.scene.control.Alert;
import javafx.scene.control.Label;
import javafx.scene.control.TextField;
import javafx.scene.input.MouseEvent;
import utilidad.BddConnection;
import utilidad.Constantes;

import java.net.URL;
import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ResourceBundle;

public class LoginController implements Initializable{

    @FXML TextField txtUsuario, txtContra;
    @FXML
    Label btnNewUsuario, btnEntrar;

    @Override
    public void initialize(URL location, ResourceBundle resources) {
        configBotones();
    }

    private void configBotones() {
        btnNewUsuario.setOnMouseClicked(new EventHandler<MouseEvent>() {
            @Override
            public void handle(MouseEvent event) {
                if (!txtContra.getText().equals("") && !txtUsuario.getText().equals("")) {
                    String insert = String.format("insert into usuario values ('%s', '%s', %d)", txtUsuario.getText(), txtContra.getText(), Constantes.VOTOS_DISPONIBLES);
                    BddConnection.ejecutarInsert_Update_Or_Delete(insert);
                    Alert dialogConfirm = new Alert(Alert.AlertType.CONFIRMATION);
                    dialogConfirm.setTitle("Éxito");
                    dialogConfirm.setHeaderText("Operación realizada con éxito.");
                    dialogConfirm.setContentText("El nuevo usuario ha sido registrado con éxito. !Bienvenido!");
                    dialogConfirm.showAndWait();
                } else{
                    Alert errorDialog = new Alert(Alert.AlertType.ERROR);
                    errorDialog.setTitle("¡Error!");
                    errorDialog.setHeaderText("Error al registrar nuevo usuario.");
                    errorDialog.setContentText("Los campos nif y contraseña deben estar rellenos, por favor, rellenelos y vuelva a intentarlo.");
                    errorDialog.showAndWait();
                }
            }
        });

        btnEntrar.setOnMouseClicked(new EventHandler<MouseEvent>() {
            @Override
            public void handle(MouseEvent event) {
                if (!txtContra.getText().equals("") && !txtUsuario.getText().equals("")) {
                    String select = String.format("select count(*) from usuario where nif = '%s' and contrasenia = '%s'", txtUsuario.getText(), txtContra.getText());
                    Connection conexion = BddConnection.newConexionMySQL("cristina_examen");
                    PreparedStatement sentencia;
                    ResultSet result;
                    try {
                        sentencia = conexion.prepareStatement(select);
                        result = sentencia.executeQuery();
                        if (result.next())
                            if (result.getInt(1) != 0) {
                                System.out.println("logueado");
                            } else {
                                Alert errorDialog = new Alert(Alert.AlertType.ERROR);
                                errorDialog.setTitle("¡Error!");
                                errorDialog.setHeaderText("Error al tratar de iniciar sesión");
                                errorDialog.setContentText("Los datos introducidos no son correctos");
                                errorDialog.showAndWait();
                            }
                        result.close();
                        sentencia.close();
                        conexion.close();
                    } catch (SQLException e) {
                        e.printStackTrace();
                    }
                } else{
                    Alert errorDialog = new Alert(Alert.AlertType.ERROR);
                    errorDialog.setTitle("¡Error!");
                    errorDialog.setHeaderText("Error al tratar de iniciar sesión");
                    errorDialog.setContentText("Los campos nif y contraseña deben estar rellenos, por favor, rellenelos y vuelva a intentarlo.");
                    errorDialog.showAndWait();
                }
            }
        });
    }
}
