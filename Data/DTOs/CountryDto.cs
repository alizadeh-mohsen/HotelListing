using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HotelListing.Data.DTOs

{
    public class CreateCountryDto
    {
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name is too long")]
        public string Name { get; set; }
    }

    public class CountryDto : CreateCountryDto
    {
        public int Id { get; set; }
        public IList<HotelDto> Hotels { get; set; }


    }
}
