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
    public class GeneralInstallerTransient : IWindsorInstaller
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
                        new AssemblyFilter(path)
                        .FilterByName(an => _projects.Any(x => an.Name.ToLower().StartsWith(x, StringComparison.Ordinal))))
                        .IncludeNonPublicTypes()
                        .Where(t => t.GetInterfaces().Any(x => x != typeof(IDisposable)) && !t.IsAssignableFrom(typeof(Exception))
                            && !t.GetCustomAttributes(false).Any(x => x.ToString().Contains("InverstionOfControl") || x is NoIoC))
                    .Configure(registration =>
                    {
                        registration.IsFallback();
                        registration.Named($"{registration.Implementation.FullName}-TransientFallback");
                    })
                    .WithServiceAllInterfaces()
                    .LifestyleTransient());
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
