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
        [MaxLength(15,ErrorMessage ="the reason field should contains only 15 caracters")]
        public string Reason { get; set; } = string.Empty;
        
        // Foreign key to Patient
        [Required]
        public string PatientId { get; set; } = string.Empty;
        [Required]
        public string DoctorId { get; set; } = string.Empty;
        [Required]
        public Patient? Patient { get; set; }
        [Required]
        public Doctor? Doctor { get; set; }
    }
}