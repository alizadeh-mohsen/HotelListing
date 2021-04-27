using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelListing.Data.DTOs
{
    public class CreateHotelDto
    {
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Length should be between 3 and 50")]
        public string Name { get; set; }

        [Required]
        [Range(1, 10)]
        public double Rating { get; set; }

        [Required]
        public int CountryId { get; set; }
        public CountryDto Country { get; set; }
    }
    public class HotelDto : CreateHotelDto
    {
        public int Id { get; set; }


    }
}
