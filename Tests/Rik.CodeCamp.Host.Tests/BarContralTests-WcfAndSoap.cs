using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Rik.Codecamp.Entities;
using Rik.CodeCamp.Core.Contracts;
using Rik.CodeCamp.Host.Tests.Helpers;

namespace Rik.CodeCamp.Host.Tests
{
    [TestFixture]
    public class BarContractTests : TestSetupFixture
    {
        
        [Test, Category("Integration")]
        public static void IsConnected_True()
        {
            var target = Container.Resolve<IBarContract>();
            var actual = target.IsConnected();
            actual.Should().BeTrue();
        }

        [TestCase("2017-03-25T20:00"), Category("Integration")]
        [TestCase("2017-03-25T20:00"), Category("Integration")]
        [TestCase("2017-03-25T21:00"), Category("Integration")]
        [TestCase("2017-03-25T21:00"), Category("Integration")]
        [TestCase("2017-03-25T22:00"), Category("Integration")]
        [TestCase("2017-03-25T22:00"), Category("Integration")]
        public static void _1_SaveOrUpdateBrave_DoesNotThrowAsync_AndReturnsBraveId(string datetime)
        {
            var target = Container.Resolve<IBarContract>();
            var actual=0;
            Assert.DoesNotThrowAsync(async ()=> actual = await target.SaveOrUpdateBrave(StubCreator.CreateRandomBrave(datetime)));
            actual.Should().BePositive();
            
        }

        [Test,Category("Integration")]
        public static void _2_GetAllBraves_DoesNotThrowAsync_AndGetsAllBraves()
        {
            var target = Container.Resolve<IBarContract>();
            IEnumerable<Brave> actual = null;
            Assert.DoesNotThrowAsync(async () => actual = await target.GetAllBraves());
            actual.Should().NotBeNull();
            actual.Should().NotBeEmpty();
            actual.Count().Should().BeGreaterOrEqualTo(6, "Because we saved 6 in the above method ");
            var world = actual.First().World;
            Asserters.AssertWorld(world);
            var newd = actual.First().New;
            Asserters.AssertNew(newd);
        }

        [Test, Category("Integration")]
        public static void _3_GetBrave()
        {
            var target = Container.Resolve<IBarContract>();
            Brave actual = null;
            Assert.DoesNotThrowAsync(async () => actual = await target.GetBrave(1)); //There should be a 1
            actual.Should().NotBeNull();
            Asserters.AssertNew(actual.New);
            Asserters.AssertWorld(actual.World);

        }
    }
}
