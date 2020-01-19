using AgentPortal.Domain.Data;
using System.Linq;
using System.Threading.Tasks;

namespace AgentPortal.Domain.Coordinators
{
    public interface IGetAllListingsCoordinator
    {
        IQueryable<Listing> GetAllListings();
    }
}