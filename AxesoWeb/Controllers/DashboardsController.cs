using AxesoWeb.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AxesoWeb.Controllers
{
    [SessionAuthorize]
    public class DashboardsController : Controller
    {
        public ActionResult Dashboard_1()
        {
            return View();
        }

        public ActionResult Dashboard_2()
        {
            return View();
        }

        public ActionResult Dashboard_3()
        {
            return View();
        }
        
        public ActionResult Dashboard_4()
        {
            return View();
        }
        
        public ActionResult Dashboard_4_1()
        {
            return View();
        }

        public ActionResult Dashboard_5()
        {
            return View();
        }
        public ActionResult Index()
        {
            return View();
        }
    }
}