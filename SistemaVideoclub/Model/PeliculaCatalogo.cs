namespace SistemaVideoclub.Model
{
    public class PeliculaCatalogo : Pelicula
    {
        public override string Tipo => "Catálogo";
        public override int DiasPrestamo => 5;
        public override decimal RecargoPorDia => 200;
    }
}
