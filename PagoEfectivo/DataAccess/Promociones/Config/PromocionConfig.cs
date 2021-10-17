
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Promociones.Config
{
    public class PromocionConfig
    {
        public PromocionConfig(EntityTypeBuilder<Domains.Entities.Promocion> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName("PromocionId");
            builder.Property(e => e.Email).HasMaxLength(250).IsRequired();
            builder.Property(e => e.Nombre).HasMaxLength(200).IsRequired();
            builder.Property(e => e.CodigoGenerado).HasMaxLength(10).IsRequired();
        }
    }
}
