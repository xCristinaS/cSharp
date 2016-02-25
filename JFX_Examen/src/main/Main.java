package main;

import javafx.application.Application;
import javafx.fxml.FXMLLoader;
import javafx.fxml.Initializable;
import javafx.scene.Parent;
import javafx.scene.Scene;
import javafx.stage.Stage;

import java.io.IOException;
import java.net.URL;

public class Main extends Application {

    @Override
    public void start(Stage primaryStage) throws Exception{
        Parent root = FXMLLoader.load(getClass().getResource("../ventanaLogin/login.fxml"));
        primaryStage.setResizable(false);
        primaryStage.setTitle("Entrada");
        primaryStage.setScene(new Scene(root));
        primaryStage.show();
    }

    public static void main(String[] args) {
        launch(args);
    }

    public static Stage lanzarVentana(String titulo, URL rutaXML, Initializable controlador) {
        Parent root;
        Stage stage = new Stage();
        String tituloWindow = titulo;
        try {
            if (controlador == null)
                root = FXMLLoader.load(rutaXML);
            else {
                FXMLLoader loader = new FXMLLoader(rutaXML);
                loader.setController(controlador);
                root = loader.load();
            }
            stage.setTitle(tituloWindow);
            stage.setScene(new Scene(root));
            stage.setResizable(false);
            //stage.getIcons().add(new Image("/imagenes/icono.png"));
            //stage.initStyle(StageStyle.UNDECORATED);
        } catch (IOException e) {
            e.printStackTrace();
        }
        return stage;
    }
}
