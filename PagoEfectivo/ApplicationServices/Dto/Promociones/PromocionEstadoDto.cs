using Domains.Entities;

namespace ApplicationServices.Dto.Promociones
{
    public class PromocionEstadoDto
    {

        public const int GENERADO = PromocionEstado.Generado;
        public const int CANJEADO = PromocionEstado.Canjeado;
        public int Id { get; set; }
        public string Descripcion { get; set; }
    }
}
