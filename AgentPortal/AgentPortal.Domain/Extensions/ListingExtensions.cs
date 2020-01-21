using AgentPortal.Domain.Data;
using AgentPortal.Domain.Http;

namespace AgentPortal.Domain.Extensions
{
    public static class ListingExtensions
    {
        public static void Update(this Listing listing, EditListingRequest request)
        {
            listing.Address = request.Address;
            listing.AskingPrice = request.AskingPrice;
            listing.Description = request.Description;
            listing.Expired = request.Expired;
            listing.NumberBedrooms = request.NumberBedrooms;
            listing.PostCode = request.PostCode;
        }
    }
}