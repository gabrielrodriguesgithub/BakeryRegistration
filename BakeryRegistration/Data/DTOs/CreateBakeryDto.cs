using BakeryRegistration.Models;
using System.ComponentModel.DataAnnotations;

namespace BakeryRegistration.Data.DTOs
{
    public class CreateBakeryDto
    {
        [Required, StringLength(200)]
        [Display(Name = "Nome da Padaria")]
        public string Name { get; set; }
        public UserModel Owner { get; set; }

        [EmailAddress, StringLength(100)]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [StringLength(11)]
        [Display(Name = "Telefone")]
        public string Phone { get; set; }

        [StringLength(250)]
        [Display(Name = "Endereço")]
        public string AddressStreet { get; set; }

        [StringLength(50)]
        [Display(Name = "Número")]
        public string AddressNumber { get; set; }

        [StringLength(100)]
        [Display(Name = "Complemento")]
        public string AddressComplement { get; set; }

        [StringLength(100)]
        [Display(Name = "Bairro")]
        public string Neighborhood { get; set; }

        [StringLength(40)]
        [Display(Name = "Cidade")]
        public string City { get; set; }

        [StringLength(40)]
        [Display(Name = "Estado")]
        public string State { get; set; }

        [StringLength(500)]
        public string? PhotoPath { get; set; }
        [Display(Name = "Foto Padaria")]
        public IFormFile? Photo { get; set; }
    }
}
