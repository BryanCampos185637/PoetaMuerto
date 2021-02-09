using BussinesLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UI.Autor.Controllers
{
    public class MeGustaController : Controller
    {
        MeGustaBL bl = new MeGustaBL(); 
        // GET: MeGusta
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public JsonResult Listar()
        {
            return Json(bl.listar(), JsonRequestBehavior.AllowGet);
        }
    }
}