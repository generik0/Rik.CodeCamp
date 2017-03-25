using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Core.Internal;
using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using GAIT.Utilities.DI.Attributes;
using GAIT.Utilities.Logging;

namespace GAIT.Utilities.DI.Installers
{
    [NoIoC]
    public class GeneralInstallerFactories : IWindsorInstaller
    {
        private readonly IEnumerable<string> _paths = ProjectMetadata.ProjectAssembiliesPaths;
        private readonly IEnumerable<string> _projects = ProjectMetadata.ProjectPrefixName;

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            try
            {
                if (FacilityHelper.DoesKernelNotAlreadyContainFacility<TypedFactoryFacility>(container))
                {
                    container.Kernel.AddFacility<TypedFactoryFacility>();
                }

                foreach (var path in _paths.Distinct())
                {
                    container.Register(Types.FromAssemblyInDirectory(
                        new AssemblyFilter(path).
                            FilterByName(an => _projects.Any(x => an.Name.ToLower().StartsWith(x, StringComparison.Ordinal))))
                        .Where(t => t.IsInterface && t.HasAttribute<InverstionOfControlInstallAsFactory>())
                        .Configure(x => x.AsFactory())
                        .LifestyleSingleton());
                }
            }
            catch (Exception exception)
            {
                LoggingFactory.Create(GetType()).Error(exception, $"{GetType()} failed to install");
                throw new MisconfiguredComponentException(exception);
            }
        }
    }
}
