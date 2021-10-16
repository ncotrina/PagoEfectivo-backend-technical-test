using System.Collections.Generic;
using System.Threading.Tasks;
using Domains.Entities;
using Domains.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Promociones.Repositories
{
    public class PromocionRepository : IPromocionRepository
    {
        private readonly ApplicationDbContext _context;
        public PromocionRepository(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }
        public async Task Insert(Promocion promocion)
        {
            await _context.Promociones.AddAsync(promocion);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ValidateDuplicate(string email)
        {
            return await _context.Promociones.AnyAsync(x => x.Email == email);
        }

        public async Task<IEnumerable<Promocion>> GetAll()
        {
            return await _context.Promociones.Include(x => x.PromocionEstado).ToListAsync();
        }

        public async Task<Promocion> Get(int id)
        {
            return await _context.Promociones.FindAsync(id);
        }
        public async Task<Promocion> GetByCodigo(string codigo)
        {
            return await _context.Promociones.FirstOrDefaultAsync(x => x.CodigoGenerado == codigo &&
                                                                               x.PromocionEstado.Id == PromocionEstado.Generado);
        }
        public async Task Update(Promocion promocion)
        {
            _context.Promociones.Update(promocion);
            await _context.SaveChangesAsync();
        }
    }
}
