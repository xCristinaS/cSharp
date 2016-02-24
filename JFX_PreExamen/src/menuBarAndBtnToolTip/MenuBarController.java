package menuBarAndBtnToolTip;

import javafx.event.Event;
import javafx.event.EventHandler;
import javafx.fxml.FXML;
import javafx.fxml.Initializable;
import javafx.scene.control.Menu;
import javafx.scene.control.MenuBar;
import javafx.scene.control.MenuItem;
import javafx.scene.layout.Pane;

import java.net.URL;
import java.util.ResourceBundle;

public class MenuBarController implements Initializable{

    @FXML Pane panel;
    @FXML MenuBar myMenuBuilder;
    @FXML Menu file;

    @Override
    public void initialize(URL location, ResourceBundle resources) {
        configMenuBar();
    }

    private void configMenuBar() {
        //myContextMenu myMenuBar = new myContextMenu();
        Menu subMenuArchivo = new Menu("Archivo"), subMenuGuardar = new Menu("Guardar"), guardarComo = new Menu("guardar como...");

        MenuItem itemArchivo1 = new MenuItem("Nuevo");
        MenuItem itemArchivo2 = new MenuItem("Abrir");
        MenuItem itemArchivo3 = new MenuItem("Renombrar");
        subMenuArchivo.getItems().addAll(itemArchivo1, itemArchivo2, itemArchivo3);

        MenuItem itemGuardarComo1 = new MenuItem("fichero txt");
        MenuItem itemGuardarComo2 = new MenuItem("fichero pdf");
        guardarComo.getItems().addAll(itemGuardarComo1, itemGuardarComo2);

        MenuItem itemGuardar1 = new MenuItem("opcion1");
        subMenuGuardar.getItems().addAll(itemGuardar1, guardarComo);

        myMenuBuilder.getMenus().addAll(subMenuArchivo, subMenuGuardar);

        EventHandler eventoMenusItems = new EventHandler() {
            @Override
            public void handle(Event event) {
                System.out.println("Se ha hecho clic en un menuItem");
            }
        };

        itemArchivo1.setOnAction(eventoMenusItems);
        itemArchivo2.setOnAction(eventoMenusItems);
        itemArchivo3.setOnAction(eventoMenusItems);

        EventHandler eventoMenu = new EventHandler() {
            @Override
            public void handle(Event event) {
                System.out.println("Se ha hecho clic en un menu principal");
            }
        };
        //panel.getChildren().add(myMenuBar);
    }
}
