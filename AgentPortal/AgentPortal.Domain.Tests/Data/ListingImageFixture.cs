using System;
using AgentPortal.Domain.Data;

namespace AgentPortal.Domain.Tests.Data
{
    public class ListingImageFixture
    {
        public string Url { get; set; }
        public Guid ListingId { get; set; }
        public Guid Id { get; set; }
        public Listing Listing { get; set; }

        public ListingImageFixture()
        {
            Id = Guid.NewGuid();
            Listing = new ListingFixture().Build();
            ListingId = Listing.Id;
            Url = "http://test.com";
        }

        public ListingImageFixture ForListing(Listing listing)
        {
            if (listing == null)
            {
                return this;
            }

            ListingId = listing.Id;
            Listing = listing;
            return this;
        }

        public ListingImage Build()
        {
            var img = new ListingImage
            {
                Id = Id,
                ListingId = ListingId,
                Listing = Listing,
                Url = Url,
            };
            return img;
        }
    }
}