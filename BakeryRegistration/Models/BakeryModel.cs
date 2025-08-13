using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BakeryRegistration.Models
{
    public class BakeryModel
    {
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string Name { get; set; }

        [EmailAddress, StringLength(100)]
        public string Email { get; set; }

        [StringLength(11)]
        public string Phone { get; set; }

        [StringLength(250)]
        public string AddressStreet { get; set; }

        [StringLength(50)]
        public string AddressNumber { get; set; }

        [StringLength(100)]
        public string? AddressComplement { get; set; }

        [StringLength(100)]
        public string Neighborhood { get; set; }

        [StringLength(40)]
        public string City { get; set; }

        [StringLength(40)]
        public string State { get; set; }

        [StringLength(500)]
        public string? PhotoPath { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        virtual public UserModel Owner { get; set; }

    }
}
