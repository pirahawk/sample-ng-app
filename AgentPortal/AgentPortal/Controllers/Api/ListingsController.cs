﻿using System;
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
        private readonly IFindListingCoordinator _findListingCoordinator;

        public ListingsController(IGetAllListingsCoordinator getListingsCoordinator, IFindListingCoordinator findListingCoordinator)
        {
            _getListingsCoordinator = getListingsCoordinator;
            _findListingCoordinator = findListingCoordinator;
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
        public async Task<IActionResult> GetListing(Guid listingId) { 
            var result = await _findListingCoordinator.Find(listingId);
            if (result == null)
            {
                return NotFound();
            }

            var response = MapListingResponse(result);

            return Ok(response);
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