using System;
using System.Threading.Tasks;
using AgentPortal.Domain.Data;
using AgentPortal.Domain.Db;
using AgentPortal.Domain.Http;

namespace AgentPortal.Domain.Coordinators
{
    public class EditListingCoordinator : IEditListingCoordinator
    {
        private readonly IPortalDbContext _dbContext;

        public EditListingCoordinator(IPortalDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Listing> Edit(Guid listingId, Listing existingListing, EditListingRequest editRequest)
        {
            if (existingListing == null) throw new ArgumentNullException(nameof(existingListing));
            if (editRequest == null) throw new ArgumentNullException(nameof(editRequest));

            _dbContext.Attach(existingListing);

            await UpdateListing(existingListing, editRequest);
            
            
            return existingListing;
        }

        private async Task UpdateListing(Listing existingListing, EditListingRequest editRequest)
        {
            existingListing.Address = editRequest.Address;
            existingListing.AskingPrice = editRequest.AskingPrice;
            existingListing.Description = editRequest.Description;
            existingListing.Expired = editRequest.Expired;
            existingListing.NumberBedrooms = editRequest.NumberBedrooms;
            existingListing.PostCode = editRequest.PostCode;

            await _dbContext.SaveChanges();
        }
    }
}