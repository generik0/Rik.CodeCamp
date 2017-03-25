using System;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using GAIT.Utilities.DI.Attributes;
using GAIT.Utilities.Logging;

namespace GAIT.Utilities.DI.Installers
{
    [NoIoC]
    public class GeneralInstallerResolvers : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {

            try
            {
                container.Kernel.Resolver.AddSubResolver(new CollectionResolver(container.Kernel, true));
                container.Kernel.Resolver.AddSubResolver(new ListResolver(container.Kernel, true));
                container.Kernel.Resolver.AddSubResolver(new ArrayResolver(container.Kernel, true));
            }
            catch (Exception exception)
            {
                LoggingFactory.Create(GetType()).Error(exception, $"{GetType()} failed to install");
                throw new MisconfiguredComponentException(exception);
            }
        }
    }
}
