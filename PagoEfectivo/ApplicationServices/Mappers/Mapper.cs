using ApplicationServices.Dto.Promociones;
using Domains.Entities;
using AutoMapper;
namespace ApplicationServices.Mappers
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<Promocion, PromocionDto>().ReverseMap()
                .ForPath(x => x.PromocionEstado.Descripcion, opt => opt.Ignore());
            CreateMap<PromocionEstado, PromocionEstadoDto>().ReverseMap();
        }
    }
}
