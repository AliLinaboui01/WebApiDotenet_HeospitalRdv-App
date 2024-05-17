using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOs.RDvDto
{
    public class Rdv
    {
        
        public DateTime AppointmentDateTime { get; set; }
        
        public string Reason { get; set; } = string.Empty;
        
        public string PatientId { get; set; } = string.Empty;
        
        public string DoctorId { get; set; } = string.Empty;
    }
}