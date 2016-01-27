/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package jfx_horario;

import javafx.application.Application;
import javafx.fxml.FXMLLoader;
import javafx.scene.Parent;
import javafx.scene.Scene;
import javafx.stage.Stage;
import jfx_horario.login.LoginController;

/**
 *
 * @author Cristina
 */
public class Horario extends Application {

    @Override
    public void start(Stage stage) throws Exception {
/*
        Parent root = FXMLLoader.load(getClass().getResource("jefatura/jefatura.fxml"));

        Scene scene = new Scene(root);

        stage.setScene(scene);
        stage.show();

*/
        Parent root = FXMLLoader.load(getClass().getResource("login/login.fxml"));
        stage.setTitle("Horario");
        Scene scene = new Scene(root);

        stage.setScene(scene);
        stage.setOnCloseRequest(event -> {
            LoginController.cerrarConexion();
        });
        stage.show();


    }

    /**
     * @param args the command line arguments
     */
    public static void main(String[] args) {
        launch(args);

    }

}
