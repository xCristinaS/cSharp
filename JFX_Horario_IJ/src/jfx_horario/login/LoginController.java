package jfx_horario.login;

import javafx.event.ActionEvent;
import javafx.event.EventHandler;
import javafx.fxml.FXML;
import javafx.fxml.FXMLLoader;
import javafx.fxml.Initializable;
import javafx.scene.Node;
import javafx.scene.Parent;
import javafx.scene.Scene;
import javafx.scene.control.Button;
import javafx.scene.control.TextField;
import javafx.stage.Stage;
import javafx.stage.Window;
import jfx_horario.jefatura.JefaturaController;

import java.io.IOException;
import java.net.URL;
import java.util.Enumeration;
import java.util.ResourceBundle;

/**
 * Created by Cristina on 27/01/2016.
 */
public class LoginController implements Initializable {

    @FXML
    TextField txtUsuario;

    @FXML
    TextField txtContra;

    @FXML
    Button btnLog;

    @Override
    public void initialize(URL location, ResourceBundle resources) {
        initViews();
    }

    private void initViews(){
        btnLog.setOnAction(new EventHandler<ActionEvent>() {
            @Override
            public void handle(ActionEvent event) {
                if (txtUsuario.getText().equals("cris") && txtContra.getText().equals("cris")){
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
}
