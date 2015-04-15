using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
//
using CookieMVC.ApplicationServices;

namespace CookieMVC.Web
{
    public class ServicesInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IMembershipService>()
                .ImplementedBy<MembershipService>()
                .LifestylePerWebRequest());
        }
    }
}