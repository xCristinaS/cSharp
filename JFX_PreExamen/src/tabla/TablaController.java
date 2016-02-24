package tabla;

import javafx.collections.FXCollections;
import javafx.fxml.FXML;
import javafx.fxml.Initializable;
import javafx.scene.control.TableColumn;
import javafx.scene.control.TableView;
import javafx.scene.control.cell.PropertyValueFactory;

import java.net.URL;
import java.util.ArrayList;
import java.util.ResourceBundle;

public class TablaController implements Initializable{

    @FXML TableView myTabla;

    private ArrayList<DataTable> datos = new ArrayList<>();

    @Override
    public void initialize(URL location, ResourceBundle resources) {
        configTabla();
    }

    private void configTabla() {
        ((TableColumn)myTabla.getColumns().get(0)).setCellValueFactory(new PropertyValueFactory<DataTable, String>("columna1"));
        ((TableColumn)myTabla.getColumns().get(1)).setCellValueFactory(new PropertyValueFactory<DataTable, String>("columna2"));

        datos.add(new DataTable("uno", "dos"));
        datos.add(new DataTable("tres", "cuatro"));
        datos.add(new DataTable("cinco", "seis"));

        myTabla.getSelectionModel().setCellSelectionEnabled(true);
        myTabla.setItems(FXCollections.observableArrayList(datos));
        myTabla.setEditable(true);
    }

}
