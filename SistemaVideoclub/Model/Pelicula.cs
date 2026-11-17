using System.Text.Json.Serialization;

namespace SistemaVideoclub.Model
{
    // Permite que el JSON guarde y recupere la subclase correcta
    [JsonPolymorphic(TypeDiscriminatorPropertyName = "tipo")]
    [JsonDerivedType(typeof(PeliculaEstreno), "estreno")]
    [JsonDerivedType(typeof(PeliculaCatalogo), "catalogo")]
    public abstract class Pelicula : EntidadBase
    {
        public string Titulo { get; set; }
        public string Director { get; set; }
        public string Genero { get; set; }
        public int Anio { get; set; }
        public bool Disponible { get; set; } = true;

        // Cada subclase define sus reglas (polimorfismo)
        [JsonIgnore] public abstract string Tipo { get; }
        [JsonIgnore] public abstract int DiasPrestamo { get; }
        [JsonIgnore] public abstract decimal RecargoPorDia { get; }

        public override string ToString()
        {
            string estado = Disponible ? "Disponible" : "Prestada";
            return $"[{Id}] {Titulo} - {Director} ({Anio}) - {Genero} - {Tipo} - {estado}";
        }
    }
}
