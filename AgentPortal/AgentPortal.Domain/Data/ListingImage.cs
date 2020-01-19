﻿using System;

namespace AgentPortal.Domain.Data
{
    public class ListingImage
    {
        public Guid Id { get; set; }
        public Guid ListingId { get; set; }
        public Listing Listing { get; set; }
        public string Url { get; set; }
    }
}