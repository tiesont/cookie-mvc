using FluentSecurity;
using FluentSecurity.Configuration;

namespace CookieMVC.Web
{
    public class AllowAnonymousSecurityProfile : SecurityProfile
    {
        public override void Configure()
        {
            For<Controllers.ErrorController>().AllowAny();
            For<Controllers.HomeController>(action => action.Help()).AllowAny();
        }
    }
}
