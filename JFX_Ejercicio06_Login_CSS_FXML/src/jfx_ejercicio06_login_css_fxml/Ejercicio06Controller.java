/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package jfx_ejercicio06_login_css_fxml;

import java.net.URL;
import java.util.ResourceBundle;
import javafx.event.ActionEvent;
import javafx.fxml.FXML;
import javafx.fxml.Initializable;
import javafx.scene.text.Text;

/**
 *
 * @author Cristina
 */
public class Ejercicio06Controller implements Initializable {
    
    @FXML
    private Text idTextoBoton;
    private JFX_Ejercicio06_Login_CSS_FXML form = new JFX_Ejercicio06_Login_CSS_FXML();

    @FXML
    protected void handleSubmitButtonAction(ActionEvent event) {
        form.ponerTexto(idTextoBoton);
    }

    @Override
    public void initialize(URL url, ResourceBundle rb) {
        // TODO
    }

}
