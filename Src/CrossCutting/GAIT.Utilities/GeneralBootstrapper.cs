using System;
using System.Diagnostics;
using System.Threading;
using Castle.Core.Logging;
using Castle.Windsor;
using Castle.Windsor.Installer;
using GAIT.Utilities.DI.Installers;
using GAIT.Utilities.Logging;

namespace GAIT.Utilities
{
    public abstract class GeneralBootstrapper : IDisposable
    {
        protected IWindsorContainer Container;
        protected bool Disposed;
        private readonly IGeneralNLogFactory _generalNLogFactory;
        public static CancellationTokenSource CancelAll = new CancellationTokenSource();

        protected GeneralBootstrapper()
        {
            var factory = ActivatorFactory.Resolve<ActivatorFactory>();
            Container = factory.Create<WindsorContainer>();
            _generalNLogFactory = factory.Create<GeneralNLogFactory>();
        }

        ~GeneralBootstrapper()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

         protected virtual void Dispose(bool disposing)
        {
            if (Disposed) return;
            if (disposing)
            {
                CancelAll.Cancel();
                Container?.Dispose();
                Container = null;
            }
            Disposed = true;
        }
        protected ILogger CreateInversionOfControlContainer()
        {
            try
            {
                _generalNLogFactory.AddNLogFactory(Container);
                Container.Install(FromAssembly.This());

                var logger = Container.Resolve<ILogger>(new { type = GetType()  }); 
                return logger;
            }
            catch (Exception exception)
            {
                LoggingFactory.Create(GetType()).Fatal(exception, "Initialization failed"); Trace.WriteLine(exception);
            }
            return null;

        }
        public T Resolve<T>()
        {
            return Container.Resolve<T>();
        }
        public void Release(object instance)
        {
            Container.Release(instance);
        }

        public T Resolve<T>(object arguementsAsAnonymousType)
        {
            return Container.Resolve<T>(arguementsAsAnonymousType);
        }
    }
}
