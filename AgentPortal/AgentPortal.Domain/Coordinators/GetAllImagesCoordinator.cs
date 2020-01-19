using System;
using System.Collections.Generic;
using System.Linq;
using AgentPortal.Domain.Data;
using AgentPortal.Domain.Db;

namespace AgentPortal.Domain.Coordinators
{
    public class GetAllImagesCoordinator : IGetAllImagesCoordinator
    {
        private readonly IPortalDbContext _dbContext;

        public GetAllImagesCoordinator(IPortalDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<ListingImage> GetAllImages(Guid listingId)
        {
            var allImages = _dbContext.Query<ListingImage>()
                .Where(i => i.ListingId == listingId)
                .ToArray();

            return allImages;
        }
    }
}