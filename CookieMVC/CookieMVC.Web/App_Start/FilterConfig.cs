using System.Web.Mvc;

namespace CookieMVC.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new FluentSecurity.HandleSecurityAttribute());
            filters.Add(new HandleErrorAttribute());
            filters.Add(new Security.RequireSecureConnectionAttribute());
        }
    }
}
