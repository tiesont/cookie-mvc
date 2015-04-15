using System.Web;
using System.Web.Mvc;

using FluentSecurity;

namespace CookieMVC.Web.Security
{
    public class DenyAccessPolicyViolationHandler : IPolicyViolationHandler
    {
        public ActionResult Handle(PolicyViolationException exception)
        {
            bool isAuthenticated = HttpContext.Current.Request.IsAuthenticated;

            return isAuthenticated ? new HttpStatusCodeResult(403, exception.Message) : new HttpStatusCodeResult(401, exception.Message);
        }
    }
}