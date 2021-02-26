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
            var lst = bl.listar();
            lst = lst.OrderByDescending(p => p.Idmegusta).ToList();
            return Json(lst, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public string eliminarLike(int id)
        {
            try
            {
                var rpt = bl.eliminarLike(id);
                return rpt.ToString();
            }
            catch(Exception e)
            {
                return "Error: " + e.Message;
            }
        }
    }
}