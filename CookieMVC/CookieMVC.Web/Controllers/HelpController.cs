using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//
using Postal;

namespace CookieMVC.Web.Controllers
{
    public class HelpController : BaseController
    {
        public HelpController(ILogger logger, IEmailService mailer)
            : base(logger, mailer)
        {
        }


        public ActionResult Index()
        {
            return View();
        }


    }
}