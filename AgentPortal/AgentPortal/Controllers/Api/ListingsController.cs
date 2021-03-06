﻿using AgentPortal.Domain.Coordinators;
using AgentPortal.Domain.Data;
using AgentPortal.Domain.Http;
using AgentPortal.Domain.Values;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AgentPortal.Controllers.Api
{
    [ApiController]
    [Route("api/listings")]
    public class ListingsController : ControllerBase
    {
        private readonly IGetAllListingsCoordinator _getListingsCoordinator;
        private readonly IFindListingCoordinator _findListingCoordinator;
        private readonly IEditListingCoordinator _editListingCoordinator;
        private readonly ICreateListingCoordinator _createListingCoordinator;

        public ListingsController(IGetAllListingsCoordinator getListingsCoordinator,
            IFindListingCoordinator findListingCoordinator, 
            IEditListingCoordinator editListingCoordinator,
            ICreateListingCoordinator createListingCoordinator)
        {
            _getListingsCoordinator = getListingsCoordinator;
            _findListingCoordinator = findListingCoordinator;
            _editListingCoordinator = editListingCoordinator;
            _createListingCoordinator = createListingCoordinator;
        }

        [Route("")]
        [HttpGet]
        public IActionResult GetListings()
        {
            var allListings = _getListingsCoordinator.GetAllListings();
            var response = allListings.AsEnumerable().Select(MapListingResponse);
            return Ok(response);
        }

        [Route("")]
        [HttpPost]
        public async Task<IActionResult> AddNewListing(EditListingRequest newListingRequest)
        {
            if (newListingRequest == null)
            {
                return BadRequest();
            }

            var newListing = await _createListingCoordinator.Create(newListingRequest);

            if (newListing == null)
            {
                return BadRequest();
            }

            var response = MapListingResponse(newListing);
            return Ok(response);
        }

        [Route("{listingId:guid}")]
        [HttpGet]
        public async Task<IActionResult> GetListing(Guid listingId)
        {
            var result = await _findListingCoordinator.Find(listingId);
            if (result == null)
            {
                return NotFound();
            }

            var response = MapListingResponse(result);

            return Ok(response);
        }

        [Route("{listingId:guid}")]
        [HttpPut]
        public async Task<IActionResult> EditListing(Guid listingId, EditListingRequest editListingRequest)
        {
            var existingListing = await _findListingCoordinator.Find(listingId);
            
            if (existingListing == null)
            {
                return NotFound();
            }

            var result = await _editListingCoordinator.Edit(listingId, existingListing, editListingRequest);

            if (result == null)
            {
                return BadRequest();
            }

            return NoContent();
        }

        private ListingResponse MapListingResponse(Listing listing)
        {
            Link[] links =
            {
                new Link
                {
                    Relation = LinkRelValueObject.SELF,
                    Href = Url.Action(nameof(GetListing), new {listingId = listing.Id})
                },
                new Link
                {
                    Relation = LinkRelValueObject.IMAGES,
                    Href = Url.Action("GetImages", "Images", new {listingId = listing.Id})
                }
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