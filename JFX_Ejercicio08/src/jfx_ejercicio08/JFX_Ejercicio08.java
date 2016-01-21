/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package jfx_ejercicio08;

import java.util.ArrayList;
import javafx.animation.KeyFrame;
import javafx.animation.KeyValue;
import javafx.animation.PathTransition;
import javafx.animation.PathTransition.OrientationType;
import javafx.animation.RotateTransition;
import javafx.animation.Timeline;
import javafx.application.Application;
import javafx.scene.Group;
import javafx.scene.Scene;
import javafx.scene.paint.Color;
import javafx.scene.paint.CycleMethod;
import javafx.scene.paint.LinearGradient;
import javafx.scene.paint.RadialGradient;
import javafx.scene.paint.Stop;
import javafx.scene.shape.CubicCurveTo;
import javafx.scene.shape.LineTo;
import javafx.scene.shape.MoveTo;
import javafx.scene.shape.Path;
import javafx.scene.shape.PathElement;
import javafx.scene.shape.Rectangle;
import javafx.stage.Stage;
import javafx.util.Duration;

/**
 *
 * @author Cristina
 */
public class JFX_Ejercicio08 extends Application {

    @Override
    public void start(Stage primaryStage) {
        Group root = new Group();
        Scene scene = new Scene(root, 1000, 900, Color.BEIGE);
        Rectangle cuadrado1, cuadrado2;
        Stop[] colores = {new Stop(0, Color.web("#f8bd55")), new Stop(0.14, Color.web("#c0fe56")), new Stop(0.28, Color.web("#5dfbc1")), new Stop(0.43, Color.web("#64c2f8")), new Stop(0.57, Color.web("#be4af7")), new Stop(0.71, Color.web("#ed5fc2")), new Stop(0.85, Color.web("#ef504c")), new Stop(1, Color.web("#f2660f"))};
        PathTransition transicion1 = new PathTransition(), transicion2 = new PathTransition();
        Path path1 = new Path(), path2 = new Path();
        CubicCurveTo curva1 = new CubicCurveTo(), curva2 = new CubicCurveTo();
        ArrayList<PathElement> lista1 = new ArrayList<PathElement>(), lista2 = new ArrayList<PathElement>();
        Timeline timeline = new Timeline();

        cuadrado1 = new Rectangle(150, 150, new RadialGradient(0f, 1f, 1f, 0f, 0.2f, true, CycleMethod.REFLECT, colores));
        cuadrado2 = new Rectangle(150, 150, new LinearGradient(0f, 1f, 1f, 0f, true, CycleMethod.REFLECT, colores));

        cuadrado1.setLayoutX(cuadrado1.getWidth() / 2 - 10);
        cuadrado1.setLayoutY(scene.getHeight() - cuadrado1.getHeight() / 2 - 10);
        cuadrado2.setLayoutX(cuadrado2.getWidth() / 2 + 10);
        cuadrado2.setLayoutY(cuadrado2.getHeight() / 2 + 10);

        cuadrado2.setArcHeight(50); // para redondear las esquinas.
        cuadrado2.setArcWidth(50); // tienen que ponerse los dos, el arcHeight y el arcWidth.
        cuadrado1.setArcWidth(50);
        cuadrado1.setArcHeight(50);

        curva1.setControlX1(scene.getWidth());
        curva1.setControlY1(150);
        curva1.setControlX2(0);
        curva1.setControlY2(scene.getHeight() - scene.getHeight() / 2);
        curva1.setX(scene.getWidth() - cuadrado2.getWidth() - 20);
        curva1.setY(scene.getHeight() - cuadrado2.getHeight() - 20);

        lista1.add(new MoveTo(0, 0));
        lista1.add(curva1);
        lista1.add(new LineTo(scene.getWidth() - cuadrado2.getWidth() - 20, 0));
        path1.getElements().addAll(lista1);

        curva2.setControlX1(cuadrado1.getWidth() + 10);
        curva2.setControlY1(-scene.getHeight());
        curva2.setControlX2(scene.getWidth() - cuadrado1.getWidth());
        curva2.setControlY2(scene.getHeight());
        curva2.setX(scene.getWidth() - cuadrado1.getWidth() - 20);
        curva2.setY(-scene.getHeight() + cuadrado1.getHeight() + 20);

        lista2.add(new MoveTo(0, 0));
        lista2.add(curva2);
        path2.getElements().addAll(lista2);

        transicion1.setDuration(Duration.millis(3000));
        transicion1.setNode(cuadrado2);
        transicion1.setPath(path1);
        transicion1.setOrientation(OrientationType.NONE);
        transicion1.setCycleCount(Timeline.INDEFINITE);
        transicion1.setAutoReverse(true);
        transicion1.play();

        transicion2.setDuration(Duration.millis(3000));
        transicion2.setNode(cuadrado1);
        transicion2.setPath(path2);
        transicion2.setOrientation(OrientationType.NONE);
        transicion2.setCycleCount(Timeline.INDEFINITE);
        transicion2.setAutoReverse(true);
        transicion2.play();

        RotateTransition rotateTransition = new RotateTransition(Duration.millis(300), cuadrado2);
        rotateTransition.setByAngle(180);
        rotateTransition.setCycleCount(Timeline.INDEFINITE);
        rotateTransition.setAutoReverse(true);
        rotateTransition.play();

        RotateTransition rotateTransition2 = new RotateTransition(Duration.millis(100), cuadrado1);
        rotateTransition2.setByAngle(180);
        rotateTransition2.setCycleCount(Timeline.INDEFINITE);
        rotateTransition2.play();

        root.getChildren().add(cuadrado1);
        root.getChildren().add(cuadrado2);

        primaryStage.setTitle("Rotación & Translación");
        primaryStage.setScene(scene);
        primaryStage.show();
    }

    /**
     * @param args the command line arguments
     */
    public static void main(String[] args) {
        launch(args);
    }

}
