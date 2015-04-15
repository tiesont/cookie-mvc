using System.Diagnostics;
using System.Web;
using CookieMVC.Web.Security;
//
using FluentSecurity;
using FluentSecurity.Configuration;

namespace CookieMVC.Web
{
    public class SecurityConfig
    {
        public static void ConfigureMvcSecurity()
        {
            var config = SecurityConfigurator.Configure(configuration =>
            {
                configuration.DefaultPolicyViolationHandlerIs(() => new DenyAccessPolicyViolationHandler());
                configuration.GetAuthenticationStatusFrom(() => SecurityProvider.IsUserAuthenticated());

                // Let Fluent Security know how to get the roles for the current user
                configuration.GetRolesFrom(() => SecurityProvider.GetCurrentUserRoles());

                configuration.ForAllControllers().DenyAnonymousAccess();

                configuration.Scan(scan =>
                {
                    scan.AssembliesFromApplicationBaseDirectory();
                    scan.LookForProfiles();
                });
            });
        }
    }
}
