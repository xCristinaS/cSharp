using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_Consola
{
    class Program
    {
        static void Main(string[] args)
        {
            byte uno = 10;
            float dos = 5.2F;
            bool tres = true;
            string cuatro = "cadena";
            double cinco = 5.12;
            short seis = 123;
            long siete = 1234567;
            int ocho = 23;

            Console.WriteLine("- TIPOS DE DATOS: ");
            Console.WriteLine("byte: " + uno + ", float: " + dos + ", boolean: " + tres + ", string: " + cuatro 
                + ", double: " + cinco + ", short: " + seis);
            Console.WriteLine("long: " + siete + ", int: " + ocho);

            Console.WriteLine();

            // Las variables de tipo dynamic hacen una interpretación del tipo de datos que debe almacenar (enteros, decimales, cadenas, etc)
            dynamic din = "hola";
            Console.WriteLine("- VARIABLES DINÁMICAS: ");
            Console.WriteLine(din);
            din = 10;
            Console.WriteLine(din);
            din = 2.1;
            Console.WriteLine(din);
            din = true;
            Console.WriteLine(din);

            Console.WriteLine();

            // Constantes: 
            const float PI = 3.1416F;
            const string SALUDO = "Hola";
            Console.WriteLine("- CONSTANTES: ");
            Console.WriteLine("valor de PI: " + PI);
            Console.WriteLine("Valor de SALUDO: " + SALUDO);

            Console.WriteLine();

            // Operadores:
            byte num1 = 124, num2 = 254;
            Console.WriteLine("- OPERADORES: ");
            Console.WriteLine("Suma: " + (num1 + num2));
            Console.WriteLine("Resta: " + (num1 - num2));
            Console.WriteLine("División: " + (num1 / num2)); // No harían falta los paréntesis porque la división se hace antes que la suma. 
            Console.WriteLine("Multiplicación: " + num1 * num2); // Quito los paréntesis porque la multiplicación también se hace antes que la suma. 
            Console.WriteLine("Concatena: " + num1 + num2); // hacenn falta paréntesis para que realice la suma.
            Console.WriteLine("Resto: " + (num1 % num2)); // Devuelve el resto de la división.  

            Console.WriteLine();

            //Incrementos y Decrementos:
            int num3 = 10;
            Console.WriteLine("- INCREMENTOS Y DECREMENTOS: ");
            Console.WriteLine("Valor: " + num3);
            num3++; // num3 = num3 + 1;
            Console.WriteLine("Valor: " + num3);
            num3 += 10; // num3 = num3 + 10;
            Console.WriteLine("Valor: " + num3);
            num3 -= 5; // num3 = num3 - 5;
            Console.WriteLine("Valor: " + num3);
            num3--; // num3 = num3 - 1;
            Console.WriteLine("Valor: " + num3);
            num3 *= 3; // num3 = num3 * 3;
            Console.WriteLine("Valor: " + num3);
            num3 /= 5; // num3 = num3 / 5;
            Console.WriteLine("Valor: " + num3);

            Console.WriteLine();

            // Operadores de comparación + AND + OR.
            Console.WriteLine("- OPERADORES DE COMPARACIÓN + AND + OR:");
            Console.WriteLine("¿Es cierto que 3 == 3? " + (3 ==  3));
            Console.WriteLine("¿Es cierto que 3 != 3? " + (3 != 3));
            Console.WriteLine("¿Es cierto que 3 > 3? " + (3 > 3));
            Console.WriteLine("¿Es cierto que 3 < 3? " + (3 < 3));
            Console.WriteLine("¿Es cierto que 3 >= 3? " + (3 >= 3));
            Console.WriteLine("¿Es cierto que 3 <= 3? " + (3 <= 3));
            Console.WriteLine("¿Es cierto que 3 == 3 y 4 == 4? " + (3 == 3 && 4==4));
            Console.WriteLine("¿Es cierto que 3 == 3 y 4 != 4? " + (3 == 3 && 4 != 4));
            Console.WriteLine("¿Es cierto que 3 == 3 o 4 == 4? " + (3 == 3 || 4 == 4));
            Console.WriteLine("¿Es cierto que 3 == 3 o 4 != 4? " + (3 == 3 || 4 != 4));
            Console.WriteLine("¿Es cierto que 3 != 3 y 4 != 4? " + (3 != 3 && 4 != 4));

            Console.WriteLine();

            // Estructura IF.
            byte edad = 10;
            Console.WriteLine("- ESTRUCTURA IF:");
            if (edad < 15)
                Console.WriteLine("Eres un enano");
            else if (edad >= 18)
                Console.WriteLine("Eres mayor de edad.");
            else
            {
                Console.WriteLine("No eres mayor de edad.");
            }

            Console.WriteLine();

            // Estructura Switch.
            string dia = "lunes";
            Console.WriteLine("- ESTRUCTURA SWITCH:");
            switch (dia)
            { 
                case "lunes":
                    Console.WriteLine("Es lunes.");
                    break;
                case "martes":
                    Console.WriteLine("Es martes.");
                    break;
                case "miercoles":
                    Console.WriteLine("Es miércoles.");
                    break;
                case "jueves":
                    Console.WriteLine("Es jueves.");
                    break;
                case "viernes":
                    Console.WriteLine("Es viernes.");
                    break;
                case "sabado":
                    Console.WriteLine("Es sábado.");
                    break;
                case "domingo":
                    Console.WriteLine("Es domingo.");
                    break;
                default:
                    Console.WriteLine("No es un dia.");
                    break;
            }

            Console.WriteLine();

            // Bucle For.
            Console.WriteLine("- BUCLE FOR:");
            for (int a = 1; a <= 3; a++)
                Console.WriteLine(a);

            Console.WriteLine();

            // Bucle While.
            Console.WriteLine("- BUCLE WHILE:");
            int b = 1;
            while (b <= 3)
            {
                Console.WriteLine(b);
                b++;
            }
            Console.WriteLine();

            // Bucle Do While.
            Console.WriteLine("- BUCLE DO WHILE:");
            int c = 1;
            do
            {
                Console.WriteLine(c);
                c++;
            } while (c <= 3);

            Console.WriteLine();

            // Arrays.
            string[] nombres = new string[3];
            nombres[0] = "lolo";
            nombres[1] = "pepe";
            nombres[2] = "juan";
            Console.WriteLine("- ARRAYS:");
            for (int d = 0; d < nombres.Length; d++)
                Console.WriteLine(nombres[d]);

            Console.WriteLine();

            // Entradas de usuario. 
    /*        Console.WriteLine("-ENTRADAS DE USUARIO: ");
            byte edad1; string nombre;
            Console.Write("Dime tu nombre: ");
            nombre = Console.ReadLine();
            Console.Write("Dime tu edad: ");
            edad1 = Convert.ToByte(Console.ReadLine());
            Console.WriteLine();
            Console.WriteLine("Te llamas " + nombre + " y tienes " + edad1 + " años.");
    */
            Console.WriteLine();

            // En clase 01. 
            Console.ForegroundColor = ConsoleColor.Red;
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.Title = "En clase";
            Console.Beep();
            bool salir = false; string resp;
            int cont = 0;
            do {
                Console.WriteLine("¿Quiere salir?");
                resp = Console.ReadLine();
                if (resp.ToUpper().Equals("NO"))
                {
                    salir = false;
                    if (cont == 0)
                        Console.WriteLine("Hola mundo");
                    else
                        Console.WriteLine("Pues aquí estamos");
                } 
                else if (resp.ToUpper().Equals("SI"))
                    salir = true;

                cont++;
            } while (!salir);

           // Console.ReadKey(); // La consola se espera a que presionemos una tecla para cerrarse. La ejecución se paraliza.  
        }
    }
}
