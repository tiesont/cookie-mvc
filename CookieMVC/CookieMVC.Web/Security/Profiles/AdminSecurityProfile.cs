using System.Web;
using FluentSecurity;
using FluentSecurity.Configuration;

namespace CookieMVC.Web
{
    public class AdminSecurityProfile : SecurityProfile
    {
        public override void Configure()
        {
            For<Elmah.Mvc.ElmahController>()
                .AddPolicy<LocalOnlyPolicy>();
        }
    }
}
