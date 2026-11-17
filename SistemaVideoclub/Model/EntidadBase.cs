namespace SistemaVideoclub.Model
{
    public abstract class EntidadBase
    {
        public int Id { get; set; }

        // Dos entidades del mismo tipo con el mismo Id son la misma
        public override bool Equals(object obj)
        {
            return obj is EntidadBase otra && GetType() == otra.GetType() && Id == otra.Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
