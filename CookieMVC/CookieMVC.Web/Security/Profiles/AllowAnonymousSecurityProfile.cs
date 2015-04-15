using FluentSecurity;
using FluentSecurity.Configuration;

namespace CookieMVC.Web
{
    public class AllowAnonymousSecurityProfile : SecurityProfile
    {
        public override void Configure()
        {
            For<Controllers.HomeController>().AllowAny();
            For<Controllers.ErrorController>().AllowAny();
        }
    }
}
