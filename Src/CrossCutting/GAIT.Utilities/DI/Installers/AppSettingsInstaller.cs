using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Core.Internal;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using GAIT.Utilities.DI.Attributes;
using GAIT.Utilities.DI.Resolves;
using GAIT.Utilities.Logging;

namespace GAIT.Utilities.DI.Installers
{
    [NoIoC]
    public class AppSettingsInstaller : IWindsorInstaller
    {
        public static string AppSettingsModelpostfix { get; private set; }

        private readonly IEnumerable<string> _paths = ProjectMetadata.ProjectAssembiliesPaths;
        private readonly IEnumerable<string> _projects = ProjectMetadata.ProjectPrefixName;


        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Kernel.Resolver.AddSubResolver(new AppSettingsConvention());

            if (string.IsNullOrWhiteSpace(AppSettingsModelpostfix))
            {
                AppSettingsModelpostfix = "AppSettings";
            }
            RegisterClassesFromPathAndProjectThatUseIAppSettings(container);
        }

        private void RegisterClassesFromPathAndProjectThatUseIAppSettings(IWindsorContainer container)
        {
            foreach (var path in _paths)
            {
                try
                {
                    container.Kernel.Register(
                    Classes.FromAssemblyInDirectory(new AssemblyFilter(path)
                        .FilterByName(an => _projects.Any(x => an.Name.ToLower().StartsWith(x, StringComparison.Ordinal))))
                        .Where(t => t.HasAttribute<InverstionOfControlInstallAsAppSetting>())
                        .Configure(
                            component =>
                            {
                                component.Named($"{component.Implementation.FullName}-{AppSettingsModelpostfix}");
                                component.IsDefault();
                            })
                        .WithServiceDefaultInterfaces()
                        .LifestyleSingleton());
                }
                catch (Exception exception)
                {
                    LoggingFactory.Create(GetType()).Error(exception, $"{GetType()} failed to install");
                    throw new MisconfiguredComponentException(exception);
                }
            }
        }
    }
}
