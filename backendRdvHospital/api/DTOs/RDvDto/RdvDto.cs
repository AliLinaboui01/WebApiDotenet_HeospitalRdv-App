using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.DTOs.RDvDto
{
    public class RdvDto
    {
        [Required]
        public DateTime AppointmentDateTime { get; set; }
        [Required]
        
        public string Reason { get; set; } = string.Empty;
        
        // Foreign key to Patient
        [Required]
        public string PatientId { get; set; } = string.Empty;
        [Required]
        public string DoctorId { get; set; } = string.Empty;
        public string? PatientName { get; set; }
        public string? DoctorName { get; set; }
        public string? PatientImage { get; set; }
        public string? DoctorImage { get; set; }

    }
}