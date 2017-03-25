using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using Castle.Facilities.TypedFactory;
using Castle.Facilities.WcfIntegration;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using FluentAssertions;
using NUnit.Framework;
using Rik.CodeCamp.Core.Contracts;
using Rik.CodeCamp.Host.Tests.Helpers;
using static Rik.CodeCamp.Host.Tests.Helpers.TestSetupFixture;

namespace Rik.CodeCamp.Host.Tests
{
    [TestFixture]
    public class BareServiceTests : TestSetupFixture
    {
        
        [Test, Category("Integration")]
        public static void IsConnected_True()
        {
            var target = Container.Resolve<IBarService>();
            var actual = target.IsConnected();
            actual.Should().BeTrue();
        }
        
    }
}
