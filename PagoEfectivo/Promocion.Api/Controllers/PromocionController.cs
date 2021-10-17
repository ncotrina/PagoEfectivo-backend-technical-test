using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ApplicationServices.Dto.Promociones;
using ApplicationServices.Services;

namespace Promocion.Api.Controllers
{
    /// <summary>
    /// Controller Promocion
    /// </summary>
    [Route("api/v1/promociones")]
    public class PromocionController : ControllerBase
    {
        /// <summary>
        /// Servicio Promoción
        /// </summary>
        protected readonly IPromocionServices PromocionServices;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="promocionServices"></param>
        public PromocionController(IPromocionServices promocionServices)
        {
            PromocionServices = promocionServices;
        }
        /// <summary>
        /// Obtener todas las promociones
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getall")]
        public async Task<IActionResult> GetAll()
        {
            var result = await PromocionServices.GetAll();
            if (result.Success)
            {
                return Ok(result.DataList);
            }
            else
            {
                return BadRequest(result);
            }
        }

        /// <summary>
        /// Crear una nueva promoción
        /// </summary>
        /// <param name="promocionDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Insert(PromocionDto promocionDto)
        {
            promocionDto.PromocionEstadoId = PromocionEstadoDto.GENERADO;
            var result = await PromocionServices.Insert(promocionDto);

            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        /// <summary>
        /// Modificar una promoción - Canjear Promoción
        /// </summary>
        /// <param name="promocionDto"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("")]
        public async Task<IActionResult> Update(PromocionDto promocionDto)
        {
            promocionDto.PromocionEstadoId = PromocionEstadoDto.CANJEADO;
            var result = await PromocionServices.Update(promocionDto);

            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        /// <summary>
        /// Obtener una promoción por el codigo y estado
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("codigo-estado")]
        public async Task<IActionResult> GetByCodigo(string codigo)
        {
            var result = await PromocionServices.GetByCodigo(codigo);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            else
            {
                return BadRequest(result);
            }
        }
    }
}
