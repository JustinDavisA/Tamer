using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Tamer.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Will you rise to be the best? Or stay with all the rest?";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contact the Tamer Team 365 24/7 (Really though, please don't)";

            return View();
        }
    }
}