using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO; // Libreria para poder gestionar la Entrada-Salida de datos y leer y escribir archivos en el sistema de archivos. 

namespace _02_Consola_EntradaSalida
{
    class Program
    {
        static void Main(string[] args)
        {
            TextWriter texto = new StreamWriter("prueba.txt"); // El fichero prueba no existe, pero se creará y guardará información. 
            texto.WriteLine("Esto se escribe en prueba.txt"); // El fichero está en la carpeta del proyecto --> "nombreProyecto"/bin/debug/prueba.txt
            texto.Close(); // hay que ponerlo para cerrar el fichero. 

            TextReader texto2 = new StreamReader("prueba.txt"); // Para leer el archivo prueba.txt y visualizarlo en consola. 
            Console.WriteLine(texto2.ReadLine());
            texto2.Close();

            StreamWriter texto3 = File.AppendText("prueba.txt"); // Para agregar lineas al fichero. 
            texto3.WriteLine("Esto se agrega al fichero, pero no borra el contenido anterior.");
            texto3.Close();

            Console.WriteLine();

            texto2 = new StreamReader("prueba.txt");
            Console.WriteLine(texto2.ReadToEnd());

            /* CUANDO ESCRIBIMOS UNA LINEA CON EL OBJETO TEXTO, LO QUE HACEMOS ES SOBREESCRIBIR TODO EL CONTENIDO DEL FICHERO. MIENTRAS QUE SI LA ESCRIBIMOD CON EL 
            OBJETO TEXTO3, LA NUEVA LINEA SE AGREGA AL CONTENIDO DEL FICHERO PERO NO BORRA LO QUE YA HUBIERA EN ÉL.*/

            Console.ReadKey();
        }
    }
}
