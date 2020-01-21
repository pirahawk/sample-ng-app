using AgentPortal.Domain.Data;
using AgentPortal.Domain.Db;
using AgentPortal.Domain.Extensions;
using AgentPortal.Domain.Http;
using System;
using System.Threading.Tasks;

namespace AgentPortal.Domain.Coordinators
{
    public class CreateListingCoordinator : ICreateListingCoordinator
    {
        private readonly IPortalDbContext _dbContext;
        private readonly IListingValidatorHelper _validationHelper;

        public CreateListingCoordinator(IPortalDbContext dbContext, IListingValidatorHelper validationHelper)
        {
            _dbContext = dbContext;
            _validationHelper = validationHelper;
        }

        public async Task<Listing> Create(EditListingRequest newListing)
        {
            if (newListing == null) throw new ArgumentNullException(nameof(newListing));

            var listing = new Listing();
            listing.Update(newListing);

            if (!_validationHelper.HasValidFields(listing))
            {
                return null;
            }

            await _dbContext.Add(listing);
            await _dbContext.SaveChanges();

            return listing;
        }
    }
}