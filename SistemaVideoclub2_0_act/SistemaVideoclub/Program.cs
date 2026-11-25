using SistemaVideoclub.Controllers;
using SistemaVideoclub.Models;
using SistemaVideoclub.Repository;

namespace SistemaVideoclub
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IRepository<Pelicula> repoPeliculas =
               new JsonRepository<Pelicula>("peliculas.json");

            IRepository<Cliente> repoClientes =
                new JsonRepository<Cliente>("clientes.json");

            IRepository<Prestamo> repoPrestamos =
                new JsonRepository<Prestamo>("prestamos.json");

            VideoclubController controller =
                new VideoclubController(repoPeliculas, repoClientes, repoPrestamos);

            controller.IniciarMenu();
        }
    }
}
