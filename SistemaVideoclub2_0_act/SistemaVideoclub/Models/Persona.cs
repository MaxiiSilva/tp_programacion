using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVideoclub.Models
{
    public abstract class Persona
    {
        public string Nombre { get; set; }
        public string Dni { get; set; }

        public Persona (string nombre, string dni)
        {
            Nombre = nombre;
            Dni = dni;
        }
    }
}
