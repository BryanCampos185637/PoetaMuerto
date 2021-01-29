﻿using System;
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
            return Json(bl.lstPoema(), JsonRequestBehavior.AllowGet);
        }
    }
}