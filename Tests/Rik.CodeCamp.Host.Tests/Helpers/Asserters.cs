using System;
using FluentAssertions;
using Rik.Codecamp.Entities;

namespace Rik.CodeCamp.Host.Tests.Helpers
{
    public class Asserters
    {
        internal static void AssertNew(New newd)
        {
            
            newd.Should().NotBeNull();
            newd.Value.Should().BeGreaterOrEqualTo(1);
            newd.Value.Should().BeLessOrEqualTo(15);
        }

        internal static void AssertWorld(World world)
        {
            world.Should().NotBeNull();
            world.DateTime.Should().NotBe(new DateTime());
        }
    }
}