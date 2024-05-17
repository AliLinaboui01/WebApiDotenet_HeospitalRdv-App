using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    [Table("DoctorSchedules")]
    public class DoctorSchedule
    {
        public int Id { get; set; }
        public string? DoctorId { get; set; }
        public TimeSpan AvailableStartTime { get; set; }
        public TimeSpan AvailableEndTime { get; set; }
        public Doctor Doctor { get; set; }=new Doctor();
        public string? PatientId { get; set; }
        public Patient? Patient { get; set; }
    }
}