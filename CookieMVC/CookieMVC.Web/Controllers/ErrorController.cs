using System;
using System.Web.Mvc;

namespace CookieMVC.Web.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult Index()
        {
            return View("general");
        }

        public ActionResult BadRequest()
        {
            return View();
        }

        public ActionResult Unauthorized()
        {
            return View();
        }

        public ActionResult Forbidden()
        {
            return View();
        }

        public ActionResult NotFound()
        {
            return View();
        }

        public ActionResult General(Exception ex = null)
        {
            return View();
        }
    }
}