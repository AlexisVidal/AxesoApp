﻿using AxesoWeb.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AxesoWeb.Controllers
{
    public class TablesController : Controller
    {

        public ActionResult StaticTables()
        {
            return View();
        }

        public ActionResult DataTables()
        {
            return View();
        }

        public ActionResult FooTables()
        {
            return View();
        }

        public ActionResult jqGrid()
        {
            return View();
        }
        
    }
}