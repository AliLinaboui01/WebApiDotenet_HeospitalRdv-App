using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOs.DoctorDto
{
    public class DoctorsDto
    {
        public string? Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }

        public string? Telephone { get; set; }
        public string? Address { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateHiring { get; set; }
        public string? Speciality { get; set; }
        public string? Image { get; set; }
    }
}