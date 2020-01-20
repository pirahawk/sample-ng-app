using System;
using System.Threading.Tasks;
using AgentPortal.Domain.Data;
using AgentPortal.Domain.Http;

namespace AgentPortal.Domain.Coordinators
{
    public interface IEditListingCoordinator
    {
        Task<Listing> Edit(Guid listingId, Listing existingListing, EditListingRequest editRequest);
    }
}