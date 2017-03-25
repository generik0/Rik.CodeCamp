using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Core.Internal;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using GAIT.Utilities.DI.Attributes;
using GAIT.Utilities.Logging;

namespace GAIT.Utilities.DI.Installers
{
    [NoIoC]
    public class GeneralInstallerSingleton : IWindsorInstaller
    {
        private readonly IEnumerable<string> _paths = ProjectMetadata.ProjectAssembiliesPaths;
        private readonly IEnumerable<string> _projects = ProjectMetadata.ProjectPrefixName;

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            try
            {
                foreach (var path in _paths)
                {
                    container.Register(Classes.FromAssemblyInDirectory(
                        new AssemblyFilter(path).
                            FilterByName(an => _projects.Any(x => an.Name.ToLower().StartsWith(x, StringComparison.Ordinal))))
                        .Where(t => t.GetInterfaces().Any() && t.HasAttribute<InverstionOfControlInstallAsSingleton>())
                        .WithServiceAllInterfaces()
                        .Configure(component =>
                        {
                            component.IsDefault();
                            component.Named($"{component.Implementation.FullName}-Singleton");
                        })
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
