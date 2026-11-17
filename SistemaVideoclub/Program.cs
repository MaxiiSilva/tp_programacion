using SistemaVideoclub.Controller;
using SistemaVideoclub.Model;
using SistemaVideoclub.Repository;
using SistemaVideoclub.View;

namespace SistemaVideoclub
{
    class Program
    {
        static void Main(string[] args)
        {
            
            IRepository<Pelicula> peliculas = new JsonRepository<Pelicula>("peliculas.json");
            IRepository<Cliente> clientes = new JsonRepository<Cliente>("clientes.json");
            IRepository<Prestamo> prestamos = new JsonRepository<Prestamo>("prestamos.json");

            var peliculaCtrl = new PeliculaController(peliculas, prestamos);
            var clienteCtrl = new ClienteController(clientes, prestamos);
            var prestamoCtrl = new PrestamoController(prestamos, peliculas, clientes);

            new MenuView(peliculaCtrl, clienteCtrl, prestamoCtrl).Iniciar();
        }
    }
}
