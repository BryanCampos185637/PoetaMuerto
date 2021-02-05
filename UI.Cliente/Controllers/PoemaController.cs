using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BussinesEntity;
using BussinesLogic;

namespace UI.Cliente.Controllers
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
        public JsonResult poemas()
        {
            return Json(bl.lstPoema().OrderByDescending(p => p.Idpoema).ToList(), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public string darMegusta(MeGusta meGusta)
        {
            MeGustaBL bl = new MeGustaBL();
            meGusta.Ipcliente = Request.UserHostAddress;
            return bl.darMegusta(meGusta);
        }

        [HttpGet]
        public JsonResult contarLike(Int64 Idpoema)
        {
            MeGustaBL bl = new MeGustaBL();
            return Json(bl.ContarMeGusta(Idpoema),JsonRequestBehavior.AllowGet);
        }
    }
}