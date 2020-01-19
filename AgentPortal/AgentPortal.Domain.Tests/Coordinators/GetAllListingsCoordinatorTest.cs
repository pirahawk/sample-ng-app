using AgentPortal.Domain.Data;
using AgentPortal.Domain.Db;
using AgentPortal.Domain.Tests.Data;
using Moq;
using System.Linq;
using AgentPortal.Domain.Coordinators;
using Xunit;

namespace AgentPortal.Domain.Tests.Coordinators
{
    public class GetAllListingsCoordinatorTest
    {
        [Fact]
        public void QueriesContextToRetrieveAllListings()
        {
            var listing1 = new ListingFixture().Build();
            var listing2 = new ListingFixture().Build();

            var mockContext = new Mock<IPortalDbContext>();
            mockContext.Setup(m => m.Query<Listing>())
                .Returns(()=> new[] { listing1, listing2}.AsQueryable())
                .Verifiable();

            var coordinator =  new GetAllListingsCoordinator(mockContext.Object);

            var result = coordinator.GetAllListings().ToArray();

            mockContext.VerifyAll();
            Assert.Contains(listing1, result);
            Assert.Contains(listing2, result);
        }
    }
}