
using System.Threading.Tasks;
using ApplicationServices.Dto.Promociones;
using ApplicationServices.Utilities;

namespace ApplicationServices.Services
{
    public interface IPromocionServices
    {
        Task<ServiceResponse<bool>> Insert(PromocionDto promocionDto);
        Task<ServiceResponse<PromocionDto>> GetAll();
        Task<ServiceResponse<PromocionDto>> GetByCodigo(string codigo);
        Task<ServiceResponse<bool>> Update(PromocionDto promocionDto);
    }
}
