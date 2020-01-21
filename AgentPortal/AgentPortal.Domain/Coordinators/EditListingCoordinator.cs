using System;
using System.Threading.Tasks;
using AgentPortal.Domain.Data;
using AgentPortal.Domain.Db;
using AgentPortal.Domain.Extensions;
using AgentPortal.Domain.Http;

namespace AgentPortal.Domain.Coordinators
{
    public class EditListingCoordinator : IEditListingCoordinator
    {
        private readonly IPortalDbContext _dbContext;
        private readonly IListingValidatorHelper _validationHelper;

        public EditListingCoordinator(IPortalDbContext dbContext, IListingValidatorHelper validationHelper)
        {
            _dbContext = dbContext;
            _validationHelper = validationHelper;
        }

        public async Task<Listing> Edit(Guid listingId, Listing existingListing, EditListingRequest editRequest)
        {
            if (existingListing == null) throw new ArgumentNullException(nameof(existingListing));
            if (editRequest == null) throw new ArgumentNullException(nameof(editRequest));

            _dbContext.Attach(existingListing);
            existingListing.Update(editRequest);

            if (!_validationHelper.HasValidFields(existingListing))
            {
                return null;
            }

            await _dbContext.SaveChanges();
            return existingListing;
        }

       
    }
}