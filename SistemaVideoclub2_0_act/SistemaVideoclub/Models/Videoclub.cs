using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVideoclub.Models
{
    public class Videoclub
    {
        public string Nombre { get; set; }
        public List<Pelicula> Peliculas { get; set; }
        public List<Cliente> Clientes { get; set; }
        public List<Prestamo> Prestamos { get; set; }
        public Videoclub(string nombre)
        {
            Nombre = nombre;
            Peliculas = new List<Pelicula>();
            Clientes = new List<Cliente>();
            Prestamos = new List<Prestamo>();
        }
        public void AgregarPelicula(Pelicula pelicula)
        {
            Peliculas.Add(pelicula);
        }
        public void AgregarCliente(Cliente cliente)
        {
            Clientes.Add(cliente);
        }
        public void AgregarPrestamo(Prestamo prestamo)
        {
            Prestamos.Add(prestamo);
        }
        public bool EliminarPelicula(Pelicula pelicula)
        {
            if (pelicula == null) return false;
            return Peliculas.Remove(pelicula);
        }
        public bool EliminarCliente(Cliente cliente)
        {
            if (cliente == null) return false;
            return Clientes.Remove(cliente);
        }

        public Pelicula BuscarPelicula(string titulo)
        {
            if (string.IsNullOrWhiteSpace(titulo))
                return null;

            return Peliculas.FirstOrDefault(p => p.Titulo.ToLower() == titulo.ToLower());
        }

      
        public Cliente BuscarCliente(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                return null;

            return Clientes.FirstOrDefault(c => c.Nombre.ToLower() == nombre.ToLower());
        }
    }
}
