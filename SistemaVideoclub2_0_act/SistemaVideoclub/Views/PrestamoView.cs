using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaVideoclub.Models;

namespace SistemaVideoclub.Views
{
    public class PrestamoView
    {
        private VideoclubView _view = new VideoclubView();

        // ---------- REGISTRAR PRESTAMO ----------

        public void MostrarPantallaPrestamo(List<Pelicula> peliculas, List<Cliente> clientes)
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Cyan;

            _view.LetrasCentradas("====================================================");
            _view.LetrasCentradas("               REGISTRAR PRESTAMO");
            _view.LetrasCentradas("====================================================");

            Console.ResetColor();

            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Yellow;
            _view.LetrasCentradas("PELICULAS DISPONIBLES");
            Console.ResetColor();

            Console.WriteLine();

            foreach (Pelicula pelicula in peliculas)
            {
                if (pelicula.Disponible)
                {
                    _view.LetrasCentradas("- " + pelicula.Titulo);
                }
            }

            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Yellow;
            _view.LetrasCentradas("CLIENTES REGISTRADOS");
            Console.ResetColor();

            Console.WriteLine();

            foreach (Cliente cliente in clientes)
            {
                _view.LetrasCentradas("- " + cliente.Nombre);
            }

            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Red;
            _view.LetrasCentradas("ESCRIBA 0 PARA VOLVER AL MENU");
            Console.ResetColor();

            Console.WriteLine();
        }

        // Pide el titulo de la pelicula a prestar
        public string PedirPelicula()
        {
            Console.Write("Pelicula: ");

            return Console.ReadLine().Trim();
        }

        // Pide el nombre del cliente que se lleva la pelicula
        public string PedirCliente()
        {
            Console.Write("Cliente: ");

            return Console.ReadLine().Trim();
        }

        public void MostrarPrestamoRegistrado(Prestamo prestamo, Pelicula pelicula)
        {
            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine();
            _view.LetrasCentradas("PRESTAMO REGISTRADO CORRECTAMENTE");
            _view.LetrasCentradas($"{prestamo.PeliculaTitulo} -> {prestamo.ClienteNombre}");

            Console.WriteLine();

            _view.LetrasCentradas($"DEBE DEVOLVERLA ANTES DEL {prestamo.FechaVencimiento:dd/MM/yyyy}");
            _view.LetrasCentradas($"RECARGO POR DIA DE ATRASO: ${pelicula.CalcularRecargo(1)}");

            Console.ResetColor();
        }

        // ---------- PRESTAMOS ACTIVOS ----------

        public void MostrarPrestamosActivos(List<Prestamo> activos)
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Cyan;

            _view.LetrasCentradas("====================================================");
            _view.LetrasCentradas("                PRESTAMOS ACTIVOS                   ");
            _view.LetrasCentradas("====================================================");

            Console.ResetColor();

            Console.WriteLine();
            Console.WriteLine();

            if (activos.Count == 0)
            {
                MostrarError("No hay prestamos activos");
                return;
            }

            _view.LetrasCentradas("--------------------------------------------------------------------");
            _view.LetrasCentradas(" ID    PELICULA              CLIENTE            VENCE        ATRASO");
            _view.LetrasCentradas("--------------------------------------------------------------------");

            foreach (Prestamo prestamo in activos)
            {
                _view.LetrasCentradas(
                    $" {prestamo.id,-5} {prestamo.PeliculaTitulo,-21} {prestamo.ClienteNombre,-18} {prestamo.FechaVencimiento:dd/MM/yyyy}   {prestamo.DiasDeAtraso(DateTime.Now)} dias"
                );
            }

            _view.LetrasCentradas("--------------------------------------------------------------------");
            _view.LetrasCentradas($" Total de prestamos activos: {activos.Count}");
            _view.LetrasCentradas("--------------------------------------------------------------------");
        }

        // ---------- DEVOLUCION ----------

        public void MostrarPantallaDevolucion(List<Prestamo> activos)
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Cyan;

            _view.LetrasCentradas("====================================================");
            _view.LetrasCentradas("              REGISTRAR DEVOLUCION");
            _view.LetrasCentradas("====================================================");

            Console.ResetColor();

            Console.WriteLine();

            // Mostrar los prestamos activos con su ID
            foreach (Prestamo prestamo in activos)
            {
                _view.LetrasCentradas(
                    $"ID: {prestamo.id} - {prestamo.PeliculaTitulo} ({prestamo.ClienteNombre})"
                );
            }

            Console.WriteLine();
        }

        // Pide el id del prestamo a devolver
        public int PedirIdPrestamo()
        {
            int id;

            Console.Write("Ingrese ID del prestamo a devolver: ");

            while (!int.TryParse(Console.ReadLine(), out id))
            {
                MostrarError("Ingrese un numero valido");
                Console.Write("Ingrese ID del prestamo a devolver: ");
            }

            return id;
        }

        public void MostrarDevolucionRegistrada(Prestamo prestamo)
        {
            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine();
            _view.LetrasCentradas("DEVOLUCION REGISTRADA CORRECTAMENTE");
            _view.LetrasCentradas($"{prestamo.PeliculaTitulo} <- {prestamo.ClienteNombre}");

            Console.WriteLine();

            if (prestamo.RecargoCobrado > 0)
            {
                _view.LetrasCentradas($"DIAS DE ATRASO: {prestamo.DiasDeAtraso(DateTime.Now)}");
                _view.LetrasCentradas($"RECARGO A COBRAR: ${prestamo.RecargoCobrado}");
            }
            else
            {
                _view.LetrasCentradas("DEVUELTA EN FECHA, SIN RECARGO");
            }

            Console.ResetColor();
        }

        public void MostrarError(string error)
        {
            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine();
            Console.WriteLine(error);

            Console.ResetColor();
        }
    }
}
