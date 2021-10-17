namespace Domains.Entities
{
    public class PromocionEstado
    {
        public const int Generado = 1;
        public const int Canjeado = 2;
        public int Id { get; set; }
        public string Descripcion { get; set; }
    }
}
