using System.Collections.Generic;
using System.Threading.Tasks;
using Domains.Entities;

namespace Domains.Repositories
{
    public interface IPromocionRepository
    {
        Task Insert(Promocion promocion);
        Task<bool> ValidateDuplicate(string email);
        Task<IEnumerable<Promocion>> GetAll();
        Task<Promocion> Get(int id);
        Task Update(Promocion promocion);
        Task<Promocion> GetByCodigo(string codigo);
    }
}
