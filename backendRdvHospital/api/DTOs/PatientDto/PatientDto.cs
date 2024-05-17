using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOs.PatientDto
{
    public class PatientDto
    {
        public string? Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Telephone { get; set; }
        public string? Address { get; set; }
        public int Age { get; set; }
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        public string? Image { get; set; }
    }
}