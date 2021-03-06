﻿using System;

namespace AgentPortal.Domain.Http
{
    public class ListingResponse
    {
        public Guid Id { get; set; }
        public int NumberBedrooms { get; set; }
        public string PostCode { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public decimal AskingPrice { get; set; }
        public bool Expired { get; set; }
        public Link[] Links { get; set; }
    }
}