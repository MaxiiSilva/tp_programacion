using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaVideoclub.Models;

namespace SistemaVideoclub.Views
{
    public class PeliculaView
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

        // Pantalla inicial de agregar pelicula
        public void MostrarPantallaAgregar(List<Pelicula> peliculas)
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Cyan;

            _view.LetrasCentradas("====================================================");
            _view.LetrasCentradas("               AGREGAR NUEVA PELICULA               ");
            _view.LetrasCentradas("====================================================");

            Console.ResetColor();

            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Yellow;
            _view.LetrasCentradas("PELICULAS REGISTRADAS");
            Console.ResetColor();

            Console.WriteLine();

            foreach (Pelicula pelicula in peliculas)
            {
                _view.LetrasCentradas("- " + pelicula.Titulo);
            }

            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Green;
            _view.LetrasCentradas("INGRESE EL TITULO DE LA NUEVA PELICULA");
            Console.ResetColor();

            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Red;
            _view.LetrasCentradas("ESCRIBA 0 PARA VOLVER AL MENU");
            Console.ResetColor();

            Console.WriteLine();
        }

        // Mensaje de éxito en la pantalla
        public void MostrarPeliculaAgregada(Pelicula pelicula)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine();
            _view.LetrasCentradas("========================================");
            _view.LetrasCentradas("PELICULA AGREGADA CORRECTAMENTE");
            _view.LetrasCentradas($"Titulo: {pelicula.Titulo}");
            _view.LetrasCentradas($"Director: {pelicula.Director}");
            _view.LetrasCentradas($"Genero: {pelicula.Genero} ({pelicula.Anio})");
            _view.LetrasCentradas(pelicula.EsEstreno ? "Tipo: Estreno" : "Tipo: Catalogo");
            _view.LetrasCentradas($"Dias de prestamo: {pelicula.DiasPrestamo()}");
            _view.LetrasCentradas("========================================");
            Console.ResetColor();
        }

        // ---------- LISTAR ----------

        public void MostrarListado(List<Pelicula> peliculas)
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Cyan;

            _view.LetrasCentradas("====================================================");
            _view.LetrasCentradas("                LISTADO DE PELICULAS                ");
            _view.LetrasCentradas("====================================================");

            Console.ResetColor();

            Console.WriteLine();
            Console.WriteLine();

            _view.LetrasCentradas("------------------------------------------------------------------");
            _view.LetrasCentradas(" N°   TITULO                    GENERO           AÑO   TIPO      ESTADO");
            _view.LetrasCentradas("------------------------------------------------------------------");

            int posicion = 1;

            foreach (Pelicula pelicula in peliculas)
            {
                string tipo = pelicula.EsEstreno ? "Estreno" : "Catalogo";
                string estado = pelicula.Disponible ? "Disponible" : "Prestada";

                _view.LetrasCentradas(
                    $" {posicion,-3}  {pelicula.Titulo,-25} {pelicula.Genero,-15} {pelicula.Anio,4}  {tipo,-9} {estado}"
                );

                posicion++;
            }

            _view.LetrasCentradas("------------------------------------------------------------------");
            _view.LetrasCentradas($" Total de peliculas registradas: {peliculas.Count}");
            _view.LetrasCentradas("------------------------------------------------------------------");
        }

        // ---------- BUSCAR ----------

        public void MostrarPantallaBuscar(List<Pelicula> peliculas)
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Cyan;

            _view.LetrasCentradas("====================================================");
            _view.LetrasCentradas("                  BUSCAR PELICULA");
            _view.LetrasCentradas("====================================================");

            Console.ResetColor();

            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Yellow;
            _view.LetrasCentradas("PELICULAS DISPONIBLES");
            Console.ResetColor();

            Console.WriteLine();

            foreach (Pelicula pelicula in peliculas)
            {
                _view.LetrasCentradas("- " + pelicula.Titulo);
            }

            Console.WriteLine();
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Green;
            _view.LetrasCentradas("INGRESE EL TITULO DE LA PELICULA");
            Console.ResetColor();

            Console.WriteLine();
        }

        public void MostrarInformacion(Pelicula encontrada)
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Green;

            _view.LetrasCentradas("====================================================");
            _view.LetrasCentradas($"        INFORMACION DE {encontrada.Titulo.ToUpper()}");
            _view.LetrasCentradas("====================================================");

            Console.ResetColor();

            Console.WriteLine();

            _view.LetrasCentradas($"Titulo   : {encontrada.Titulo}");
            _view.LetrasCentradas($"Director : {encontrada.Director}");
            _view.LetrasCentradas($"Genero   : {encontrada.Genero}");
            _view.LetrasCentradas($"Anio     : {encontrada.Anio}");
            _view.LetrasCentradas($"Tipo     : {(encontrada.EsEstreno ? "Estreno" : "Catalogo")}");
            _view.LetrasCentradas($"Dias     : {encontrada.DiasPrestamo()}");
            _view.LetrasCentradas($"Recargo  : ${encontrada.CalcularRecargo(1)} por dia de atraso");
            _view.LetrasCentradas($"Estado   : {(encontrada.Disponible ? "Disponible" : "Prestada")}");

            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Cyan;
            _view.LetrasCentradas("====================================================");
            Console.ResetColor();
        }

        // ---------- ELIMINAR ----------

        public void MostrarPantallaEliminar(List<Pelicula> peliculas)
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Red;

            _view.LetrasCentradas("====================================================");
            _view.LetrasCentradas("                ELIMINAR PELICULA");
            _view.LetrasCentradas("====================================================");

            Console.ResetColor();

            Console.WriteLine();

            // Mostrar todas las peliculas con su ID
            foreach (Pelicula pelicula in peliculas)
            {
                _view.LetrasCentradas($"ID: {pelicula.id} - {pelicula.Titulo}");
            }

            Console.WriteLine();
        }

        // ---------- ACTUALIZAR ----------

        public void MostrarPantallaActualizar(List<Pelicula> peliculas)
        {
            Console.Clear();

            _view.LetrasCentradas("ACTUALIZAR PELICULA");

            foreach (Pelicula pelicula in peliculas)
            {
                _view.LetrasCentradas($"ID: {pelicula.id} - {pelicula.Titulo}");
            }
        }

        // Pide el id de la pelicula (accion: "eliminar" o "modificar")
        public string PedirId(string accion)
        {
            Console.Write($"Ingrese ID de la pelicula a {accion}: ");

            return Console.ReadLine();
        }

        public string PedirNuevoTitulo()
        {
            Console.Write("Nuevo titulo: ");

            return Console.ReadLine();
        }

        public string PedirNuevoDirector()
        {
            Console.WriteLine();

            Console.Write("Nuevo director: ");

            return Console.ReadLine();
        }

        // ---------- PEDIR DATOS ----------

        // Pide titulo de la pelicula
        public string PedirTitulo()
        {
            Console.Write("Titulo de la pelicula: ");
            return Console.ReadLine();
        }

        // Pide director
        public string PedirDirector()
        {
            Console.Write("Director: ");

            return Console.ReadLine();
        }

        public string PedirGenero()
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Seleccione el genero:");
                Console.WriteLine("1 - Accion");
                Console.WriteLine("2 - Comedia");
                Console.WriteLine("3 - Drama");
                Console.WriteLine("4 - Terror");
                Console.WriteLine("5 - Ciencia ficcion");
                Console.Write("Opcion: ");

                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        return "Accion";

                    case "2":
                        return "Comedia";

                    case "3":
                        return "Drama";

                    case "4":
                        return "Terror";

                    case "5":
                        return "Ciencia ficcion";

                    default:
                        MostrarError("Opcion invalida");
                        break;
                }
            }
        }

        // Pide año de estreno
        public int PedirAnio()
        {
            int anio;

            Console.WriteLine();

            Console.Write("Anio de estreno: ");

            while (!int.TryParse(Console.ReadLine(), out anio))
            {
                MostrarError("Ingrese un numero valido");
                Console.Write("Anio de estreno: ");
            }

            return anio;
        }

        // Pide si es estreno o catalogo
        public bool PedirEsEstreno()
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Seleccione el tipo de pelicula:");
                Console.WriteLine("1 - Estreno");
                Console.WriteLine("2 - Catalogo");
                Console.Write("Opcion: ");

                string opcion = Console.ReadLine();

                if (opcion == "1")
                    return true;

                if (opcion == "2")
                    return false;

                MostrarError("Opcion invalida");
            }
        }
    }
}
