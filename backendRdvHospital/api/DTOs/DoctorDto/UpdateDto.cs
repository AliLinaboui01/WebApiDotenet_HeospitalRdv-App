using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOs.DoctorDto
{
    public class UpdateDto
    {
        [Required]
        public string? FirstName { get; set; }

        [Required]
        public string? LastName { get; set; }
        [Required]
        [MaxLength(125, ErrorMessage = "Symbol cannot be over 12 over characters")]
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

        
        
        
    }
}