using System.ComponentModel.DataAnnotations;

namespace HotelListing.Data.DTOs
{
    public class UserLoginDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(15, MinimumLength = 4)]
        public string Password { get; set; }
    }


    public class UserDto : UserLoginDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}
