using System;
using System.Linq;
using AgentPortal.Domain.Coordinators;
using AgentPortal.Domain.Data;
using AgentPortal.Domain.Db;
using AgentPortal.Domain.Tests.Data;
using Moq;
using Xunit;

namespace AgentPortal.Domain.Tests.Coordinators
{
    public class GetAllImagesCoordinatorTest
    {
        [Fact]
        public void FindsAllImagesThatBelongToAListing()
        {
            var listing1 = new ListingFixture().Build();
            var listing2 = new ListingFixture().Build();
            var image1 = new ListingImageFixture().ForListing(listing1).Build();
            var image2 = new ListingImageFixture().ForListing(listing2).Build();

            var mockContext = new Mock<IPortalDbContext>();
            mockContext.Setup(m => m.Query<ListingImage>())
                .Returns(() => new[] { image1, image2 }.AsQueryable())
                .Verifiable();

            var coordinator = new GetAllImagesCoordinator(mockContext.Object);

            var result = coordinator.GetAllImages(listing1.Id);

            mockContext.VerifyAll();
            Assert.Contains(image1, result);
            Assert.DoesNotContain(image2, result);
        }

        [Fact]
        public void ReturnsEmptyIfListingHasNoImages()
        {
            var listing1 = new ListingFixture().Build();
            var image1 = new ListingImageFixture().Build();
            var image2 = new ListingImageFixture().Build();

            var mockContext = new Mock<IPortalDbContext>();
            mockContext.Setup(m => m.Query<ListingImage>())
                .Returns(() => new[] { image1, image2 }.AsQueryable())
                .Verifiable();

            var coordinator = new GetAllImagesCoordinator(mockContext.Object);

            var result = coordinator.GetAllImages(listing1.Id);

            mockContext.VerifyAll();
            Assert.Empty(result);
        }
    }
}