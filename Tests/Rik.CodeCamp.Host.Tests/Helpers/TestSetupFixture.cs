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
        private static Process _process;

        [OneTimeSetUp]
        public static void TestSetup()
        {
            if (!Process.GetProcesses().Any(x => x.ProcessName.Contains("Rik.CodeCamp.Host")))
            {
                var paths = TestContext.CurrentContext.TestDirectory.Split('\\');
                var testsIndex = paths.Select((path, i) => new { path, i }).First(x=>x.path.Equals("Tests")).i;
                var hostPath = $@"{string.Join(@"\", paths.Take(testsIndex))}\Src\Hosts\Rik.CodeCamp.Host\bin\Debug\Rik.CodeCamp.Host.exe";
                _process = Process.Start(hostPath);
                Task.Delay(2000).Wait(); //I don't have enough time to do better than a timer right now :-/
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
            Container.Register(Component.For<IBarContract>()
                .AsWcfClient(new DefaultClientModel
                {
                    Endpoint = WcfEndpoint.BoundTo(_clientBinding).At("http://localhost/BarService")
                }).LifestyleTransient());

        }

        //[OneTimeTearDown]
        //public static void TestTearDown()
        //{
        //    if (Process.GetProcesses().Any(x => x.ProcessName.Contains("Rik.CodeCamp.Host")))
        //    {
        //        if (_process == null) return;
        //        _process.Kill();
        //        Task.Delay(500).Wait(); //I don't have enough time to do better than a timer right now :-/
        //        Task.Run(() =>
        //        {
        //            if (File.Exists("C:\\RikCodeCampDb.db"))
        //            {
        //                File.Delete("C:\\RikCodeCampDb.db");
        //            }
        //        });
        //    }
        //}
        
    }
}
