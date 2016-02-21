/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package jfx_horario;

import javafx.application.Application;
import javafx.fxml.FXMLLoader;
import javafx.fxml.Initializable;
import javafx.scene.Parent;
import javafx.scene.Scene;
import javafx.scene.image.Image;
import javafx.scene.layout.BorderPane;
import javafx.stage.Stage;

import java.io.IOException;
import java.net.URL;

/**
 * @author Cristina
 */
public class Horario extends Application {

    private static double posX, posY;

    @Override
    public void start(Stage stage) throws Exception {
        //stage.initStyle(StageStyle.TRANSPARENT); // Para quitarle el borde a la ventana.
        BorderPane root = new BorderPane(FXMLLoader.load(getClass().getResource("login/login.fxml")));
        configDragDropWindow(root, stage);
        stage.setTitle("Login");
        Scene scene = new Scene(root);

        stage.setScene(scene);
        stage.setResizable(false);
        stage.getIcons().add(new Image("/imagenes/icono.png"));
        stage.show();
    }

    public static Stage lanzarVentana(String titulo, URL rutaXML, Initializable controlador) {
        Parent root;
        Stage stage = new Stage();
        String tituloWindow = titulo;
        try {
            if (controlador == null) // si el controlador es igual a null, es porque el fichero xml ya lleva asociado su controlador, en ese caso
                root = FXMLLoader.load(rutaXML); // inflo sus especificaciones
            else { // si el fichero no lleva asociado su controlador:
                FXMLLoader loader = new FXMLLoader(rutaXML); // al loader le indico que tiene que inflar las especificaciones xml
                loader.setController(controlador); // asigno al loader el controlador recibido como argumento
                root = loader.load(); // inflo las especificaciones
            }
            stage.setTitle(tituloWindow);
            stage.setScene(new Scene(root));
            stage.setResizable(false);
            stage.getIcons().add(new Image("/imagenes/icono.png"));
            //stage.initStyle(StageStyle.UNDECORATED);
            configDragDropWindow(root, stage); // para que se pueda arrastrar la ventana
        } catch (IOException e) {
            e.printStackTrace();
        }
        return stage;
    }

    private static void configDragDropWindow(Parent root, Stage stage) {
        root.setOnMousePressed(event -> {
            posX = event.getX();
            posY = event.getY();
        });

        root.setOnMouseDragged(event -> {
            stage.setX(event.getScreenX() - posX);
            stage.setY(event.getScreenY() - posY);
        });
    }

    public static void main(String[] args) {
        launch(args);
    }

}
