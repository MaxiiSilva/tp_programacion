using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVideoclub.Models
{
    
    public class Cliente : Persona, IEntidad
    {
        public int id { get; set; }

        public string Telefono { get; set; } 

        public List<Prestamo> Prestamos { get; set; } 

        public Cliente(string nombre, string dni, string telefono) : base(nombre, dni)
        {
            Telefono = telefono;
            Prestamos = new List<Prestamo>();
        }
        public void AgregarPrestamo(Prestamo prestamo)
        {
            Prestamos.Add(prestamo);
        }
    }
}
