package chooser;

import javafx.fxml.Initializable;
import javafx.scene.control.ChoiceDialog;
import javafx.stage.FileChooser;
import javafx.stage.Stage;
import javafx.stage.Window;

import java.io.File;
import java.io.FileWriter;
import java.io.IOException;
import java.net.URL;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Date;
import java.util.Optional;
import java.util.ResourceBundle;

public class SaveDialogController implements Initializable {


    @Override
    public void initialize(URL location, ResourceBundle resources) {
        ArrayList<String> choices = new ArrayList<>(); // creo un arrayList con las opciones que mostrará el diálogo
        SimpleDateFormat formato = new SimpleDateFormat("dd_MM_yyyy_HH_mm_ss");
        int opcion;
        choices.add("opcion1");
        choices.add("opcion2");
        choices.add("opcion3");

        ChoiceDialog<String> dialog = new ChoiceDialog<>(choices.get(0), choices); // creo el dialogo con las opciones especificadas anteriormente y marco por defecto la primera
        dialog.setTitle("Generar PDF");
        dialog.setHeaderText("¿Quiere generar un fichero PDF?");
        dialog.setContentText("Elija una opción");

        Optional<String> result = dialog.showAndWait(); // obtengo el resultado
        if (result.isPresent()) { // si hay resultado
            opcion = choices.indexOf(result.get()); // recojo el indice de la opción seleccionada
            FileChooser fileChooser = new FileChooser(); // creo un nuevo diálogo para guardar el fichero
            fileChooser.setTitle("Guardar");
            FileChooser.ExtensionFilter extFilter = new FileChooser.ExtensionFilter("Text File (*.txt)", "*.txt"); // le pongo extensión pdf
            fileChooser.getExtensionFilters().add(extFilter);
            fileChooser.setInitialFileName(String.format("fichrero_%s", formato.format(new Date()))); // nombre por defecto del fichero
            File file = fileChooser.showSaveDialog(dialog.getOwner()); // lanzo el nuevo dialogo
            //fileChooser.showOpenDialog(dialog.getOwner()); // Para abrir ficheros
            if (file != null) { // si se ha seleccionado destino para el nuevo fichero
                crearFichero(file, opcion); // creo el pdf
            }
        }
    }

    private void crearFichero(File file, int opcion) {
        try {
            FileWriter escritor = new FileWriter(file);
            escritor.write("hola que tal");
            escritor.close();
        } catch (IOException e) {
            e.printStackTrace();
        }
    }
}
