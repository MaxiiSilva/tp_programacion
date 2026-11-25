using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVideoclub.Views
{

    public class VideoclubView
    {
        public void MostrarBienvenida()
        {
            Console.Clear();

            // Título
            Console.ForegroundColor = ConsoleColor.Red;

            LetrasCentradas("██╗   ██╗██╗██████╗ ███████╗ ██████╗  ██████╗██╗     ██╗   ██╗██████╗ ");
            LetrasCentradas("██║   ██║██║██╔══██╗██╔════╝██╔═══██╗██╔════╝██║     ██║   ██║██╔══██╗");
            LetrasCentradas("██║   ██║██║██║  ██║█████╗  ██║   ██║██║     ██║     ██║   ██║██████╔╝");
            LetrasCentradas("╚██╗ ██╔╝██║██║  ██║██╔══╝  ██║   ██║██║     ██║     ██║   ██║██╔══██╗");
            LetrasCentradas(" ╚████╔╝ ██║██████╔╝███████╗╚██████╔╝╚██████╗███████╗╚██████╔╝██████╔╝");
            LetrasCentradas("  ╚═══╝  ╚═╝╚═════╝ ╚══════╝ ╚═════╝  ╚═════╝╚══════╝ ╚═════╝ ╚═════╝ ");

            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.White;
            LetrasCentradas("══════════════════════════════════════════════════════════════════════");
            LetrasCentradas("     GESTION DE VIDEOCLUB");
            LetrasCentradas("══════════════════════════════════════════════════════════════════════");

            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Cyan;
            LetrasCentradas("GESTION DE PELICULAS");
            LetrasCentradas("GESTION DE CLIENTES");
            LetrasCentradas("REGISTRO DE PRESTAMOS");
            LetrasCentradas("REGISTRO DE DEVOLUCIONES");
            LetrasCentradas("PRESTAMOS ACTIVOS");

            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.White;
            LetrasCentradas("---------------------------------------------");
            LetrasCentradas($"FECHA: {DateTime.Now:dd/MM/yyyy}");
            LetrasCentradas($"HORA : {DateTime.Now:HH:mm:ss}");
            LetrasCentradas("---------------------------------------------");

            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Yellow;
            LetrasCentradas(">>> PRESIONE UNA TECLA PARA COMENZAR <<<");

            Console.ResetColor();

            Console.ReadKey();
        }





        // Muestra el menu principal
        public string MostrarMenu()
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Cyan;

            LetrasCentradas("==============================================================");
            LetrasCentradas("                       VIDEOCLUB                              ");
            LetrasCentradas("==============================================================");

            Console.ResetColor();

            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.White;
            LetrasCentradas($"FECHA: {DateTime.Now:dd/MM/yyyy}    HORA: {DateTime.Now:HH:mm:ss}");
            Console.ResetColor();

            Console.WriteLine();
            LetrasCentradas("******************** MENU PRINCIPAL ********************");
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Magenta;
            LetrasCentradas("[1]  AGREGAR PELICULA");
            LetrasCentradas("[2]  LISTAR PELICULAS");
            LetrasCentradas("[3]  BUSCAR PELICULA");
            LetrasCentradas("[4]  AGREGAR CLIENTE");
            LetrasCentradas("[5]  REGISTRAR PRESTAMO");
            LetrasCentradas("[6]  VER PRESTAMOS ACTIVOS");
            LetrasCentradas("[7]  ELIMINAR PELICULA DEL VIDEOCLUB");
            LetrasCentradas("[8]  VER CLIENTES");
            LetrasCentradas("[9]  ELIMINAR CLIENTE DEL VIDEOCLUB");
            LetrasCentradas("[10] ACTUALIZAR PELICULA");
            LetrasCentradas("[11] ACTUALIZAR CLIENTE");
            LetrasCentradas("[12] REGISTRAR DEVOLUCION");
            LetrasCentradas("[0]  SALIR");
            Console.ResetColor();

            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Yellow;
            LetrasCentradas("ELIJA UNA OPCION DEL MENU");
            Console.ResetColor();

            Console.WriteLine();
            Console.Write("Opcion: ");

            return Console.ReadLine();
        }



        // Limpia la pantalla
        public void Limpiar()
        {
            Console.Clear();
        }

        public void MostrarMensaje(string mensaje)
        {
            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine();
            Console.WriteLine(mensaje);

            Console.ResetColor();
        }





        // Muestra mensajes de error

        public void MostrarError(string error)
        {
            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine();
            Console.WriteLine(error);

            Console.ResetColor();
        }


        // Espera una tecla para continuar
        public void Pausa()
        {
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.DarkGray;

            Console.WriteLine("Presione una tecla para continuar...");

            Console.ResetColor();

            Console.ReadKey();
        }




        //esto es para q las letras esten centradas y no todo al costado
        public void LetrasCentradas(string texto)
        {
            int espacios = (Console.WindowWidth - texto.Length) / 2;

            if (espacios < 0)
                espacios = 0;

            Console.WriteLine(new string(' ', espacios) + texto);
        }

    }
}
