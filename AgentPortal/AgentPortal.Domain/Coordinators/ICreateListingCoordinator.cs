using System.Threading.Tasks;
using AgentPortal.Domain.Data;
using AgentPortal.Domain.Http;

namespace AgentPortal.Domain.Coordinators
{
    public interface ICreateListingCoordinator
    {
        Task<Listing> Create(EditListingRequest newListing);
    }
}