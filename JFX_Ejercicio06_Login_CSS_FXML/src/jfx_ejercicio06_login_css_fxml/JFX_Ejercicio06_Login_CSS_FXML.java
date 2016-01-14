/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package jfx_ejercicio06_login_css_fxml;

import javafx.application.Application;
import javafx.fxml.FXMLLoader;
import javafx.scene.Parent;
import javafx.scene.Scene;
import javafx.scene.text.Text;
import javafx.stage.Stage;

/**
 *
 * @author Cristina
 */
public class JFX_Ejercicio06_Login_CSS_FXML extends Application {
    Scene scene; Parent root;
    @Override
    public void start(Stage stage) throws Exception {
        root = FXMLLoader.load(getClass().getResource("fxml_ejercicio06.fxml"));
        scene = new Scene(root, 300, 275);
        
        stage.setTitle("Ejercicio06 FXML");
        stage.setScene(scene);
        stage.show();
    }

    public void ponerTexto(Object o){
        if (o instanceof Text)
            ((Text) o).setText("Lo conseguí");
    }
    
    /**
     * @param args the command line arguments
     */
    public static void main(String[] args) {
        launch(args);
    }
    
}
