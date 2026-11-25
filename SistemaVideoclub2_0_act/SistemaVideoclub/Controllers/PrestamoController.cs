using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaVideoclub.Models;
using SistemaVideoclub.Repository;
using SistemaVideoclub.Views;

namespace SistemaVideoclub.Controllers
{
    public class PrestamoController
    {
        private Videoclub _videoclub;
        private VideoclubView _view;
        private IRepository<Pelicula> _repoPeliculas;
        private IRepository<Prestamo> _repoPrestamos;
        private PrestamoView _prestamoView;

        public PrestamoController(Videoclub videoclub, IRepository<Pelicula> repoPeliculas, IRepository<Prestamo> repoPrestamos)
        {
            _videoclub = videoclub;
            _view = new VideoclubView();
            _repoPeliculas = repoPeliculas;
            _repoPrestamos = repoPrestamos;
            _prestamoView = new PrestamoView();
        }
        public void RegistrarPrestamo()
        {
            _prestamoView.MostrarPantallaPrestamo(_videoclub.Peliculas, _videoclub.Clientes);

            string nombrePelicula = _prestamoView.PedirPelicula();

            if (nombrePelicula == "0")
                return;

            string nombreCliente = _prestamoView.PedirCliente();

            if (nombreCliente == "0")
                return;

            Pelicula peliculaPrestada = _videoclub.BuscarPelicula(nombrePelicula);
            Cliente clientePrestamo = _videoclub.BuscarCliente(nombreCliente);

            if (peliculaPrestada == null || clientePrestamo == null)
            {
                _view.MostrarError("La pelicula o el cliente no existen");
                return;
            }

            if (!peliculaPrestada.Disponible)
            {
                _view.MostrarError("Esa pelicula ya esta prestada");
                return;
            }

            DateTime hoy = DateTime.Now;

            Prestamo prestamo = new Prestamo(
                clientePrestamo.Nombre,
                peliculaPrestada.Titulo,
                hoy,
                hoy.AddDays(peliculaPrestada.DiasPrestamo())
            );

            peliculaPrestada.Disponible = false;

            _videoclub.AgregarPrestamo(prestamo);
            clientePrestamo.AgregarPrestamo(prestamo);

            _repoPrestamos.Agregar(prestamo);

            _repoPeliculas.GuardarTodos(_videoclub.Peliculas);

            _prestamoView.MostrarPrestamoRegistrado(prestamo, peliculaPrestada);
        }

        public void VerPrestamosActivos()
        {
            _prestamoView.MostrarPrestamosActivos(
                _videoclub.Prestamos.Where(p => !p.Devuelto).ToList()
            );
        }

        public void RegistrarDevolucion()
        {
            _prestamoView.MostrarPantallaDevolucion(
                _videoclub.Prestamos.Where(p => !p.Devuelto).ToList()
            );

            int id = _prestamoView.PedirIdPrestamo();

            Prestamo prestamo = _videoclub.Prestamos.FirstOrDefault(p => p.id == id && !p.Devuelto);

            if (prestamo == null)
            {
                _view.MostrarError("Prestamo activo no encontrado");
                return;
            }

            Pelicula pelicula = _videoclub.BuscarPelicula(prestamo.PeliculaTitulo);

            if (pelicula == null)
            {
                _view.MostrarError("La pelicula de este prestamo ya no existe");
                return;
            }

            prestamo.RegistrarDevolucion(pelicula, DateTime.Now);

            _repoPrestamos.Actualizar(prestamo);
            _repoPeliculas.GuardarTodos(_videoclub.Peliculas);

            _prestamoView.MostrarDevolucionRegistrada(prestamo);
        }
    }
}
