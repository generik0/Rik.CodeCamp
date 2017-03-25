
using GAIT.Utilities;

namespace Rik.CodeCamp.Host
{
    internal class Program
    {
        private static void Main()
        {
            HostFactory.Run(x =>
            {
                x.Service<ServiceControl>(sc =>
                {
                    sc.ConstructUsing(CreateServiceControl);
                    sc.WhenStarted((s, hostControl) => s.Start(hostControl));
                    sc.WhenStopped((s, hostControl) => s.Stop(hostControl));
                });
                x.SetDescription("Hist for Rik.CodeCamp");
                x.SetDisplayName("Rik.CodeCamp.Host");
                x.SetServiceName("Rik.CodeCamp.Host");
                x.UseNLog();
            });
        }

        private static ServiceControl CreateServiceControl(HostSettings settings)
        {
            return ActivatorFactory.Resolve<CodeCampControl>();
        }
    }
}
