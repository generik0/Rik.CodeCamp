using System.Linq;
using System.Text;
using Castle.Core.Logging;
using Castle.MicroKernel;
using Castle.MicroKernel.Handlers;
using Castle.Windsor;
using Castle.Windsor.Diagnostics;
using GAIT.Utilities;
using Rik.CodeCamp.Core.Installers;
using Rik.CodeCamp.Data;

namespace Rik.CodeCamp.Host
{
    internal class Bootstrapper : GeneralBootstrapper
    {
        public Bootstrapper()
        {
            CreateInversionOfControlContainer();
            Container.Install(new DataFactoryInstaller());
            ActivatorFactory.Resolve<NancyBootstrapper>(Container);
            CheckForPotentiallyMisconfiguredComponents(Container);
        }
        private static void CheckForPotentiallyMisconfiguredComponents(IWindsorContainer container)
        {
            var host = (IDiagnosticsHost)container.Kernel.GetSubSystem(SubSystemConstants.DiagnosticsKey);
            var diagnostics = host.GetDiagnostic<IPotentiallyMisconfiguredComponentsDiagnostic>();

            var handlers = diagnostics.Inspect();

            if (!handlers.Any()) return;
            var message = new StringBuilder();
            var inspector = new DependencyInspector(message);

            foreach (var handler1 in handlers)
            {
                var handler = (IExposeDependencyInfo)handler1;
                handler.ObtainDependencyDetails(inspector);

            }
            var logger = container.Resolve<ILogger>();
            logger.Debug($"Dependancy errors:\n{message}");
        }
    }
}