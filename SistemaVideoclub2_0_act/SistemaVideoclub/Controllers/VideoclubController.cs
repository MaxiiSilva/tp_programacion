using SistemaVideoclub.Models;
using SistemaVideoclub.Views;
using SistemaVideoclub.Repository;

namespace SistemaVideoclub.Controllers
{
    public class VideoclubController
    {
        private Videoclub _videoclub;
        private VideoclubView _view;


        private IRepository<Pelicula> _repoPeliculas;
        private IRepository<Cliente> _repoClientes;
        private IRepository<Prestamo> _repoPrestamos;

        private PeliculaController _peliculaController;
        private ClienteController _clienteController;
        private PrestamoController _prestamoController;

        public VideoclubController(IRepository<Pelicula> repoPeliculas, IRepository<Cliente> repoClientes, IRepository<Prestamo> repoPrestamos)
        {
            _repoPeliculas = repoPeliculas;
            _repoClientes = repoClientes;
            _repoPrestamos = repoPrestamos;

            _videoclub = new Videoclub("Videoclub");

            _view = new VideoclubView();

            _peliculaController = new PeliculaController(_videoclub, _repoPeliculas);
            _clienteController = new ClienteController(_videoclub, _repoClientes);
            _prestamoController = new PrestamoController(_videoclub, _repoPeliculas, _repoPrestamos);

            _view.MostrarBienvenida();

            CargarPeliculas();
            CargarClientes();
            CargarPrestamos();

        }

        private void CargarPeliculas()
        {
            var peliculas = _repoPeliculas.LeerTodos();

            if (peliculas != null && peliculas.Count > 0)
            {
                foreach (var p in peliculas)
                {
                    _videoclub.AgregarPelicula(p);
                }
            }
        }

        
        private void CargarClientes()
        {
            var clientes = _repoClientes.LeerTodos();

            if (clientes != null && clientes.Count > 0)
            {
                foreach (var c in clientes)
                {
                    _videoclub.AgregarCliente(c);
                }
            }
        }


        private void CargarPrestamos()
        {

            var prestamos = _repoPrestamos.LeerTodos();

            foreach (var p in prestamos)
            {
                _videoclub.AgregarPrestamo(p);

                var cliente = _videoclub.BuscarCliente(p.ClienteNombre);

                if (cliente != null)
                {
                    cliente.AgregarPrestamo(p);
                }
            }
        }



        public void IniciarMenu()
        {
            string opcion;

            do
            {
                opcion = _view.MostrarMenu();

                switch (opcion)
                {
                    case "1":
                        _view.Limpiar();
                        _peliculaController.AgregarPelicula();
                        break;

                    case "2":
                        _view.Limpiar();
                        _peliculaController.ListarPeliculas();
                        break;

                    case "3":
                        _view.Limpiar();
                        _peliculaController.BuscarPelicula();
                        break;

                    case "4":
                        _view.Limpiar();
                        _clienteController.AgregarCliente();
                        break;

                    case "5":
                        _view.Limpiar();
                        _prestamoController.RegistrarPrestamo();
                        break;

                    case "6":
                        _view.Limpiar();
                        _prestamoController.VerPrestamosActivos();
                        break;

                    case "7":
                        _view.Limpiar();
                        _peliculaController.EliminarPelicula();
                        break;

                    case "8":
                        _view.Limpiar();
                        _clienteController.VerClientes();
                        break;

                    case "9":
                        _view.Limpiar();
                        _clienteController.EliminarCliente();
                        break;
                    case "10":
                        _view.Limpiar();
                        _peliculaController.ActualizarPelicula();
                        break;
                    case "11":
                        _view.Limpiar();
                        _clienteController.ActualizarCliente();
                        break;
                    case "12":
                        _view.Limpiar();
                        _prestamoController.RegistrarDevolucion();
                        break;

                    case "0":
                        _view.MostrarMensaje("Hasta luego");
                        break;

                    default:
                        _view.MostrarError("Opcion invalida");
                        break;
                }

                if (opcion != "0")
                {
                    _view.Pausa();
                }

            } while (opcion != "0");
        }
    }
}
