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
    public class PeliculaController
    {
        private Videoclub _videoclub;
        private VideoclubView _view;
        private PeliculaView _peliculaView;
        private IRepository<Pelicula> _repo;

        public PeliculaController(Videoclub videoclub, IRepository<Pelicula> repo)
        {
            _videoclub = videoclub;
            _view = new VideoclubView();
            _repo = repo;
            _peliculaView = new PeliculaView();
        }

        public void AgregarPelicula()
        {
            _peliculaView.MostrarPantallaAgregar(_videoclub.Peliculas);

            string titulo;

            while (true)
            {

                titulo = _peliculaView.PedirTitulo();

                if (titulo == "0")
                {
                    return;
                }

                if (string.IsNullOrWhiteSpace(titulo))
                {
                    _view.MostrarError("El titulo no puede estar vacio");
                    continue;
                }

                if (_videoclub.BuscarPelicula(titulo) != null)
                {
                    _view.MostrarError("Esa pelicula ya existe");
                    continue;
                }

                break;
            }

            string director = _peliculaView.PedirDirector();

            if (string.IsNullOrWhiteSpace(director))
            {
                _view.MostrarError("El director no puede estar vacio");
                return;
            }

            string genero = _peliculaView.PedirGenero();

            int anio;
            while (true)
            {
                anio = _peliculaView.PedirAnio();

                if (anio < 1900 || anio > DateTime.Now.Year)
                {
                    _view.MostrarError($"El anio debe estar entre 1900 y {DateTime.Now.Year}");
                    continue;
                }
                break;
            }

            bool esEstreno = _peliculaView.PedirEsEstreno();

            Pelicula nuevaPelicula = new Pelicula(titulo, director, genero, anio, esEstreno); 

            _videoclub.AgregarPelicula(nuevaPelicula);

            _repo.Agregar(nuevaPelicula);

            _view.MostrarMensaje($"La pelicula '{titulo}' fue agregada correctamente");

            _peliculaView.MostrarPeliculaAgregada(nuevaPelicula);
        }

        public void ListarPeliculas()
        {
            _peliculaView.MostrarListado(
                _videoclub.Peliculas.OrderBy(p => p.Titulo).ToList()
            );
        }

        public void BuscarPelicula()
        {
            _peliculaView.MostrarPantallaBuscar(_videoclub.Peliculas);

            string titulo = _peliculaView.PedirTitulo();

            Pelicula encontrada = _videoclub.BuscarPelicula(titulo);

            if (encontrada == null)
            {
                _view.MostrarError("Pelicula no encontrada");
                return;
            }

            _peliculaView.MostrarInformacion(encontrada);
        }

        public void EliminarPelicula()
        {
            _peliculaView.MostrarPantallaEliminar(_videoclub.Peliculas);

            if (!int.TryParse(_peliculaView.PedirId("eliminar"), out int id))
            {
                _view.MostrarError("ID inválido");
                return;
            }

            Pelicula peliculaEliminar = _repo.BuscarPorId(id);

            if (peliculaEliminar == null)
            {
                _view.MostrarError("Pelicula no encontrada");
                return;
            }

            _videoclub.EliminarPelicula(
                _videoclub.Peliculas.FirstOrDefault(p => p.id == id));

            _repo.Eliminar(id);

            _videoclub.Peliculas = _repo.LeerTodos();

            _view.MostrarMensaje($"Pelicula '{peliculaEliminar.Titulo}' eliminada correctamente");
        }

        public void ActualizarPelicula()
        {
            _peliculaView.MostrarPantallaActualizar(_videoclub.Peliculas);

            if (!int.TryParse(_peliculaView.PedirId("modificar"), out int id))
            {
                _view.MostrarError("ID inválido");
                return;
            }

            Pelicula peliculaEdit = _repo.BuscarPorId(id);

            if (peliculaEdit == null)
            {
                _view.MostrarError("Pelicula no encontrada");
                return;
            }

            string nuevoTitulo = _peliculaView.PedirNuevoTitulo();

            if (!string.IsNullOrWhiteSpace(nuevoTitulo))
            {
                Pelicula repetida = _videoclub.BuscarPelicula(nuevoTitulo);

                if (repetida != null && repetida.id != peliculaEdit.id)
                {
                    _view.MostrarError("Ya existe otra pelicula con ese titulo");
                    return;
                }

                peliculaEdit.Titulo = nuevoTitulo;
            }

            string nuevoDirector = _peliculaView.PedirNuevoDirector();

            if (!string.IsNullOrWhiteSpace(nuevoDirector))
            {
                peliculaEdit.Director = nuevoDirector;
            }

            peliculaEdit.Genero = _peliculaView.PedirGenero();

            int nuevoAnio;
            while (true)
            {
                nuevoAnio = _peliculaView.PedirAnio();

                if (nuevoAnio < 1900 || nuevoAnio > DateTime.Now.Year)
                {
                    _view.MostrarError($"El anio debe estar entre 1900 y {DateTime.Now.Year}");
                    continue;
                }
                break;
            }

            peliculaEdit.Anio = nuevoAnio;

            peliculaEdit.EsEstreno = _peliculaView.PedirEsEstreno();

            _repo.Actualizar(peliculaEdit);

            _videoclub.Peliculas = _repo.LeerTodos();

            _view.MostrarMensaje("Pelicula actualizada correctamente");
        }

    }
}
