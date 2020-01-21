using System;
using System.Threading.Tasks;
using AgentPortal.Domain.Coordinators;
using AgentPortal.Domain.Data;
using AgentPortal.Domain.Db;
using AgentPortal.Domain.Tests.Data;
using Moq;
using Xunit;

namespace AgentPortal.Domain.Tests.Coordinators
{
    public class FindListingCoordinatorTest
    {
        [Fact]
        public async Task CanRetrieveListingById()
        {
            var listing1 = new ListingFixture().Build();
            var listing2 = new ListingFixture().Build();

            var mockContext = new Mock<IPortalDbContext>();
            mockContext.Setup(m => m.Find<Listing>(listing1.Id))
                .Returns(async () =>
                {
                    await Task.CompletedTask;
                    return listing1;
                })
                .Verifiable();

            var coordinator = new FindListingCoordinator(mockContext.Object);

            var result = await coordinator.Find(listing1.Id);

            mockContext.VerifyAll();
            Assert.Equal(listing1, result);
        }

        [Fact]
        public async Task ReturnsNullWhenListingNotFound()
        {
            var listing1 = new ListingFixture().Build();

            var mockContextThatReturnsNoListings = new Mock<IPortalDbContext>();
            mockContextThatReturnsNoListings.Setup(m => m.Find<Listing>(It.IsAny<Guid>()))
                .Returns(async () =>
                {
                    await Task.CompletedTask;
                    return null;
                })
                .Verifiable();

            var coordinator = new FindListingCoordinator(mockContextThatReturnsNoListings.Object);

            var result = await coordinator.Find(listing1.Id);

            mockContextThatReturnsNoListings.VerifyAll();
            Assert.Null(result);
        }
    }
}