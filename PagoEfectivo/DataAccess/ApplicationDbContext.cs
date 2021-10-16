using DataAccess.Promociones.Config;
using Domains.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
   public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Promocion> Promociones { get; set; }
        public DbSet<PromocionEstado> PromocionEstados { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            ModelConfig(builder);
        }
        private void ModelConfig(ModelBuilder builder)
        {
            _ = new PromocionConfig(builder.Entity<Promocion>());
            _ = new PromocionEstadoConfig(builder.Entity<PromocionEstado>());
        }
    }
}
