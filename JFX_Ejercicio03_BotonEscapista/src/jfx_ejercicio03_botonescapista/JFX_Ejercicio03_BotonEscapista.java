/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package jfx_ejercicio03_botonescapista;

import javafx.animation.KeyFrame;
import javafx.animation.Timeline;
import javafx.application.Application;
import javafx.event.ActionEvent;
import javafx.event.EventHandler;
import javafx.scene.Scene;
import javafx.scene.control.Button;
import javafx.scene.input.MouseEvent;
import javafx.scene.layout.Pane;
import javafx.stage.Stage;
import javafx.util.Duration;

/**
 *
 * @author Cristina
 */
public class JFX_Ejercicio03_BotonEscapista extends Application {
    
    @Override
    public void start(Stage primaryStage) {
        Button btn = new Button();
        btn.setText("Atrápame!");
        btn.setLayoutX(400);
        btn.setLayoutY(400);
        
        Pane root = new Pane(); // el stackPane no te deja poner tamaños manualmente. Éste sí. 
        root.getChildren().add(btn);
        
        Scene scene = new Scene(root, 850, 800);
        
        scene.setOnMouseMoved(new EventHandler<javafx.scene.input.MouseEvent>(){
            @Override
            public void handle(MouseEvent event) {
                //btn.setLayoutX(btn.getLayoutX()+1);
                int i, velocidad = 5;
                if (event.getX() > btn.getLayoutX() && btn.getLayoutX() - 1 > 5)
                    for (i = 0; i <= velocidad; i++)
                        btn.setLayoutX(btn.getLayoutX() -1);
                if (event.getX() < btn.getLayoutX() && btn.getLayoutX() + 1 < 750)
                    for (i = 0; i <= velocidad; i++)
                        btn.setLayoutX(btn.getLayoutX() +1);
                if (event.getY() > btn.getLayoutY() && btn.getLayoutY() - 1 > 5)
                    for (i = 0; i <= velocidad; i++)
                        btn.setLayoutY(btn.getLayoutY() -1);
                if (event.getY() < btn.getLayoutY() && btn.getLayoutY() + 1 < 750)
                    for (i = 0; i <= velocidad; i++)
                        btn.setLayoutY(btn.getLayoutY() +1);
            }
        });
        
        btn.setOnAction(new EventHandler<ActionEvent>() {
            
            @Override
            public void handle(ActionEvent event) {
                btn.setLayoutX(Math.random() * (800 - btn.getWidth()));
                btn.setLayoutY(Math.random() * (800 - btn.getHeight())); 
            }
        });
        
        Timeline timer = new Timeline(new KeyFrame(Duration.seconds(1), new EventHandler<ActionEvent>() {

            @Override
            public void handle(ActionEvent event) {
                btn.setScaleX(btn.getScaleX()+1);
                btn.setScaleY(btn.getScaleY()+1);
            }
        }));
        
        timer.setCycleCount(Timeline.INDEFINITE);
        timer.play();

        primaryStage.setTitle("Botón escapista");
        primaryStage.setScene(scene);
        primaryStage.show();
    }
    
    public static void main(String[] args) {
        launch(args);
    }
    
}
