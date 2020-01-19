using AgentPortal.Domain.Data;
using AgentPortal.Domain.Db;
using System.Linq;

namespace AgentPortal.Domain.Coordinators
{
    public class GetAllListingsCoordinator : IGetAllListingsCoordinator
    {
        private readonly IPortalDbContext _dbContext;

        public GetAllListingsCoordinator(IPortalDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<Listing> GetAllListings()
        {
            var allListings =  _dbContext.Query<Listing>()
                .Where(listing => !listing.Expired);

            return allListings;
        }
     }
}