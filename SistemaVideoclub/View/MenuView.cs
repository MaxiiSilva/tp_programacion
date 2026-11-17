using System;
using System.Collections.Generic;
using SistemaVideoclub.Controller;

namespace SistemaVideoclub.View
{
    // Solo muestra datos y pide entradas. No tiene lógica de negocio ni acceso a datos.
    public class MenuView
    {
        private readonly PeliculaController peliculaCtrl;
        private readonly ClienteController clienteCtrl;
        private readonly PrestamoController prestamoCtrl;

        public MenuView(PeliculaController peliculaCtrl, ClienteController clienteCtrl, PrestamoController prestamoCtrl)
        {
            this.peliculaCtrl = peliculaCtrl;
            this.clienteCtrl = clienteCtrl;
            this.prestamoCtrl = prestamoCtrl;
        }

        public void Iniciar()
        {
            int opcion;
            do
            {
                Console.WriteLine("\n===== VIDEOCLUB =====");
                Console.WriteLine("1. Películas\n2. Clientes\n3. Préstamos\n0. Salir");
                opcion = PedirEntero("Opción: ");

                Ejecutar(() =>
                {
                    switch (opcion)
                    {
                        case 1: MenuPeliculas(); break;
                        case 2: MenuClientes(); break;
                        case 3: MenuPrestamos(); break;
                        case 0: Console.WriteLine("Hasta luego."); break;
                        default: Console.WriteLine("Opción inválida."); break;
                    }
                });
            } while (opcion != 0);
        }

        // ================= PELICULAS =================
        private void MenuPeliculas()
        {
            Console.WriteLine("\n--- PELÍCULAS ---");
            Console.WriteLine("1. Agregar\n2. Listar todas\n3. Listar disponibles\n4. Buscar\n5. Modificar\n6. Eliminar");
            switch (PedirEntero("Opción: "))
            {
                case 1:
                    string titulo = PedirTexto("Título: ");
                    string director = PedirTexto("Director: ");
                    string genero = PedirTexto("Género: ");
                    int anio = PedirEntero("Año: ");
                    bool estreno = PedirTexto("¿Es estreno? (s/n): ").ToLower() == "s";
                    peliculaCtrl.Agregar(titulo, director, genero, anio, estreno);
                    Console.WriteLine("Película agregada.");
                    break;
                case 2: Mostrar(peliculaCtrl.ObtenerTodas()); break;
                case 3: Mostrar(peliculaCtrl.ObtenerDisponibles()); break;
                case 4: Mostrar(peliculaCtrl.Buscar(PedirTexto("Buscar: "))); break;
                case 5:
                    Mostrar(peliculaCtrl.ObtenerTodas());
                    int idMod = PedirEntero("Id a modificar: ");
                    peliculaCtrl.Modificar(idMod, PedirTexto("Título: "), PedirTexto("Director: "),
                                           PedirTexto("Género: "), PedirEntero("Año: "));
                    Console.WriteLine("Película modificada.");
                    break;
                case 6:
                    Mostrar(peliculaCtrl.ObtenerTodas());
                    peliculaCtrl.Eliminar(PedirEntero("Id a eliminar: "));
                    Console.WriteLine("Película eliminada.");
                    break;
                default: Console.WriteLine("Opción inválida."); break;
            }
        }

        // ================= CLIENTES =================
        private void MenuClientes()
        {
            Console.WriteLine("\n--- CLIENTES ---");
            Console.WriteLine("1. Agregar\n2. Listar\n3. Buscar\n4. Modificar\n5. Eliminar\n6. Historial de préstamos");
            switch (PedirEntero("Opción: "))
            {
                case 1:
                    clienteCtrl.Agregar(PedirTexto("Nombre: "), PedirTexto("DNI: "), PedirTexto("Teléfono: "));
                    Console.WriteLine("Cliente agregado.");
                    break;
                case 2: Mostrar(clienteCtrl.ObtenerTodos()); break;
                case 3: Mostrar(clienteCtrl.Buscar(PedirTexto("Nombre o DNI: "))); break;
                case 4:
                    Mostrar(clienteCtrl.ObtenerTodos());
                    int idMod = PedirEntero("Id a modificar: ");
                    clienteCtrl.Modificar(idMod, PedirTexto("Nombre: "), PedirTexto("DNI: "), PedirTexto("Teléfono: "));
                    Console.WriteLine("Cliente modificado.");
                    break;
                case 5:
                    Mostrar(clienteCtrl.ObtenerTodos());
                    clienteCtrl.Eliminar(PedirEntero("Id a eliminar: "));
                    Console.WriteLine("Cliente eliminado.");
                    break;
                case 6:
                    Mostrar(clienteCtrl.ObtenerTodos());
                    var historial = clienteCtrl.ObtenerHistorial(PedirEntero("Id del cliente: "));
                    if (historial.Count == 0) Console.WriteLine("Sin préstamos.");
                    foreach (var p in historial) Console.WriteLine(prestamoCtrl.Describir(p));
                    break;
                default: Console.WriteLine("Opción inválida."); break;
            }
        }

        // ================= PRESTAMOS =================
        private void MenuPrestamos()
        {
            Console.WriteLine("\n--- PRÉSTAMOS ---");
            Console.WriteLine("1. Prestar película\n2. Devolver película\n3. Préstamos activos");
            switch (PedirEntero("Opción: "))
            {
                case 1:
                    Mostrar(clienteCtrl.ObtenerTodos());
                    Mostrar(peliculaCtrl.ObtenerDisponibles());
                    int clienteId = PedirEntero("Id del cliente: ");
                    int peliculaId = PedirEntero("Id de la película: ");
                    prestamoCtrl.Prestar(clienteId, peliculaId);
                    Console.WriteLine("Película prestada.");
                    break;
                case 2:
                    MostrarPrestamos();
                    decimal recargo = prestamoCtrl.Devolver(PedirEntero("Id del préstamo: "));
                    Console.WriteLine(recargo > 0 ? $"Devuelta con atraso. Recargo: ${recargo}" : "Devuelta en término.");
                    break;
                case 3: MostrarPrestamos(); break;
                default: Console.WriteLine("Opción inválida."); break;
            }
        }

        // ================= METODOS =================
        private void MostrarPrestamos()
        {
            var activos = prestamoCtrl.ObtenerActivos();
            if (activos.Count == 0) Console.WriteLine("No hay préstamos activos.");
            foreach (var p in activos) Console.WriteLine(prestamoCtrl.Describir(p));
        }

        private void Mostrar<T>(List<T> lista)
        {
            if (lista.Count == 0) Console.WriteLine("(sin resultados)");
            foreach (var item in lista) Console.WriteLine(item);
        }

        // Los controladores lanzan excepciones; la vista las captura y muestra el mensaje
        private void Ejecutar(Action accion)
        {
            try { accion(); }
            catch (Exception ex) { Console.WriteLine("Error: " + ex.Message); }
        }

        private string PedirTexto(string mensaje)
        {
            Console.Write(mensaje);
            return Console.ReadLine();
        }

        private int PedirEntero(string mensaje)
        {
            Console.Write(mensaje);
            int numero;
            while (!int.TryParse(Console.ReadLine(), out numero))
                Console.Write("Ingresá un número válido: ");
            return numero;
        }
    }
}
