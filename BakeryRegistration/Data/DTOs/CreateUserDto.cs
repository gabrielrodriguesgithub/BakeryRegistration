using System.ComponentModel.DataAnnotations;

namespace BakeryRegistration.Data.DTOs
{
    public class CreateUserDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [Compare("Password")]
        public string RePassword { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        [RegularExpression(@"\d{14}")]
        public string CNPJ { get; set; }
    }
}