using System;
using System.Linq;
using AgentPortal.Domain.Coordinators;
using AgentPortal.Domain.Data;
using AgentPortal.Domain.Http;
using AgentPortal.Domain.Values;
using Microsoft.AspNetCore.Mvc;

namespace AgentPortal.Controllers.Api
{
    [ApiController]
    [Route("api/images")]
    public class ImagesController : ControllerBase
    {
        private readonly IGetAllImagesCoordinator _getAllImagesCoordinator;

        public ImagesController(IGetAllImagesCoordinator getAllImagesCoordinator)
        {
            _getAllImagesCoordinator = getAllImagesCoordinator;
        }

        [Route("{listingId:guid}")]
        [HttpGet]
        public IActionResult GetImages(Guid listingId)
        {
            var result = _getAllImagesCoordinator.GetAllImages(listingId);
            var response = result.Select(img => MapListingImageResponse(img, listingId));

            return Ok(response);
        }

        private ListingImageResponse MapListingImageResponse(ListingImage image, Guid listingId)
        {
            Link[] links =
            {
                new Link
                {
                    Relation = LinkRelValueObject.SELF,
                    Href = Url.Action(nameof(GetImages), new {listingId = listingId})
                },
                new Link
                {
                    Relation = LinkRelValueObject.LISTING,
                    Href = Url.Action("GetListing", "Listings", new {listingId = listingId})
                }
            };

            return new ListingImageResponse()
            {
                Id = image.Id,
                Url = image.Url,
                ListingId = listingId,
                Links = links
            };
        }
    }
}