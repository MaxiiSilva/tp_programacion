namespace SistemaVideoclub.Model
{
    public class PeliculaEstreno : Pelicula
    {
        public override string Tipo => "Estreno";
        public override int DiasPrestamo => 2;
        public override decimal RecargoPorDia => 500;
    }
}
