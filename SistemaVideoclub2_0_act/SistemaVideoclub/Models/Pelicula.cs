using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVideoclub.Models
{
    public class Pelicula : IRecargable, IEntidad 
    {
        public int id { get; set; }
        public string Titulo { get; set; } 
        public string Director { get; set; }
        public string Genero { get; set; }
        public int Anio { get; set; }
        public bool EsEstreno { get; set; } 
        public bool Disponible { get; set; } 
        public Pelicula(string titulo, string director, string genero, int anio, bool esEstreno)
        {
            Titulo = titulo;
            Director = director;
            Genero = genero;
            Anio = anio;
            EsEstreno = esEstreno;

            Disponible = true;
        }

        public int DiasPrestamo()
        {
            if (EsEstreno)
                return 2;

            return 5;
        }

        public decimal CalcularRecargo(int diasAtraso)
        {
            if (EsEstreno)
                return diasAtraso * 500;

            return diasAtraso * 200;
        }
    }
}
