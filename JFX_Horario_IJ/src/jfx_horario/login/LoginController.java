package jfx_horario.login;

import javafx.application.Platform;
import javafx.event.ActionEvent;
import javafx.event.EventHandler;
import javafx.fxml.FXML;
import javafx.fxml.FXMLLoader;
import javafx.fxml.Initializable;
import javafx.scene.Parent;
import javafx.scene.Scene;
import javafx.scene.control.Button;
import javafx.scene.control.Label;
import javafx.scene.control.PasswordField;
import javafx.scene.control.TextField;
import javafx.scene.input.KeyCode;
import javafx.scene.input.KeyEvent;
import javafx.stage.Stage;
import jfx_horario.profesor.ProfesorController;
import misClases.BddConnection;
import misClases.Constantes;

import java.awt.event.MouseEvent;
import java.io.IOException;
import java.net.URL;
import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ResourceBundle;

/**
 * Created by Cristina on 27/01/2016.
 */
public class LoginController implements Initializable {

    @FXML
    TextField txtUsuario;
    @FXML
    PasswordField txtContra;
    @FXML
    Button btnLogin;
    @FXML
    Label lblError;

    private double posX, posY;

    @Override
    public void initialize(URL location, ResourceBundle resources) {
        initViews();
    }

    private void initViews() {
        btnLogin.setOnAction(new EventHandler<ActionEvent>() {
            @Override
            public void handle(ActionEvent event) {
                if (textFielRellenos())
                    tryToloadNextWindow();
            }
        });

        EventHandler<KeyEvent> evento = new EventHandler<KeyEvent>() {
            @Override
            public void handle(KeyEvent event) {
                if (event.getCode().equals(KeyCode.ENTER)) {
                    if (textFielRellenos())
                        tryToloadNextWindow();
                } else
                    lblError.setVisible(false);
            }
        };
        txtContra.setOnKeyPressed(evento);
        txtUsuario.setOnKeyPressed(evento);
        btnLogin.setOnKeyPressed(evento);
    }

    private int consultarBDD(String idUsuario, String contra) {
        int r = -1;
        Connection conexion = BddConnection.newConexionMySQL("horario");
        PreparedStatement sentencia;
        ResultSet result;
        String select = "select tipo from profesor where codProf = ? and contra = ?;";

        try {
            sentencia = conexion.prepareStatement(select);
            sentencia.setString(1, idUsuario);
            sentencia.setString(2, contra);
            result = sentencia.executeQuery();
            if (result.next())
                r = result.getByte(1);
            result.close();
            sentencia.close();
            conexion.close();
        } catch (SQLException e) {
            e.printStackTrace();
        }

        return r;
    }

    public boolean textFielRellenos() {
        boolean r = false;
        if (!txtUsuario.getText().isEmpty() && !txtContra.getText().isEmpty() && txtUsuario.getText().length() <= 3) {
            r = true;
        } else
            lblError.setVisible(true);
        return r;
    }

    public void tryToloadNextWindow() {
        int tipoUser;
        Parent root = null;
        Stage stage;
        boolean logueadoConExito = false;
        String tituloWindow = "";

        tipoUser = consultarBDD(txtUsuario.getText(), txtContra.getText());
        try {
            if (tipoUser == Constantes.TIPO_PROFE) {
                FXMLLoader loader = new FXMLLoader(getClass().getResource(("../profesor/profesor.fxml")));
                loader.setController(new ProfesorController(txtUsuario.getText()));
                root = loader.load();
                tituloWindow = "Horario Profesor";
                logueadoConExito = true;
            } else if (tipoUser == Constantes.TIPO_JEFATURA) {
                root = FXMLLoader.load(getClass().getResource("../jefatura/jefatura.fxml"));
                tituloWindow = "Jefatura";
                logueadoConExito = true;
            } else {
                lblError.setVisible(true);
            }

            if (logueadoConExito) {
                stage = new Stage();
                stage.setTitle(tituloWindow);
                stage.setScene(new Scene(root));
                stage.setResizable(false);
                configDragDropWindow(root, stage);
                stage.show();
                ((Stage)btnLogin.getScene().getWindow()).close();
            }
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    private void configDragDropWindow(Parent root, Stage stage){
        root.setOnMousePressed(event -> {
            posX = event.getX();
            posY =  event.getY();
        });

        root.setOnMouseDragged(event -> {
            stage.setX(event.getScreenX() - posX);
            stage.setY(event.getScreenY() - posY);
        });
    }
}
