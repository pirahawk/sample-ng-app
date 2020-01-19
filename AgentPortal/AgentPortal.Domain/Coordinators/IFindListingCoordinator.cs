using System;
using System.Threading.Tasks;
using AgentPortal.Domain.Data;

namespace AgentPortal.Domain.Coordinators
{
    public interface IFindListingCoordinator
    {
        Task<Listing> Find(Guid listingId);
    }
}