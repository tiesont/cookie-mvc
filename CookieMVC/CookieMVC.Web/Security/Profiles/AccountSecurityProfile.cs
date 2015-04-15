using FluentSecurity;
using FluentSecurity.Configuration;

namespace CookieMVC.Web
{
    public class AccountSecurityProfile : SecurityProfile
    {
        public override void Configure()
        {
            For<Controllers.AccountController>(ac => ac.Login(string.Empty)).AllowAny();
            For<Controllers.AccountController>(ac => ac.ForgotPassword()).AllowAny();
            For<Controllers.AccountController>(ac => ac.ResetPassword(string.Empty)).AllowAny();
            //For<Controllers.AccountController>(ac => ac.Register(string.Empty)).Ignore();
        }
    }
}
