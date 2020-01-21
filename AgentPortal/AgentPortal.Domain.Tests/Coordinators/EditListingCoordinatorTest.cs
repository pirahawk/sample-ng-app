using System.Threading.Tasks;
using AgentPortal.Domain.Coordinators;
using AgentPortal.Domain.Data;
using AgentPortal.Domain.Db;
using AgentPortal.Domain.Http;
using AgentPortal.Domain.Tests.Data;
using Moq;
using Xunit;

namespace AgentPortal.Domain.Tests.Coordinators
{
    public class EditListingCoordinatorTest
    {
        [Fact]
        public async Task AppliesEditsAndSavesExistingListing()
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

            var mockValidator = new Mock<IListingValidatorHelper>();
            mockValidator.Setup(v => v.HasValidFields(It.IsAny<Listing>())).Returns(true).Verifiable();

            var coordinator = new EditListingCoordinator(mockContext.Object, mockValidator.Object);
            var result = await coordinator.Edit(existingListing.Id, existingListing, editRequest);

            mockContext.VerifyAll();
            mockValidator.VerifyAll();
            Assert.Equal(editRequest.Address, existingListing.Address);
            Assert.Equal(editRequest.AskingPrice, existingListing.AskingPrice);
            Assert.Equal(editRequest.Description, existingListing.Description);
            Assert.Equal(editRequest.Expired, existingListing.Expired);
            Assert.Equal(editRequest.NumberBedrooms, existingListing.NumberBedrooms);
            Assert.Equal(editRequest.PostCode, existingListing.PostCode);
        }

        [Fact]
        public async Task DoesNotSaveExistingListingIfFieldsAreDeemedInvalid()
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

            var mockValidatorThatAlwaysFails = new Mock<IListingValidatorHelper>();
            mockValidatorThatAlwaysFails.Setup(v => v.HasValidFields(It.IsAny<Listing>())).Returns(false).Verifiable();

            var coordinator = new EditListingCoordinator(mockContext.Object, mockValidatorThatAlwaysFails.Object);
            var result = await coordinator.Edit(existingListing.Id, existingListing, editRequest);

            mockValidatorThatAlwaysFails.VerifyAll();
            mockContext.VerifyAll();
            mockContext.Verify(m => m.SaveChanges(), Times.Never);
            Assert.Null(result);
        }
    }
}