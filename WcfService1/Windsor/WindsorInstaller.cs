using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace WcfService1
{
    public class WindsorInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IWriter, Writer>(),
                Component.For<ILogger, Logger>(),
                Component.For<IStripe, Stripe>());
        }
    }
}