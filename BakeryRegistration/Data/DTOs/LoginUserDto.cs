using System.ComponentModel.DataAnnotations;

namespace BakeryRegistration.Data.DTOs
{
    public class LoginUserDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}