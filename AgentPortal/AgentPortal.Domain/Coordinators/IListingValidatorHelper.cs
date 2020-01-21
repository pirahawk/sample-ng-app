using AgentPortal.Domain.Data;

namespace AgentPortal.Domain.Coordinators
{
    public interface IListingValidatorHelper
    {
        bool HasValidFields(Listing listing);
    }
}