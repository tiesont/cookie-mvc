using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

using FluentSecurity;

namespace CookieMVC.Web
{
    public class SecurityInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromThisAssembly()
                .BasedOn<IPolicyViolationHandler>()
                .LifestyleSingleton());
        }
    }
}
