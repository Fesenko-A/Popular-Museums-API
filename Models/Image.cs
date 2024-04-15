using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PopularMuseumsAPI.Models
{
    public class Image {
        [Key]
        public int ImageId { get; set; }
        public string ImageUrl { get; set; }
        public int MuseumId { get; set; }
        [ForeignKey(nameof(MuseumId))]
        public Museum Museum { get; set; }
    }
}
