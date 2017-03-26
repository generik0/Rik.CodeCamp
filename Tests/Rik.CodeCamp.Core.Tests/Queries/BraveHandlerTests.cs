using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Rik.Codecamp.Entities;
using Rik.CodeCamp.Core.Queries;
using Rik.CodeCamp.Data.Sessions;
using Rik.CodeCamp.Repository;
using Smooth.IoC.Cqrs;
using Smooth.IoC.UnitOfWork;

namespace Rik.CodeCamp.Core.Tests.Queries
{
    [TestFixture]
    [SuppressMessage("ReSharper", "ConsiderUsingConfigureAwait")]
    public class BraveHandlerTests
    {
        [Test]
        public static void Query_DoesNotThrowAsync()
        {
            IEnumerable<Brave> expected = new[] { new Brave(), new Brave(), new Brave(), };

            var handlerFactory= new Mock<IHandlerFactory>();
            var dbFactory = new Mock<IDbFactory>();
            var fooSession = new Mock<IFooSession>();
            dbFactory.Setup(x => x.Create<IFooSession>()).Returns(fooSession.Object).Verifiable();
            var braveRepository = new Mock<IBraveRepository>();
            braveRepository.Setup(x => x.GetAllAsync(fooSession.Object)).Returns(Task.FromResult(expected)).Verifiable();

            var target = new BraveHandler(handlerFactory.Object, dbFactory.Object, braveRepository.Object);

            IEnumerable<Brave> actual = null;
            Assert.DoesNotThrowAsync(async ()=> actual = await target.QueryAsync());
            actual.Should().NotBeNull();
            actual.Should().NotBeEmpty();
            actual.Should().BeSameAs(expected);
            dbFactory.Verify();
            braveRepository.Verify();
        }
    }
}
