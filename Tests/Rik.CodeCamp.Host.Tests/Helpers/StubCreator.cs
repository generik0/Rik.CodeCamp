using System;
using System.Globalization;
using NUnit.Framework;
using Rik.Codecamp.Entities;

namespace Rik.CodeCamp.Host.Tests.Helpers
{
    public class StubCreator
    {
        internal static Brave CreateRandomBrave(string datetime)
        {
            return new Brave
            {
                New = new New
                {
                    Value = TestContext.CurrentContext.Random.NextDouble(1,15)
                },
                World = new World
                {
                    DateTime = DateTime.Parse(datetime, CultureInfo.InvariantCulture)
                }
            };
        }
    }
}