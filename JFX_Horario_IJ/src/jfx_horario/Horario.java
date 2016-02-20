/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package jfx_horario;

import javafx.application.Application;
import javafx.event.EventHandler;
import javafx.fxml.FXMLLoader;
import javafx.scene.Parent;
import javafx.scene.Scene;
import javafx.scene.input.MouseEvent;
import javafx.scene.layout.BorderPane;
import javafx.stage.Stage;
import javafx.stage.StageStyle;
import javafx.stage.Window;
import jfx_horario.login.LoginController;

/**
 *
 * @author Cristina
 */
public class Horario extends Application {

    private double posX, posY;
    @Override
    public void start(Stage stage) throws Exception {
        stage.initStyle(StageStyle.TRANSPARENT); // Para quitarle el borde a la ventana.
        BorderPane root = new BorderPane(FXMLLoader.load(getClass().getResource("login/login.fxml")));

        root.setOnMousePressed(event -> {
            posX = event.getX();
            posY =  event.getY();
        });

        root.setOnMouseDragged(event -> {
            stage.setX(event.getScreenX() - posX);
            stage.setY(event.getScreenY() - posY);
        });

        stage.setTitle("Login");
        Scene scene = new Scene(root);

        stage.setScene(scene);
        stage.setResizable(false);
        stage.show();
    }

    public static void main(String[] args) {
        launch(args);
    }

}
