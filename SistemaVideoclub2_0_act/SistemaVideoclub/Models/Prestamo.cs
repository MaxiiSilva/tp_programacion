using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVideoclub.Models
{
    public class Prestamo : IEntidad
    {
        public int id { get; set; } 
        public string ClienteNombre { get; set; } 
        public string PeliculaTitulo { get; set; } 
        public DateTime FechaPrestamo { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public DateTime? FechaDevolucion { get; set; } 
        public bool Devuelto { get; set; }
        public decimal RecargoCobrado { get; set; }
        
        public Prestamo(string clienteNombre, string peliculaTitulo, DateTime fechaPrestamo, DateTime fechaVencimiento)
        {
            ClienteNombre = clienteNombre;
            PeliculaTitulo = peliculaTitulo;
            FechaPrestamo = fechaPrestamo;
            FechaVencimiento = fechaVencimiento;

            Devuelto = false;
            RecargoCobrado = 0;
        }
        public int DiasDeAtraso(DateTime hasta)
        {
            int dias = (hasta.Date - FechaVencimiento.Date).Days;

            if (dias < 0)
                return 0;

            return dias;
        }
        public void RegistrarDevolucion(Pelicula pelicula, DateTime fechaDevolucion)
        {
            FechaDevolucion = fechaDevolucion;
            Devuelto = true;

            RecargoCobrado = pelicula.CalcularRecargo(DiasDeAtraso(fechaDevolucion));

            pelicula.Disponible = true;
        }
    }
}
