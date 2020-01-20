using AgentPortal.Domain.Data;
using AgentPortal.Domain.Db;
using AgentPortal.Domain.Tests.Data;
using Moq;
using System.Linq;
using AgentPortal.Domain.Coordinators;
using AgentPortal.Domain.Http;
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

        [Fact]
        public void DoesNotReturnExpiredListings()
        {
            var listing1 = new ListingFixture().Build();
            var expiredListing = new ListingFixture
            {
                Expired = true
            }.Build();

            var mockContext = new Mock<IPortalDbContext>();
            mockContext.Setup(m => m.Query<Listing>())
                .Returns(() => new[] { listing1, expiredListing }.AsQueryable())
                .Verifiable();

            var coordinator = new GetAllListingsCoordinator(mockContext.Object);

            var result = coordinator.GetAllListings().ToArray();

            mockContext.VerifyAll();
            Assert.Contains(listing1, result);
            Assert.DoesNotContain(expiredListing,result);
        }
    }

    public class EditListingCoordinatorTest
    {
        [Fact]
        public void AppliesEditsAndSavesExistingListing()
        {
            var existingListing = new ListingFixture().Build();
            var editRequest = new EditListingRequest
            {
                Address = existingListing + "edit",
                AskingPrice = existingListing.AskingPrice + 10m,
                Description = existingListing.Description + "edit",
                Expired = true,
                NumberBedrooms = existingListing.NumberBedrooms + 1,
                PostCode = existingListing.PostCode + "edit"
            };

            var mockContext = new Mock<IPortalDbContext>();
            mockContext.Setup(m => m.Attach(existingListing)).Verifiable();
            mockContext.Setup(m=> m.SaveChanges()).Verifiable();

            var coordinator = new EditListingCoordinator(mockContext.Object);
            coordinator.Edit(existingListing.Id, existingListing, editRequest);

            mockContext.VerifyAll();
            Assert.Equal(editRequest.Address, existingListing.Address);
            Assert.Equal(editRequest.AskingPrice, existingListing.AskingPrice);
            Assert.Equal(editRequest.Description, existingListing.Description);
            Assert.Equal(editRequest.Expired, existingListing.Expired);
            Assert.Equal(editRequest.NumberBedrooms, existingListing.NumberBedrooms);
            Assert.Equal(editRequest.PostCode, existingListing.PostCode);
        }
    }
}