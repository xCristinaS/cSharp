package contextMenu;

import javafx.event.ActionEvent;
import javafx.event.EventHandler;
import javafx.fxml.FXML;
import javafx.fxml.Initializable;
import javafx.scene.control.Button;
import javafx.scene.control.ContextMenu;
import javafx.scene.control.MenuItem;

import java.net.URL;
import java.util.ResourceBundle;

public class ContextMenuController implements Initializable {

    @FXML
    Button btn;

    @Override
    public void initialize(URL location, ResourceBundle resources) {
        configContextMenu();
    }

    private void configContextMenu() {
        ContextMenu menu = new ContextMenu();
        MenuItem insertar = new MenuItem("Insertar"); // creo los items para el contextMenu
        MenuItem actualizar = new MenuItem("Actualizar");
        MenuItem eliminar = new MenuItem("Eliminar");
/*
        btn.setOnMouseClicked(new EventHandler<MouseEvent>() {
            @Override
            public void handle(MouseEvent event) {
                if (event.getButton() == MouseButton.SECONDARY) { // si hago clic en la tabla con el boton derecho del ratón
                    menu.getItems().setAll(insertar, actualizar, eliminar);
                }
            }
        });
*/

        insertar.setOnAction(new EventHandler<ActionEvent>() {
            @Override
            public void handle(ActionEvent event) {
                System.out.println("has hecho clic en insertar");
            }
        });

        menu.getItems().setAll(insertar, actualizar, eliminar);
        btn.setContextMenu(menu);
    }
}
