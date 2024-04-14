using System.ComponentModel.DataAnnotations;

namespace PopularMuseumsAPI.Models {
    public class Museum {
        [Key]
        public int MuseumId { get; set; }
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
