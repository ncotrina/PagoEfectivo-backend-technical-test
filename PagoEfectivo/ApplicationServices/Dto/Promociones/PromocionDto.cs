namespace ApplicationServices.Dto.Promociones
{
    public class PromocionDto
    {
        public int Id { get; set; }
        public string CodigoGenerado { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public int PromocionEstadoId { get; set; }
        public virtual PromocionEstadoDto PromocionEstado { get; set; }
    }
}
