using System;

namespace AgentPortal.Domain.Http
{
    public class ListingImageResponse
    {
        public Guid Id { get; set; }
        public Guid ListingId { get; set; }
        public string Url { get; set; }
        public Link[] Links { get; set; }
    }

    public class ListingImageRequest
    {
        public Guid Id { get; set; }
        public Guid ListingId { get; set; }
        public string MediaContentType { get; set; }
        public byte[] Content { get; set; }
    }
}