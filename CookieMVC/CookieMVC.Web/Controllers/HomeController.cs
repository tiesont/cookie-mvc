using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CookieMVC.Web.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(ILogger logger)
            :base(logger)
        {
        }


        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
    }
}