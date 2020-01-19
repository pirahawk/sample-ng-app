using System;
using System.Collections.Generic;
using AgentPortal.Domain.Data;

namespace AgentPortal.Domain.Coordinators
{
    public interface IGetAllImagesCoordinator
    {
        IEnumerable<ListingImage> GetAllImages(Guid listingId);
    }
}