using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOs.RDvDto
{
    public class CreateRdvDto
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
    }
}