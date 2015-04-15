using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
//
using CookieMVC.Web.Security;

namespace CookieMVC.Web
{
    public class AuthenticationInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component
                .For<IAuthenticator>()
                .ImplementedBy<FormsAuthenticator>()
                .LifestyleTransient());
        }
    }
}