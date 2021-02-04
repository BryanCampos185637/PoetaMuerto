using System;
using System.Web.Mvc;
using BussinesEntity;
using BussinesLogic;

namespace UI.Autor.Controllers
{
    public class PoemaController : Controller
    {
        PoemaBL bl = new PoemaBL();
        // GET: Poema
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public JsonResult listar()
        {
            return Json(bl.lstPoema(), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public string guardar(Poema poema)
        { 
            poema.Bhabilitado = "A";
            return bl.guardar(poema);
        }
        [HttpGet]
        public string eliminar(Int64 id)
        {
            return bl.eliminar(id);
        }
        [HttpGet]
        public JsonResult obtener(Int64 id)
        {
            return Json(bl.obtenerPorId(id), JsonRequestBehavior.AllowGet);
        }
    }
}