using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.DTOs.DoctorScheduelDto
{
    public class CreateDoctorScheduelDto
    {
        [Required]
        public string? DoctorId { get; set; }
        
        [Required]
        public TimeSpan AvailableStartTime { get; set; }
        [Required]
        public Doctor? Doctor { get; set; }
    }
}