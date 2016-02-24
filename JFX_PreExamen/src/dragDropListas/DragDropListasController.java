package dragDropListas;

import javafx.collections.FXCollections;
import javafx.event.Event;
import javafx.event.EventHandler;
import javafx.fxml.FXML;
import javafx.fxml.Initializable;
import javafx.scene.control.ListView;
import javafx.scene.input.*;

import java.net.URL;
import java.util.ArrayList;
import java.util.ResourceBundle;

public class DragDropListasController implements Initializable {

    @FXML
    ListView lst1, lst2;

    private ArrayList<String> lista1 = new ArrayList<>(), lista2 = new ArrayList<>();

    @Override
    public void initialize(URL location, ResourceBundle resources) {
        configListView();
        configDragDrop();
    }

    private void configListView() {
        lista1.add("hola");
        lista1.add("eo");
        lista2.add("feo");
        lista2.add("adios");
        lista2.add("wait");

        lst1.setItems(FXCollections.observableArrayList(lista1));
        lst2.setItems(FXCollections.observableArrayList(lista2));
    }

    private void configDragDrop() {

        EventHandler dragDetected = new EventHandler<MouseEvent>() {
            @Override
            public void handle(MouseEvent event) {
                if (event.getSource() instanceof ListView) {
                    ListView l = (ListView) event.getSource();
                    if (l.getSelectionModel().getSelectedItem() != null) {
                        Dragboard db = l.startDragAndDrop(TransferMode.MOVE);
                        ClipboardContent content = new ClipboardContent();
                        content.putString(l.getSelectionModel().getSelectedItem().toString() + "," + l.getId());
                        db.setContent(content);
                    }
                }
                event.consume();
            }
        };

        EventHandler dragOver = new EventHandler<DragEvent>() {
            public void handle(DragEvent event) {
                event.acceptTransferModes(TransferMode.MOVE);
                event.consume();
            }
        };

        EventHandler dragDropped = new EventHandler<DragEvent>() {
            @Override
            public void handle(DragEvent event) {
                Dragboard db = event.getDragboard();
                if (db.hasString()) {
                    String[] aux = db.getString().split(",");
                    String idInicioDragDrop = aux[1];
                    ListView l = (ListView)event.getSource();
                    if (!idInicioDragDrop.equals(l.getId())) {
                        l.getItems().add(aux[0]);
                        if (aux[1].equals(lst1.getId()))
                            lst1.getItems().remove(aux[0]);
                        else
                            lst2.getItems().remove(aux[0]);
                    }
                }
                event.consume();
            }
        };

        lst1.setOnDragDetected(dragDetected);
        lst1.setOnDragDropped(dragDropped);
        lst1.setOnDragOver(dragOver);

        lst2.setOnDragDetected(dragDetected);
        lst2.setOnDragDropped(dragDropped);
        lst2.setOnDragOver(dragOver);
    }


}
