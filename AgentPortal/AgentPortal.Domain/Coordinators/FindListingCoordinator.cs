using System;
using System.Threading.Tasks;
using AgentPortal.Domain.Data;
using AgentPortal.Domain.Db;

namespace AgentPortal.Domain.Coordinators
{
    public class FindListingCoordinator : IFindListingCoordinator
    {
        private readonly IPortalDbContext _dbContext;

        public FindListingCoordinator(IPortalDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Listing> Find(Guid listingId)
        {
            var result = await _dbContext.Find<Listing>(listingId);
            return result;
        }
    }
}