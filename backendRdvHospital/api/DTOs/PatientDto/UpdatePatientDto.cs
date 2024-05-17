using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOs.PatientDto
{
    public class UpdatePatientDto
    {
        [Required]
        public string? FirstName { get; set; }

        [Required]
        public string? LastName { get; set; }
        [Required]
        public string? UserName { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }

        [Required]
        public string? Telephone { get; set; }

        [Required]
        public string? Address { get; set; }

        public IFormFile? Image { get; set; }

        
    }
}