using System;
using System.Linq;
using System.Threading.Tasks;
using AgentPortal.Domain.Coordinators;
using AgentPortal.Domain.Data;
using AgentPortal.Domain.Db;
using AgentPortal.Domain.Http;
using AgentPortal.Domain.Values;
using Microsoft.AspNetCore.Mvc;

namespace AgentPortal.Controllers.Api
{
    [ApiController]
    [Route("api/listings")]
    public class ListingsController : ControllerBase
    {
        private readonly IGetAllListingsCoordinator _getListingsCoordinator;

        public ListingsController(IGetAllListingsCoordinator getListingsCoordinator)
        {
            _getListingsCoordinator = getListingsCoordinator;
        }

        [HttpGet]
        public IActionResult GetListings()
        {
            var allListings = _getListingsCoordinator.GetAllListings();
            var response = allListings.AsEnumerable().Select(MapListingResponse);
            return Ok(response);
        }

        [Route("{listingId:guid}")]
        [HttpGet]
        public IActionResult GetListing(Guid listingId)
        {
            return Ok();
        }

        private ListingResponse MapListingResponse(Listing listing)
        {
            Link[] links =
            {
                new Link {Relation = LinkRelValueObject.SELF, Href = Url.Action(nameof(GetListing), new { listingId = listing.Id})}
            };

            return new ListingResponse()
            {
                Id = listing.Id,
                NumberBedrooms = listing.NumberBedrooms,
                PostCode = listing.PostCode,
                Address = listing.Address,
                Description = listing.Description,
                AskingPrice = listing.AskingPrice,
                Expired = listing.Expired,
                Links = links
            };
        }
    }
}