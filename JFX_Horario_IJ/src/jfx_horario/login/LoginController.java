package jfx_horario.login;

import javafx.event.ActionEvent;
import javafx.event.Event;
import javafx.event.EventHandler;
import javafx.fxml.FXML;
import javafx.fxml.FXMLLoader;
import javafx.fxml.Initializable;
import javafx.scene.Parent;
import javafx.scene.Scene;
import javafx.scene.control.Button;
import javafx.scene.control.TextField;
import javafx.scene.image.Image;
import javafx.scene.image.ImageView;
import javafx.stage.Stage;
import misClases.BddConnection;

import java.io.IOException;
import java.net.URL;
import java.sql.Connection;
import java.sql.SQLException;
import java.util.ResourceBundle;

/**
 * Created by Cristina on 27/01/2016.
 */
public class LoginController implements Initializable {

    @FXML
    TextField txtUsuario, txtContra;

    @FXML
    Button btnLog;

    @FXML
    ImageView imgControlU, imgControlC;


    private static Connection conexion;
    private Image imagenCorrecto = new Image("file:..\\..\\imagenes\\bien.png"), imagenFallo = new Image("file:..\\..\\imagenes\\fallo.png");

    @Override
    public void initialize(URL location, ResourceBundle resources) {
        initViews();
        abrirConexion();
        //imgControlC.setImage(imagenCorrecto);
        //imgControlU.setImage(imagenFallo);
    }

    private void initViews() {
        btnLog.setOnAction(new EventHandler<ActionEvent>() {
            @Override
            public void handle(ActionEvent event) {
                if (txtUsuario.getText().equals("cris") && txtContra.getText().equals("cris")) {
                    try {
                        Parent root = FXMLLoader.load(getClass().getResource("../jefatura/jefatura.fxml"));
                        Stage stage = new Stage();
                        stage.setTitle("Jefatura");
                        stage.setScene(new Scene(root));
                        btnLog.getScene().getWindow().hide();
                        stage.showAndWait();
                        ((Stage) btnLog.getScene().getWindow()).show();
                    } catch (IOException e) {
                        e.printStackTrace();
                    }
                }
            }
        });
    }


    public void onTxtUsuarioTextChanged(Event event) {

    }

    public void onTxtContraTextChanged(Event event) {

    }

    public static void abrirConexion() {
        conexion = BddConnection.newConexionMySQL("horario");
    }

    public static void cerrarConexion() {
        try {
            conexion.close();
        } catch (SQLException e) {
            e.printStackTrace();
        }
    }
}
