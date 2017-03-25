using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using Castle.Facilities.WcfIntegration;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using FluentAssertions;
using NUnit.Framework;
using Rik.CodeCamp.Core.Contracts;

namespace Rik.CodeCamp.Wcf.Tests
{
    [TestFixture]
    public class BareServiceTests
    {
        private static BasicHttpBinding _clientBinding;
        private static IWindsorContainer _container;

        [OneTimeSetUp]
        public static void TestSetup()
        {
            var process = Process.GetProcesses();
            if (!process.Any(x => x.ProcessName.Contains("Rik.CodeCamp.Host")))
            {
                Assert.Fail("Please start the host as admin....");
            }
            _container = new WindsorContainer();
            _clientBinding = new BasicHttpBinding
            {
                Security =
                {
                    Mode = BasicHttpSecurityMode.Transport,
                    Transport = {ClientCredentialType = HttpClientCredentialType.None},
                    Message = {ClientCredentialType = BasicHttpMessageCredentialType.Certificate }
                }
            };
            _container.Register(Component.For<IBarService>()
                .AsWcfClient(new DefaultClientModel
                {
                    Endpoint = WcfEndpoint.BoundTo(_clientBinding).At("http://localhost/BarService")
                }).LifestyleTransient());

        }

        [Test, Category("Integration")]
        public static void IsConnected_True()
        {
            var target = _container.Resolve<IBarService>();
            var actual = target.IsConnected();
            actual.Should().BeTrue();
        }
        
    }
}
