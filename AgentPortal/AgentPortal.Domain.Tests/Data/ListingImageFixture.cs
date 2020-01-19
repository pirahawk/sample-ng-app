using System;
using AgentPortal.Domain.Data;

namespace AgentPortal.Domain.Tests.Data
{
    public class ListingImageFixture
    {
        public string ContentMd5Hash { get; set; }
        public string Url { get; set; }
        public Guid ListingId { get; set; }
        public Guid Id { get; set; }
        public Listing Listing { get; set; }

        public ListingImageFixture()
        {
            Id = new Guid();
            Listing = new ListingFixture().Build();
            ListingId = Listing.Id;
            Url = "http://test.com";
            ContentMd5Hash = "123";
        }

        public ListingImage Build()
        {
            var img = new ListingImage
            {
                Id = Id,
                ListingId = ListingId,
                Listing = Listing,
                Url = Url,
                ContentMd5Hash = ContentMd5Hash
            };
            return img;
        }
    }
}