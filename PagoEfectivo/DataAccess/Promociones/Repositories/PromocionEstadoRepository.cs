using System.Threading.Tasks;
using Domains.Entities;
using Domains.Repositories;

namespace DataAccess.Promociones.Repositories
{
    public class PromocionEstadoRepository : IPromocionEstadoRepository
    {
        private readonly ApplicationDbContext _context;
        public PromocionEstadoRepository(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }
        public async Task<PromocionEstado> Get(int id)
        {
            return await _context.PromocionEstados.FindAsync(id);
        }
    }
}
