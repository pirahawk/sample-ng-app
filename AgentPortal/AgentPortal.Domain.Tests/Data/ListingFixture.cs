using System;
using System.Collections.Generic;
using System.Linq;
using AgentPortal.Domain.Data;

namespace AgentPortal.Domain.Tests.Data
{
    public class ListingFixture
    {
        public IEnumerable<ListingImage> Images { get; set; }
        public bool Expired { get; set; }
        public decimal AskingPrice { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string PostCode { get; set; }
        public int NumberBedrooms { get; set; }
        public Guid Id { get; set; }

        public ListingFixture()
        {
            Id = Guid.NewGuid();
            Description = "One Hyde Park, 100";
            Address = "100 Knightsbridge, London";
            AskingPrice = 200000000m;
            NumberBedrooms = 10;
            PostCode = "SW1X 7LJ";
            Images = Enumerable.Empty<ListingImage>();
        }

        public Listing Build()
        {
            return new Listing()
            {
                Id = Id,
                NumberBedrooms= NumberBedrooms,
                PostCode = PostCode,
                Address = Address,
                Description = Description,
                AskingPrice = AskingPrice,
                Expired = Expired,
                Images = Images

            };
        }
    }
}