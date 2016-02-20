package jfx_horario.insertUpdate;

import javafx.event.ActionEvent;
import javafx.event.EventHandler;
import javafx.fxml.FXML;
import javafx.fxml.Initializable;
import javafx.scene.control.Button;
import javafx.scene.control.Label;
import javafx.scene.control.ListView;
import javafx.stage.Stage;
import javafx.stage.Window;
import javafx.stage.WindowEvent;
import jfx_horario.jefatura.JefaturaController;
import misClases.BddConnection;
import misClases.Constantes;

import java.net.URL;
import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.ResourceBundle;

/**
 * Created by Cristina on 10/02/2016.
 */
public class InsertUpdateFormController implements Initializable {

    @FXML
    ListView lstClases;

    @FXML
    Label lblTexto;

    @FXML
    Button btnEnviar;

    private String codProf;

    public InsertUpdateFormController(String codProf) {
        this.codProf = codProf;
    }

    @Override
    public void initialize(URL location, ResourceBundle resources) {
        initViews();
        //JefaturaController.setVentanaInserUpdateAbierta(true);
        //((Stage) btnEnviar.getScene().getWindow()).setOnHidden();
    }

    private void initViews() {
        // obtengo las asignaturas que imparte el profesor recibido como argumento al crear la instancia de esta ventana
        String select = "select codAsignatura, codCurso, codOe from reparto where codProf = ? order by 2;";
        Connection conexion = BddConnection.newConexionMySQL("horario");
        ArrayList<String> contenido = new ArrayList<>();
        PreparedStatement sentencia;
        ResultSet result;
        try {
            sentencia = conexion.prepareStatement(select);
            sentencia.setString(1, codProf);
            result = sentencia.executeQuery();
            while (result.next())
                contenido.add(String.format("Asignatura: %s - Curso: %s %s", result.getString(1), result.getString(2), result.getString(3))); // obtengo los datos y los introduzco en la lista del contenido
            select = "select nombre from profesor where codProf = ?"; // obtengo el nombre del profesor
            sentencia = conexion.prepareStatement(select);
            sentencia.setString(1, codProf);
            result = sentencia.executeQuery();
            if (result.next())
                lblTexto.setText(Constantes.TEXTO_CLASES_INSERT_UPDATE + result.getString(1)); // lo muestro en el label (clases impartidas por: nombre profesor)
            result.close();
            sentencia.close();
            conexion.close();
        } catch (SQLException e) {
            e.printStackTrace();
        }
        lstClases.getItems().setAll(contenido); // agrego el contenido a la lista de clases que imparte ese profesor
        btnEnviar.setOnAction(new EventHandler<ActionEvent>() {
            @Override
            public void handle(ActionEvent event) { // cuando hago clic en el botón de enviar
                String contenido = lstClases.getSelectionModel().getSelectedItem() == null ? "" : (String) lstClases.getSelectionModel().getSelectedItem(); // si se seleccionó algun registro de la lista
                if (!contenido.equals("")) {
                    String[] aux = contenido.split(" ");
                    JefaturaController.callBack_RecogerDatosFormUpdateInsert(aux[1], aux[4], aux[5]); // le paso los datos a la ventana de jefatura (codAsignatura, codCurso, codOe)
                }
                ((Stage) btnEnviar.getScene().getWindow()).close(); // cierro esta ventana
            }
        });
    }
}
