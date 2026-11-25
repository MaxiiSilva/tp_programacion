using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaVideoclub.Models;

namespace SistemaVideoclub.Views
{
    public class ClienteView
    {
        private VideoclubView _view = new VideoclubView();

        public void MostrarError(string error)
        {
            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine();
            Console.WriteLine(error);

            Console.ResetColor();
        }

        // ---------- AGREGAR ----------

        public void MostrarPantallaAgregar(List<Cliente> clientes)
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Cyan;

            _view.LetrasCentradas("====================================================");
            _view.LetrasCentradas("                  AGREGAR CLIENTE");
            _view.LetrasCentradas("====================================================");

            Console.ResetColor();

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

        // Mensaje de éxito en la pantalla
        public void MostrarClienteAgregado(Cliente cliente)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine();
            _view.LetrasCentradas("========================================");
            _view.LetrasCentradas("CLIENTE AGREGADO CORRECTAMENTE");
            _view.LetrasCentradas($"Nombre: {cliente.Nombre}");
            _view.LetrasCentradas($"DNI: {cliente.Dni}");
            _view.LetrasCentradas($"Telefono: {cliente.Telefono}");
            _view.LetrasCentradas("========================================");
            Console.ResetColor();
        }

        // ---------- ELIMINAR ----------

        public void MostrarPantallaEliminar(List<Cliente> clientes)
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Red;

            _view.LetrasCentradas("====================================================");
            _view.LetrasCentradas("                ELIMINAR CLIENTE                    ");
            _view.LetrasCentradas("====================================================");

            Console.ResetColor();

            Console.WriteLine();

            // Mostrar todos los clientes con ID
            foreach (Cliente cliente in clientes)
            {
                _view.LetrasCentradas($"ID: {cliente.id} - {cliente.Nombre} (DNI {cliente.Dni})");
            }

            Console.WriteLine();
        }

        // ---------- ACTUALIZAR ----------

        public void MostrarPantallaActualizar(List<Cliente> clientes)
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Cyan;

            _view.LetrasCentradas("====================================================");
            _view.LetrasCentradas("                ACTUALIZAR CLIENTE");
            _view.LetrasCentradas("====================================================");

            Console.ResetColor();

            Console.WriteLine();

            // Mostrar todos los clientes con su ID
            foreach (Cliente cliente in clientes)
            {
                _view.LetrasCentradas($"ID: {cliente.id} - {cliente.Nombre}");
            }

            Console.WriteLine();
        }

        // Pide el id del cliente (accion: "eliminar" o "modificar")
        public string PedirId(string accion)
        {
            Console.Write($"Ingrese ID del cliente a {accion}: ");

            return Console.ReadLine();
        }

        public string PedirNuevoNombre()
        {
            Console.WriteLine();

            Console.Write("Nuevo nombre: ");

            return Console.ReadLine();
        }

        public string PedirNuevoDni()
        {
            Console.WriteLine();

            Console.Write("Nuevo DNI: ");

            return Console.ReadLine();
        }

        public string PedirNuevoTelefono()
        {
            Console.WriteLine();

            Console.Write("Nuevo telefono: ");

            return Console.ReadLine();
        }

        // ---------- VER CLIENTES ----------

        public void MostrarPantallaConsulta(List<Cliente> clientes)
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Cyan;

            _view.LetrasCentradas("====================================================");
            _view.LetrasCentradas("               CONSULTA DE CLIENTES                 ");
            _view.LetrasCentradas("====================================================");

            Console.ResetColor();

            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Yellow;
            _view.LetrasCentradas("CLIENTES DISPONIBLES");
            Console.ResetColor();

            Console.WriteLine();

            foreach (Cliente cliente in clientes)
            {
                _view.LetrasCentradas("- " + cliente.Nombre);
            }

            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Green;
            _view.LetrasCentradas("INGRESE EL NOMBRE DEL CLIENTE");
            Console.ResetColor();

            Console.WriteLine();
        }

        public void MostrarHistorial(Cliente cliente)
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Yellow;

            _view.LetrasCentradas("====================================================");
            _view.LetrasCentradas($"HISTORIAL DE {cliente.Nombre.ToUpper()}   ");
            _view.LetrasCentradas("====================================================");

            Console.ResetColor();

            Console.WriteLine();

            Console.WriteLine($" DNI: {cliente.Dni}   Telefono: {cliente.Telefono}");
            Console.WriteLine();

            if (cliente.Prestamos.Count == 0)
            {
                MostrarError("No hay prestamos registrados");
                return;
            }

            Console.WriteLine("----------------------------------------------------------------");
            Console.WriteLine(" N°   PELICULA               PRESTADA     VENCE        ESTADO");
            Console.WriteLine("----------------------------------------------------------------");

            int contador = 1;

            foreach (Prestamo prestamo in cliente.Prestamos)
            {
                string estado = prestamo.Devuelto
                    ? $"Devuelta (recargo ${prestamo.RecargoCobrado})"
                    : "Activo";

                Console.WriteLine(
                    $" {contador,-4} {prestamo.PeliculaTitulo,-22} {prestamo.FechaPrestamo:dd/MM/yyyy}   {prestamo.FechaVencimiento:dd/MM/yyyy}   {estado}");
                contador++;
            }

            Console.WriteLine("----------------------------------------------------------------");
            Console.WriteLine($" Total de prestamos: {cliente.Prestamos.Count}");
            Console.WriteLine("----------------------------------------------------------------");
        }

        // ---------- PEDIR DATOS ----------

        // Pide nombre del cliente
        public string PedirNombreCliente()
        {
            Console.Write("Nombre del cliente: ");

            return Console.ReadLine();
        }

        // Pide DNI
        public string PedirDni()
        {
            Console.Write("DNI: ");

            return Console.ReadLine();
        }

        // Pide telefono
        public string PedirTelefono()
        {
            Console.Write("Telefono: ");

            return Console.ReadLine();
        }
    }
}
