using System.Threading.Tasks;
using AgentPortal.Domain.Coordinators;
using AgentPortal.Domain.Data;
using AgentPortal.Domain.Db;
using AgentPortal.Domain.Tests.Data;
using Moq;
using Xunit;

namespace AgentPortal.Domain.Tests.Coordinators
{
    public class CreateListingCoordinatorTest
    {
        [Fact]
        public async Task CanCreateAndPersistListing()
        {
            var listingFixture = new ListingFixture
            {
                Address = "test street",
                AskingPrice = 123,
                Description = "A House",
                Expired = false,
                NumberBedrooms = 2,
                PostCode = "AA1 1AA"
            };
            var request = listingFixture.AsRequest();

            var mockContext = new Mock<IPortalDbContext>();
            mockContext.Setup(m => m.Add(It.IsAny<Listing>())).Verifiable();
            mockContext.Setup(m => m.SaveChanges()).Verifiable();

            var mockValidator = new Mock<IListingValidatorHelper>();
            mockValidator.Setup(v => v.HasValidFields(It.IsAny<Listing>())).Returns(true).Verifiable();

            var coordinator = new CreateListingCoordinator(mockContext.Object, mockValidator.Object);

            var result = await coordinator.Create(request);

            mockContext.VerifyAll();
            mockValidator.VerifyAll();
            Assert.Equal(request.Address, result.Address);
            Assert.Equal(request.AskingPrice, result.AskingPrice);
            Assert.Equal(request.Description, result.Description);
            Assert.Equal(request.Expired, result.Expired);
            Assert.Equal(request.NumberBedrooms, result.NumberBedrooms);
            Assert.Equal(request.PostCode, result.PostCode);

        }

        [Fact]
        public async Task DoesNotPersistListingIfFieldsAreDeemedInvalid()
        {
            var invalidListing = new ListingFixture();
            var request = invalidListing.AsRequest();
            var mockContext = new Mock<IPortalDbContext>();
            var mockValidatorThatAlwaysReturnsFalse = new Mock<IListingValidatorHelper>();
            mockValidatorThatAlwaysReturnsFalse.Setup(v => v.HasValidFields(It.IsAny<Listing>())).Returns(false).Verifiable();

            var coordinator = new CreateListingCoordinator(mockContext.Object, mockValidatorThatAlwaysReturnsFalse.Object);
            
            var result = await coordinator.Create(request);
            
            mockValidatorThatAlwaysReturnsFalse.VerifyAll();
            mockContext.Verify(m => m.Add(It.IsAny<Listing>()), Times.Never);
            mockContext.Verify(m => m.SaveChanges(), Times.Never);
            Assert.Null(result);
        }
    }
}