using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using RestSharp;
using Rik.Codecamp.Entities;
using Rik.CodeCamp.Host.Tests.Helpers;

namespace Rik.CodeCamp.Host.Tests
{
    [TestFixture]
    public class BraveServiceTests : TestSetupFixture
    {
        private const string BaseUrl = "http://localhost:8080/BarHost/";

        [Test]
        public static void IsConnected_Returns_True()
        {
            
            var target = new RestClient(BaseUrl);
            var request = new RestRequest("BraveService/IsConnected", Method.GET);
            var actual = target.Execute<bool>(request).Data;
            actual.Should().BeTrue();
        }

        [TestCase("2017-03-26T20:00"), Category("Integration")]
        [TestCase("2017-03-26T20:00"), Category("Integration")]
        [TestCase("2017-03-26T21:00"), Category("Integration")]
        [TestCase("2017-03-26T21:00"), Category("Integration")]
        [TestCase("2017-03-26T22:00"), Category("Integration")]
        [TestCase("2017-03-26T22:00"), Category("Integration")]
        public static void _1_SaveOrUpdate_Returns_Id(string datetime)
        {
            var target = new RestClient(BaseUrl);
            var request = new RestRequest("BraveService/SaveOrUpdate", Method.PUT);
            request.AddJsonBody(StubCreator.CreateRandomBrave(datetime));
            var actual = target.Execute<int>(request).Data;
            actual.Should().BePositive();
        }

        [Test]
        public static void GetAllBraves_Returns_List()
        {
            var target = new RestClient(BaseUrl);
            var request = new RestRequest("BraveService/GetAll", Method.GET);
            var actual = target.Execute<List<Brave>>(request).Data;
            actual.Should().NotBeNull();
            actual.Should().NotBeEmpty();
            var world = actual.First().World;
            Asserters.AssertWorld(world);
            var newd = actual.First().New;
            Asserters.AssertNew(newd);
        }

        [Test]
        public static void Get_Returns_Brave()
        {
            var target = new RestClient(BaseUrl);
            var request = new RestRequest("BraveService/Get/{id}", Method.GET);
            request.AddUrlSegment("id", 1.ToString());
            var actual = target.Execute<Brave>(request).Data;
            actual.Should().NotBeNull();
            Asserters.AssertWorld(actual.World);
            Asserters.AssertNew(actual.New);
        }


    }
}
