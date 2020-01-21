using System;
using System.Linq;
using System.Threading.Tasks;
using AgentPortal.Domain.Data;
using AgentPortal.Domain.Db;
using AgentPortal.Domain.Store;

namespace AgentPortal.Domain.Coordinators
{
    public class AddImagesCoordinator : IAddImagesCoordinator
    {
        private readonly IImageStore _imageStore;
        private readonly IPortalDbContext _dbContext;
        private readonly IFindListingCoordinator _findListingCoordinator;

        public AddImagesCoordinator(IImageStore imageStore, IPortalDbContext dbContext, IFindListingCoordinator findListingCoordinator)
        {
            _imageStore = imageStore;
            _dbContext = dbContext;
            _findListingCoordinator = findListingCoordinator;
        }
        public async Task<ListingImage> AddImage(Guid listingId, byte[] imageContent, string imageMediaType)
        {
            if (imageContent == null) throw new ArgumentNullException(nameof(imageContent));
            if (string.IsNullOrWhiteSpace(imageMediaType))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(imageMediaType));

            var listing = await _findListingCoordinator.Find(listingId);

            if (listing == null)
            {
                return null;
            }

            var imageFileName = $"{Guid.NewGuid()}";
            var url = await _imageStore.PersistArticleEntryMedia(imageFileName, imageContent, imageMediaType);

            var image = new ListingImage()
            {
                Listing = listing,
                ListingId = listingId,
                Url = url
            };

            await _dbContext.Add(image);
            await _dbContext.SaveChanges();

            return image;
        }
    }
}