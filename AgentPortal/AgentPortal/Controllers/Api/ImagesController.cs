using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AgentPortal.Domain.Coordinators;
using AgentPortal.Domain.Data;
using AgentPortal.Domain.Http;
using AgentPortal.Domain.Values;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgentPortal.Controllers.Api
{
    [ApiController]
    [Route("api/images")]
    public class ImagesController : ControllerBase
    {
        private readonly IGetAllImagesCoordinator _getAllImagesCoordinator;
        private readonly IAddImagesCoordinator _addImagesCoordinator;

        public ImagesController(IGetAllImagesCoordinator getAllImagesCoordinator, IAddImagesCoordinator addImagesCoordinator )
        {
            _getAllImagesCoordinator = getAllImagesCoordinator;
            _addImagesCoordinator = addImagesCoordinator;
        }

        [Route("{listingId:guid}")]
        [HttpGet]
        public IActionResult GetImages(Guid listingId)
        {
            var result = _getAllImagesCoordinator.GetAllImages(listingId);
            var response = result.Select(img => MapListingImageResponse(img, listingId));

            return Ok(response);
        }

        [Route("{listingId:guid}")]
        [HttpPost]
        [RequestSizeLimit(52428800)]
        public async Task<IActionResult> AddImage(Guid listingId)
        {
            foreach (var file in Request.Form.Files)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    var bytes = ms.GetBuffer();
                    await _addImagesCoordinator.AddImage(listingId, bytes, file.ContentType);
                }
            }
            
            return NoContent();
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