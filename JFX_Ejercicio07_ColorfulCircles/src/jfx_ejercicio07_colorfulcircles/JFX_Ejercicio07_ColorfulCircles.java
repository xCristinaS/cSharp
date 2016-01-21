/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package jfx_ejercicio07_colorfulcircles;

import static java.lang.Math.random;
import javafx.animation.KeyFrame;
import javafx.animation.KeyValue;
import javafx.animation.Timeline;
import javafx.application.Application;
import javafx.scene.Group;
import javafx.scene.Node;
import javafx.scene.Scene;
import javafx.scene.effect.BlendMode;
import javafx.scene.effect.BoxBlur;
import javafx.scene.paint.Color;
import javafx.scene.paint.CycleMethod;
import javafx.scene.paint.LinearGradient;
import javafx.scene.paint.Stop;
import javafx.scene.shape.Circle;
import javafx.scene.shape.Rectangle;
import javafx.scene.shape.StrokeType;
import javafx.stage.Stage;
import javafx.util.Duration;

public class JFX_Ejercicio07_ColorfulCircles extends Application {

    @Override
    public void start(Stage primaryStage) {
        Group root = new Group();
        Scene scene = new Scene(root, 900, 900, Color.BLACK);

        Group circles = new Group();
        for (int i = 0; i < 30; i++) {
            Circle circle = new Circle(150, Color.web("white", 0.05));
            circle.setStrokeType(StrokeType.OUTSIDE);
            circle.setStroke(Color.web("white", 0.16));
            circle.setStrokeWidth(4);
            circles.getChildren().add(circle);
        }
        // Gradientes de colores para el rectángulo.
        Rectangle colors = new Rectangle(scene.getWidth(), scene.getHeight(),
                new LinearGradient(0f,// startX.
                        1f, // startY.
                        1f, // endX.
                        0f, // endY.
                        true, // el valor true indica que el gradiente es proporcional al rectángulo. 
                        CycleMethod.NO_CYCLE, // para que no se repita el ciclo de gradientes. 
                        new Stop[]{
                            new Stop(0, Color.web("#f8bd55")),
                            new Stop(0.14, Color.web("#c0fe56")),
                            new Stop(0.28, Color.web("#5dfbc1")),
                            new Stop(0.43, Color.web("#64c2f8")),
                            new Stop(0.57, Color.web("#be4af7")),
                            new Stop(0.71, Color.web("#ed5fc2")),
                            new Stop(0.85, Color.web("#ef504c")),
                            new Stop(1, Color.web("#f2660f"))}));

        //colors.widthProperty().bind(scene.widthProperty()); // para que la anchura del rectángulo esté ligada a la de scene.
        //colors.heightProperty().bind(scene.heightProperty()); // para que la altura del rectángulo esté ligada a la de scene.
        //root.getChildren().add(colors);
        //root.getChildren().add(circles);
        Group blendModeGroup = new Group(new Group(new Rectangle(scene.getWidth(), scene.getHeight(), Color.BLACK), circles), colors);
        colors.setBlendMode(BlendMode.OVERLAY);
        root.getChildren().add(blendModeGroup);

        circles.setEffect(new BoxBlur(10, 10, 3)); // Para hacer el desenfoque. 

        Timeline timeline = new Timeline();
        for (Node circle : circles.getChildren()) {
            timeline.getKeyFrames().addAll(
                    new KeyFrame(Duration.ZERO, // set start position at 0
                            new KeyValue(circle.translateXProperty(), random() * 800),
                            new KeyValue(circle.translateYProperty(), random() * 800)
                    ),
                    new KeyFrame(new Duration(40000), // set end position at 40s
                            new KeyValue(circle.translateXProperty(), random() * 800),
                            new KeyValue(circle.translateYProperty(), random() * 800)
                    )
            );
        }
        // play 40s of animation
        timeline.play();

        primaryStage.setScene(scene);
        primaryStage.show();
    }

    public static void main(String[] args) {
        launch(args);
    }
}
