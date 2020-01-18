using System;

namespace AgentPortal.Domain.Data
{
    public class ListingImage
    {
        public Guid Id { get; set; }
        public string Url { get; set; }
        public string ContentMd5Hash { get; set; }
    }
}