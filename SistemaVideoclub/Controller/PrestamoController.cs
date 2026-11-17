using System;
using System.Collections.Generic;
using SistemaVideoclub.Model;
using SistemaVideoclub.Repository;

namespace SistemaVideoclub.Controller
{
    public class PrestamoController
    {
        private readonly IRepository<Prestamo> prestamos;
        private readonly IRepository<Pelicula> peliculas;
        private readonly IRepository<Cliente> clientes;

        public PrestamoController(IRepository<Prestamo> prestamos, IRepository<Pelicula> peliculas, IRepository<Cliente> clientes)
        {
            this.prestamos = prestamos;
            this.peliculas = peliculas;
            this.clientes = clientes;
        }

        public void Prestar(int clienteId, int peliculaId)
        {
            Cliente cliente = clientes.ObtenerPorId(clienteId) ?? throw new Exception("El cliente no existe.");
            Pelicula pelicula = peliculas.ObtenerPorId(peliculaId) ?? throw new Exception("La película no existe.");

            if (!pelicula.Disponible)
                throw new Exception("La película no está disponible.");

            pelicula.Disponible = false;
            peliculas.Modificar(pelicula);
            prestamos.Agregar(new Prestamo(cliente, pelicula));
        }

        // Devuelve el recargo cobrado para que la vista lo muestre
        public decimal Devolver(int prestamoId)
        {
            Prestamo prestamo = prestamos.ObtenerPorId(prestamoId) ?? throw new Exception("El préstamo no existe.");

            prestamo.Devolver(); // lanza excepción si ya estaba devuelto
            prestamos.Modificar(prestamo);

            Pelicula pelicula = peliculas.ObtenerPorId(prestamo.PeliculaId);
            if (pelicula != null)
            {
                pelicula.Disponible = true;
                peliculas.Modificar(pelicula);
            }

            return prestamo.RecargoCobrado;
        }

        public List<Prestamo> ObtenerActivos() => prestamos.Buscar(p => p.Activo);

        public List<Prestamo> ObtenerTodos() => prestamos.ObtenerTodos();

        // Para mostrar nombres en lugar de Ids
        public string Describir(Prestamo p)
        {
            Cliente c = clientes.ObtenerPorId(p.ClienteId);
            Pelicula pel = peliculas.ObtenerPorId(p.PeliculaId);
            string cliente = c != null ? c.Nombre : "(cliente eliminado)";
            string titulo = pel != null ? pel.Titulo : "(película eliminada)";
            string estado = p.Activo ? "Activo" : $"Devuelto (recargo ${p.RecargoCobrado})";
            return $"[{p.Id}] {titulo} -> {cliente} - vence {p.FechaVencimiento:dd/MM/yyyy} - {estado}";
        }
    }
}
