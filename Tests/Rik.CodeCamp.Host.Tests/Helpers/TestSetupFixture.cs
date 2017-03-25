using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using Castle.Facilities.TypedFactory;
using Castle.Facilities.WcfIntegration;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using NUnit.Framework;
using Rik.CodeCamp.Core.Contracts;

namespace Rik.CodeCamp.Host.Tests.Helpers
{
    public abstract class TestSetupFixture
    {
        private static BasicHttpBinding _clientBinding;
        public static IWindsorContainer Container;

        [OneTimeSetUp]
        public static void TestSetup()
        {
            var process = Process.GetProcesses();
            if (!process.Any(x => x.ProcessName.Contains("Rik.CodeCamp.Host")))
            {
                var paths = TestContext.CurrentContext.TestDirectory.Split('\\');
                var testsIndex = paths.Select((path, i) => new { path, i }).First(x=>x.path.Equals("Tests")).i;
                var hostPath = $@"{string.Join(@"\", paths.Take(testsIndex))}\Src\Hosts\Rik.CodeCamp.Host\bin\Debug\Rik.CodeCamp.Host.exe";
                Process.Start(hostPath);
                Task.Delay(2000).Wait();
            }
            if (Container != null) return;
            Container = new WindsorContainer();
            Container.Kernel.AddFacility<TypedFactoryFacility>();
            Container.Kernel.AddFacility<WcfFacility>();
            _clientBinding = new BasicHttpBinding
            {
                Security =
                {
                    Mode = BasicHttpSecurityMode.None,
                    Transport = {ClientCredentialType = HttpClientCredentialType.None},
                    Message = {ClientCredentialType = BasicHttpMessageCredentialType.Certificate}
                }
            };
            Container.Register(Component.For<IBarService>()
                .AsWcfClient(new DefaultClientModel
                {
                    Endpoint = WcfEndpoint.BoundTo(_clientBinding).At("http://localhost/BarService")
                }).LifestyleTransient());

        }
        
    }
}
