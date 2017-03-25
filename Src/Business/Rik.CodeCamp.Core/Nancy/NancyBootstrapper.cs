namespace Rik.CodeCamp.Core.Nancy
{
    [NoIoC]
    public class NancyBootstrapper
    {
        private  static ILogger _logger;

        public NancyBootstrapper(IWindsorContainer container)
        {
            Setup(container, null);
        }

        public NancyBootstrapper(IWindsorContainer container, ILogger logger)
        {
            Setup(container, logger);
        }
        private void Setup(IWindsorContainer container, ILogger logger)
        {
            if (_logger == null)
            {
                _logger = logger ?? LoggingFactory.Create(GetType());
            }
            container.Register(Component.For<IWindsorNancyBootstrapperWrapper>()
                .UsingFactoryMethod(() => new WindsorNancyBootstrapperWrapper(container)).IsDefault());

        }
    }
}
