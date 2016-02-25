package ventanaEstadisticas;

import javafx.collections.FXCollections;
import javafx.fxml.FXML;
import javafx.fxml.Initializable;
import javafx.scene.control.*;
import javafx.scene.control.cell.PropertyValueFactory;
import misClases.BddConnection;
import misClases.MyTableData;

import java.net.URL;
import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.ResourceBundle;

public class EstadisticasController implements Initializable {

    @FXML TableView myTabla;

    private ArrayList<MyTableData> datos = new ArrayList<>();
    @Override
    public void initialize(URL location, ResourceBundle resources) {
        configTabla();
    }

    private void configTabla() {
        ((TableColumn)myTabla.getColumns().get(0)).setCellValueFactory(new PropertyValueFactory<MyTableData, String>("tituloPropuesta"));
        ((TableColumn)myTabla.getColumns().get(1)).setCellValueFactory(new PropertyValueFactory<MyTableData, Integer>("numeroVotos"));

        obtenerDatosDeBdd();

        myTabla.setItems(FXCollections.observableArrayList(datos));
        myTabla.getSelectionModel().setCellSelectionEnabled(true);
    }

    private void obtenerDatosDeBdd() {
        String select = String.format("select titulo, count(*) from votaciones v, propuesta p where p.id = v.propuesta group by propuesta order by 2 desc");
        Connection conexion = BddConnection.newConexionMySQL("cristina_examen");
        PreparedStatement sentencia;
        ResultSet result;
        try {
            sentencia = conexion.prepareStatement(select);
            result = sentencia.executeQuery();
            while (result.next())
                datos.add(new MyTableData(result.getString(1), result.getInt(2)));
            result.close();
            sentencia.close();
            conexion.close();
        } catch (SQLException e) {
            e.printStackTrace();
        }
    }
}
