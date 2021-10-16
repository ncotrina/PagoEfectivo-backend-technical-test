using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebPagoEfectivo.Controllers.Promociones
{
    public class PromocionController : Controller
    {
        private readonly ILogger<PromocionController> _logger;

        public PromocionController(ILogger<PromocionController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public ActionResult Crear()
        {
            return View("~/Views/Promociones/Crear.cshtml");
        }
        
    }
}
