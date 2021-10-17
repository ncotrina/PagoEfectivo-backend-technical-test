using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationServices.Dto.Promociones;
using ApplicationServices.Utilities;
using Domains.Entities;
using Domains.Repositories;
using AutoMapper;
namespace ApplicationServices.Services
{
    public class PromocionServices : IPromocionServices
    {
        private readonly IPromocionRepository _promocionRepository;
        private readonly IPromocionEstadoRepository _promocionEstadoRepository;
        private readonly IMapper _mapper;

        public PromocionServices(IPromocionRepository promocionRepository, IMapper mapper, IPromocionEstadoRepository promocionEstadoRepository)
        {
            _promocionRepository = promocionRepository;
            _mapper = mapper;
            _promocionEstadoRepository = promocionEstadoRepository;
        }

        public async Task<ServiceResponse<bool>> Insert(PromocionDto promocionDto)
        {
            var serviceResponse = new ServiceResponse<bool> { Messages = new List<string>() };
            try
            {
                var validate = await _promocionRepository.ValidateDuplicate(promocionDto.Email);
                if (!validate)
                {

                    var promocion = _mapper.Map<Promocion>(promocionDto);
                    var estado = await _promocionEstadoRepository.Get(promocionDto.PromocionEstadoId);
                    if (estado == null)
                    {
                        serviceResponse.Messages.Add("No se encontró el estado de la promoción!");
                        return serviceResponse;
                    }
                    promocion.PromocionEstado = estado;
                    promocion.CodigoGenerado = promocion.GenerarCodigo();
                    var validates = promocion.ValidateInsert();
                    if (validates.Any())
                    {
                        serviceResponse.Messages = validates;
                        return serviceResponse;
                    }
                    await _promocionRepository.Insert(promocion);
                    serviceResponse.Messages.Add(
                        $"La promoción se guardó correctamente! El código de Promoción es: {promocion.CodigoGenerado}");
                    serviceResponse.Success = true;
                }
                else
                {
                    serviceResponse.Messages.Add("Ya existe un código para el usuario ingresado!");
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Messages.Add(ex.Message);
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<PromocionDto>> GetAll()
        {
            var serviceResponse = new ServiceResponse<PromocionDto> { Messages = new List<string>() };
            try
            {
                var promociones = await _promocionRepository.GetAll();
                var mapper = _mapper.Map<IList<PromocionDto>>(promociones.ToList());
                serviceResponse.DataList = mapper;
                serviceResponse.Success = true;
            }
            catch (Exception ex)
            {
                serviceResponse.Messages.Add(ex.Message);
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<PromocionDto>> GetByCodigo(string codigo)
        {
            var serviceResponse = new ServiceResponse<PromocionDto>{Messages = new List<string>()};
            try
            {
                var promocion = await _promocionRepository.GetByCodigo(codigo);
                if (promocion == null)
                {
                    serviceResponse.Messages.Add("No se encontró la promoción por código");
                    return serviceResponse;
                }
                var mapper = _mapper.Map<PromocionDto>(promocion);
                serviceResponse.Data = mapper;
                serviceResponse.Success = true;
            }
            catch (Exception ex)
            {
                serviceResponse.Messages.Add(ex.Message);
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<bool>> Update(PromocionDto promocionDto)
        {
            var serviceResponse = new ServiceResponse<bool> { Messages = new List<string>() };
            try
            {
                var estado = await _promocionEstadoRepository.Get(promocionDto.PromocionEstadoId);
                if (estado == null)
                {
                    serviceResponse.Messages.Add("No se encontró el estado de la promoción!");
                    return serviceResponse;
                }
                var promocionPersited = await _promocionRepository.GetByCodigo(promocionDto.CodigoGenerado);
                if (promocionPersited == null)
                {
                    serviceResponse.Messages.Add("No se encontró la promoción!");
                    return serviceResponse;
                }

                promocionPersited.PromocionEstado = estado;
                var validates = promocionPersited.ValidateCanjear();
                if (validates.Any())
                {
                    serviceResponse.Messages = validates;
                    return serviceResponse;
                }
                await _promocionRepository.Update(promocionPersited);
                serviceResponse.Messages.Add("La promoción se actualizó correctamente!");
                serviceResponse.Success = true;
            }
            catch (Exception ex)
            {
                serviceResponse.Messages.Add(ex.Message);
            }
            return serviceResponse;
        }
    }
}
