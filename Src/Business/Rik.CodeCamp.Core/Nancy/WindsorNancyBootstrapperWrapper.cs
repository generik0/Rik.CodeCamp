﻿namespace Rik.CodeCamp.Core.Nancy
{
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
