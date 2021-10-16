using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebPagoEfectivo.Controllers
{
    public class PromocionController : Controller
    {
        [HttpGet]
        public ActionResult Crear()
        {
            return View("~/Views/Promociones/Crear.cshtml");
        }

        [HttpGet]
        public ActionResult Canjear()
        {
            return View("~/Views/Promociones/Editar.cshtml");
        }
        [HttpGet]
        public ActionResult Listar()
        {
            return View("~/Views/Promociones/Listar.cshtml");
        }
    }
}
