using System.Collections.Generic;
using Domains.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Promociones.Config
{
   public class PromocionEstadoConfig
    {
        public PromocionEstadoConfig(EntityTypeBuilder<PromocionEstado> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName("PromocionEstadoId");
            builder.Property(e => e.Descripcion).HasMaxLength(50).IsRequired();

            #region Insert Data Default
            var promociones = new List<PromocionEstado>
            {
                new PromocionEstado
                {
                    Id = 1,
                    Descripcion = "Generado "
                },
                new PromocionEstado
                {
                    Id = 2,
                    Descripcion = "Canjeado"
                }
            };
            builder.HasData(promociones);
            #endregion
        }
    }
}
