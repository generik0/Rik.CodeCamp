using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using GAIT.Utilities.DI.Installers;
using Smooth.IoC.Cqrs;
using Smooth.IoC.Cqrs.Commanding;
using Smooth.IoC.Cqrs.Query;
using Smooth.IoC.Cqrs.Requests;
using Smooth.IoC.Cqrs.Tap;


namespace Rik.CodeCamp.Core.Installers
{
    public class CqrsInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            if (FacilityHelper.DoesKernelNotAlreadyContainFacility<TypedFactoryFacility>(container))
            {
                container.Kernel.AddFacility<TypedFactoryFacility>();
            }
            container.Register(Component.For<IHandlerFactory>().AsFactory());
            container.Register(Component.For<ICommandDispatcher>().ImplementedBy<CommandDispatcher>());
            container.Register(Component.For<IRequestDispatcher>().ImplementedBy<RequestDispatcher>());
            container.Register(Component.For<IQueryDispatcher>().ImplementedBy<QueryDispatcher>());
            container.Register(Component.For<ICqrsDispatcher>().ImplementedBy<CqrsDispatcher>());
        }
    }
}
