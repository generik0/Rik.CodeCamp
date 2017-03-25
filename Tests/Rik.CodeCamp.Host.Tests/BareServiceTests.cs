using FluentAssertions;
using NUnit.Framework;
using Rik.CodeCamp.Core.Contracts;
using Rik.CodeCamp.Host.Tests.Helpers;

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
