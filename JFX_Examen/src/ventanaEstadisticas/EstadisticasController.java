package ventanaEstadisticas;

import javafx.event.EventHandler;
import javafx.fxml.FXML;
import javafx.fxml.Initializable;
import javafx.scene.control.Alert;
import javafx.scene.control.Label;
import javafx.scene.control.PasswordField;
import javafx.scene.control.TextField;
import javafx.scene.input.MouseEvent;
import javafx.stage.Stage;
import main.Main;
import utilidad.BddConnection;
import utilidad.Constantes;
import ventanaListaPropuestas.ListaPropuestasController;

import java.net.URL;
import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ResourceBundle;

public class EstadisticasController implements Initializable {


    @Override
    public void initialize(URL location, ResourceBundle resources) {

    }
}
