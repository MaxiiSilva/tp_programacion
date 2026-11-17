using System;
using System.Text.Json.Serialization;

namespace SistemaVideoclub.Model
{
    // Guarda solo los Ids de cliente y película, para que el JSON no duplique datos
    public class Prestamo : EntidadBase
    {
        public int ClienteId { get; set; }
        public int PeliculaId { get; set; }
        public DateTime FechaPrestamo { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public DateTime? FechaDevolucion { get; set; }
        public decimal RecargoPorDia { get; set; }
        public decimal RecargoCobrado { get; set; }

        [JsonIgnore]
        public bool Activo => FechaDevolucion == null;

        // Constructor vacío: lo necesita el deserializador de JSON
        public Prestamo() { }

        // Polimorfismo: no importa si es Estreno o Catálogo, cada una sabe sus días y recargo
        public Prestamo(Cliente cliente, Pelicula pelicula)
        {
            ClienteId = cliente.Id;
            PeliculaId = pelicula.Id;
            FechaPrestamo = DateTime.Now;
            FechaVencimiento = FechaPrestamo.AddDays(pelicula.DiasPrestamo);
            RecargoPorDia = pelicula.RecargoPorDia;
        }

        public int DiasDeAtraso()
        {
            DateTime fin = FechaDevolucion ?? DateTime.Now;
            int dias = (fin.Date - FechaVencimiento.Date).Days;
            return dias > 0 ? dias : 0;
        }

        public void Devolver()
        {
            if (!Activo)
                throw new Exception("Ese préstamo ya fue devuelto.");

            FechaDevolucion = DateTime.Now;
            RecargoCobrado = DiasDeAtraso() * RecargoPorDia;
        }

        public override string ToString()
        {
            string estado = Activo ? "Activo" : "Devuelto";
            return $"[{Id}] Cliente {ClienteId} - Película {PeliculaId} - vence {FechaVencimiento:dd/MM/yyyy} - {estado}";
        }
    }
}
