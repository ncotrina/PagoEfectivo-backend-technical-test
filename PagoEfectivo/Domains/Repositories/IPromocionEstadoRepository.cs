using System.Threading.Tasks;
using Domains.Entities;

namespace Domains.Repositories
{
    public interface IPromocionEstadoRepository
    {
        Task<PromocionEstado> Get(int id);
    }
}
