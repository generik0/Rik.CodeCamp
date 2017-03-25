using Castle.MicroKernel.Registration;
using Castle.Windsor;
using GAIT.Utilities.DI.Attributes;
using Nancy.Bootstrappers.Windsor;
using Nancy.Serialization.JsonNet;
using Newtonsoft.Json;

namespace Rik.CodeCamp.Core.Installers
{
    [NoIoC]
    public class WindsorNancyBootstrapperWrapper : WindsorNancyBootstrapper, IWindsorNancyBootstrapperWrapper
    {
        private readonly IWindsorContainer _container;

        public WindsorNancyBootstrapperWrapper(IWindsorContainer container)
        {
            _container = container;
        }

        protected override void ConfigureApplicationContainer(IWindsorContainer container)
        {
            container.Register(Component.For<JsonSerializer, CustomJsonSerializer>());
            container.Register(Component.For<JsonNetBodyDeserializer>());
        }

        protected override IWindsorContainer GetApplicationContainer()
        {
            return _container;
        }
    }
}
