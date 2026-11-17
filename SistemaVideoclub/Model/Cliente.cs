namespace SistemaVideoclub.Model
{
    public class Cliente : Persona
    {
        public string Telefono { get; set; }

        public override string ToString()
        {
            return $"[{Id}] {Nombre} - DNI: {Dni} - Tel: {Telefono}";
        }
    }
}
