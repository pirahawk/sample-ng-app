using System;
using System.Threading.Tasks;
using AgentPortal.Domain.Data;

namespace AgentPortal.Domain.Coordinators
{
    public interface IAddImagesCoordinator
    {
        Task<ListingImage> AddImage(Guid listingId, byte[] imageContent, string imageMediaType);
    }
}