using System.Text.RegularExpressions;
using AgentPortal.Domain.Data;

namespace AgentPortal.Domain.Coordinators
{
    public class ListingValidatorHelper : IListingValidatorHelper
    {
        private readonly string validUkPostCodeRegex = @"^[a-zA-Z](\d{1,2}|[a-zA-Z]\d{1,2}|\d[a-zA-Z]|[a-zA-Z]\d[a-zA-Z])\s?\d[a-zA-Z]{2}$";
        
        public bool HasValidFields(Listing listing)
        {
            return listing != null
                   && listing.NumberBedrooms > 0
                   && listing.AskingPrice > 0
                   && !string.IsNullOrWhiteSpace(listing.Address)
                   && !string.IsNullOrWhiteSpace(listing.Description)
                   && Regex.IsMatch(listing.PostCode,validUkPostCodeRegex);
        }
    }
}