using System.Threading.Tasks;
using AgentPortal.Domain.Coordinators;
using AgentPortal.Domain.Data;
using AgentPortal.Domain.Db;
using AgentPortal.Domain.Store;
using AgentPortal.Domain.Tests.Data;
using Moq;
using Xunit;

namespace AgentPortal.Domain.Tests.Coordinators
{
    public class AddImagesCoordinatorTest
    {
        [Fact]
        public async Task CanUploadImageAndPersistAsExpected()
        {
            var bytes = new byte[] {1,2,3 };
            var imageMediaType = "image/jpg";
            var mockReturnUrl = "someUrl";

            var listing1 = new ListingFixture().Build();
            var mockFindListingCoordinator = new Mock<IFindListingCoordinator>();
            mockFindListingCoordinator.Setup(m => m.Find(listing1.Id)).ReturnsAsync(listing1).Verifiable();

            var imageStoreMock = new Mock<IImageStore>();
            imageStoreMock.Setup(m => m.PersistArticleEntryMedia(It.IsAny<string>(), bytes, imageMediaType)).ReturnsAsync(mockReturnUrl).Verifiable();

            var mockDbContext = new Mock<IPortalDbContext>();
            mockDbContext.Setup(m=>m.Add(It.IsAny<ListingImage>())).Verifiable();
            mockDbContext.Setup(m => m.SaveChanges()).Verifiable();

            var coordinator = new AddImagesCoordinator(imageStoreMock.Object, mockDbContext.Object, mockFindListingCoordinator.Object);

            var result = await coordinator.AddImage(listing1.Id, bytes, imageMediaType);

            mockFindListingCoordinator.VerifyAll();
            imageStoreMock.VerifyAll();
            mockDbContext.VerifyAll();

            Assert.Equal(result.ListingId, listing1.Id);
            Assert.Equal(result.Listing, listing1);
            Assert.Equal(result.Url, mockReturnUrl);
        }
    }
}