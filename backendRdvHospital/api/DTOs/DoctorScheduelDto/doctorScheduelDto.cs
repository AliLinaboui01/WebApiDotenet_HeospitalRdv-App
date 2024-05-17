using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOs.DoctorScheduelDto
{
    public class doctorScheduelDto
    {
        
        public string? DoctorId { get; set; }
        
        public TimeSpan AvailableStartTime { get; set; }
        public TimeSpan AvailableEndTime { get; set; }
        public string? PatientName { get; set; }
        public string? DoctorName { get; set; }
    }
}