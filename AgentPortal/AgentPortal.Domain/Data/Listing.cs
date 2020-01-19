using System;
using System.Collections.Generic;

namespace AgentPortal.Domain.Data
{
    public class Listing
    {
        public Guid Id { get; set; }
        public int NumberBedrooms { get; set; }
        public string PostCode { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public decimal AskingPrice { get; set; }
        public bool Expired { get; set; }
        public IEnumerable<ListingImage> Images { get; set; }
    }
}
