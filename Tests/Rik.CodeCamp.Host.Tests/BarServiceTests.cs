using System;
using System.Globalization;
using FluentAssertions;
using NUnit.Framework;
using Rik.Codecamp.Entities;
using Rik.CodeCamp.Core.Contracts;
using Rik.CodeCamp.Host.Tests.Helpers;

namespace Rik.CodeCamp.Host.Tests
{
    [TestFixture]
    public class BarServiceTests : TestSetupFixture
    {
        
        [Test, Category("Integration")]
        public static void IsConnected_True()
        {
            var target = Container.Resolve<IBarService>();
            var actual = target.IsConnected();
            actual.Should().BeTrue();
        }

        [TestCase("2017-03-25T20:00"), Category("Integration")]
        [TestCase("2017-03-25T20:00"), Category("Integration")]
        [TestCase("2017-03-25T21:00"), Category("Integration")]
        [TestCase("2017-03-25T21:00"), Category("Integration")]
        [TestCase("2017-03-25T22:00"), Category("Integration")]
        [TestCase("2017-03-25T22:00"), Category("Integration")]
        public static void SaveOrUpdateBrave_DoesNotThrowAsync_AndReturnsBraveId(string datetime)
        {
            var target = Container.Resolve<IBarService>();
            var actual=0;
            Assert.DoesNotThrowAsync(async ()=> actual = await target.SaveOrUpdateBrave(CreateRandomBrave(datetime)));
            actual.Should().BePositive();
            
        }

        public static void xxx()
        {
            
        }


        private static Brave CreateRandomBrave(string datetime)
        {
            return new Brave
            {
                New = new New
                {
                    Value = TestContext.CurrentContext.Random.NextDouble(0,15)
                },
                World = new World
                {
                    DateTime = DateTime.Parse(datetime, CultureInfo.InvariantCulture)
                }
            };
        }
    }
}
