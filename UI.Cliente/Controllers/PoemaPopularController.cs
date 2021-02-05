using BussinesLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UI.Cliente.Controllers
{
    public class PoemaPopularController : Controller
    {
        PoemaBL poemaBL = new PoemaBL();
        // GET: PoemaPopular
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public JsonResult listarPoemasPopulares()
        {
            return Json(poemaBL.poemasPopulares(), JsonRequestBehavior.AllowGet);
        }
    }
}