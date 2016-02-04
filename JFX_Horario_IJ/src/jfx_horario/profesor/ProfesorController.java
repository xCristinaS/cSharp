package jfx_horario.profesor;

import javafx.fxml.FXML;
import javafx.fxml.Initializable;
import javafx.scene.control.Label;
import javafx.scene.control.ListView;
import misClases.BddConnection;
import misClases.Constantes;
import misClases.Tramos;

import java.net.URL;
import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.ResourceBundle;
import java.util.logging.Level;
import java.util.logging.Logger;

/**
 * Created by Cristina on 28/01/2016.
 */
public class ProfesorController implements Initializable {

    @FXML
    Label lblNombreProf, lblAlta;

    @FXML
    ListView lstLunes, lstMartes, lstMiercoles, lstJueves, lstViernes;

    private final String idProf;

    public ProfesorController(String idProf) {
        this.idProf = idProf;
    }

    @Override
    public void initialize(URL location, ResourceBundle resources) {
        cargarHorario();
    }

    private void cargarHorario() {
        Connection conexion = BddConnection.newConexionMySQL("horario");
        PreparedStatement sentencia;
        ResultSet result;
        SimpleDateFormat formatoParse = new SimpleDateFormat("yyyy-MM-dd"), formato = new SimpleDateFormat("dd/MM/yyyy");
        String select = "select codTramo, h.codCurso, h.codOe, h.codAsignatura, a.nombre from horario h, reparto r, asignatura a where h.codAsignatura = a.codAsignatura and r.codOe = h.codOe and r.codcurso = h.codcurso " +
                "and r.CodAsignatura = h.CodAsignatura and codProf = ? order by substring(codTramo, 1, 2) like'L%' desc, substring(codTramo, 1, 2) like'M%' desc, substring(codTramo, 1, 2) like'X%' desc, codtramo;";

        try {
            sentencia = conexion.prepareStatement(select);
            sentencia.setString(1, idProf);
            result = sentencia.executeQuery();
            while (result.next()) {
                if (result.getString(1).charAt(0) == Constantes.LUNES)
                    lstLunes.getItems().add(String.format("Tramo horario: %s - Curso: %s %s - Código asignatura: %s - Nombre asingnatura: %s", dameTramo(result.getString(1).charAt(1)), result.getString(2), result.getString(3), result.getString(4), result.getString(5)));
                else if (result.getString(1).charAt(0) == Constantes.MARTES)
                    lstMartes.getItems().add(String.format("Tramo horario: %s - Curso: %s %s - Código asignatura: %s - Nombre asingnatura: %s", dameTramo(result.getString(1).charAt(1)), result.getString(2), result.getString(3), result.getString(4), result.getString(5)));
                else if (result.getString(1).charAt(0) == Constantes.MIERCOLES)
                    lstMiercoles.getItems().add(String.format("Tramo horario: %s - Curso: %s %s - Código asignatura: %s - Nombre asingnatura: %s", dameTramo(result.getString(1).charAt(1)), result.getString(2), result.getString(3), result.getString(4), result.getString(5)));
                else if (result.getString(1).charAt(0) == Constantes.JUEVES)
                    lstJueves.getItems().add(String.format("Tramo horario: %s - Curso: %s %s - Código asignatura: %s - Nombre asingnatura: %s", dameTramo(result.getString(1).charAt(1)), result.getString(2), result.getString(3), result.getString(4), result.getString(5)));
                else
                    lstViernes.getItems().add(String.format("Tramo horario: %s - Curso: %s %s - Código asignatura: %s - Nombre asingnatura: %s", dameTramo(result.getString(1).charAt(1)), result.getString(2), result.getString(3), result.getString(4), result.getString(5)));
            }

            select = "select nombre, alta from profesor where codProf = ?;";
            sentencia = conexion.prepareStatement(select);
            sentencia.setString(1, idProf);
            result = sentencia.executeQuery();
            if (result.next()) {
                lblNombreProf.setText(Constantes.NOMBRE_PROF + result.getString(1));
                try {
                    lblAlta.setText(Constantes.ALTA_PROF + formato.format(formatoParse.parse(result.getString(2).substring(0,10))));
                } catch (ParseException e) {
                    e.printStackTrace();
                }
            }
            result.close();
            sentencia.close();
            conexion.close();
        } catch (SQLException ex) {
            Logger.getLogger(ProfesorController.class.getName()).log(Level.SEVERE, null, ex);
        }
    }

    private String dameTramo(char hora) {
        switch (hora) {
            case '1':
                return Tramos.PRIMERA.getTramo_H();
            case '2':
                return Tramos.SEGUNDA.getTramo_H();
            case '3':
                return Tramos.TERCERA.getTramo_H();
            case '4':
                return Tramos.CUARTA.getTramo_H();
            case '5':
                return Tramos.QUINTA.getTramo_H();
            case '6':
                return Tramos.SEXTA.getTramo_H();
            default:
                return "";
        }
    }
}
