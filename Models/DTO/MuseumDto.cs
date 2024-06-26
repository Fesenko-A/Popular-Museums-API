﻿namespace PopularMuseumsAPI.Models.DTO {
    public class MuseumDto {
        public string Name { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public uint VisitorsPerYear { get; set; }
        public string WebsiteLink { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public uint FoundationYear { get; set; }
    }
}
