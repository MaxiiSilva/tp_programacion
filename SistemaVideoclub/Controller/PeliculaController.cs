using System;
using System.Collections.Generic;
using System.Linq;
using SistemaVideoclub.Model;
using SistemaVideoclub.Repository;

namespace SistemaVideoclub.Controller
{
    public class PeliculaController
    {
        private readonly IRepository<Pelicula> peliculas;
        private readonly IRepository<Prestamo> prestamos;

        public PeliculaController(IRepository<Pelicula> peliculas, IRepository<Prestamo> prestamos)
        {
            this.peliculas = peliculas;
            this.prestamos = prestamos;
        }

        public void Agregar(string titulo, string director, string genero, int anio, bool esEstreno)
        {
            Validar(titulo, director, anio);

            Pelicula pelicula = esEstreno ? new PeliculaEstreno() : new PeliculaCatalogo();
            pelicula.Titulo = titulo;
            pelicula.Director = director;
            pelicula.Genero = genero;
            pelicula.Anio = anio;

            peliculas.Agregar(pelicula);
        }

        public void Modificar(int id, string titulo, string director, string genero, int anio)
        {
            Pelicula pelicula = peliculas.ObtenerPorId(id) ?? throw new Exception("La película no existe.");
            Validar(titulo, director, anio);

            pelicula.Titulo = titulo;
            pelicula.Director = director;
            pelicula.Genero = genero;
            pelicula.Anio = anio;

            peliculas.Modificar(pelicula);
        }

        public void Eliminar(int id)
        {
            Pelicula pelicula = peliculas.ObtenerPorId(id) ?? throw new Exception("La película no existe.");
            if (!pelicula.Disponible)
                throw new Exception("No se puede eliminar una película que está prestada.");

            peliculas.Eliminar(id);
        }

        public List<Pelicula> ObtenerTodas() => peliculas.ObtenerTodos();

        public List<Pelicula> ObtenerDisponibles() => peliculas.Buscar(p => p.Disponible);

        public List<Pelicula> Buscar(string texto)
        {
            texto = (texto ?? "").ToLower();
            return peliculas.Buscar(p => p.Titulo.ToLower().Contains(texto)
                                      || p.Director.ToLower().Contains(texto)
                                      || p.Genero.ToLower().Contains(texto));
        }

        private void Validar(string titulo, string director, int anio)
        {
            if (string.IsNullOrWhiteSpace(titulo)) throw new Exception("El título es obligatorio.");
            if (string.IsNullOrWhiteSpace(director)) throw new Exception("El director es obligatorio.");
            if (anio < 1950 || anio > DateTime.Now.Year) throw new Exception("El año no es válido.");
        }
    }
}
