using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    [Table("Rdvs")]
    public class RDV
    {
        public int Id { get; set; }
        public DateTime AppointmentDateTime { get; set; }
        public string Reason { get; set; } = string.Empty;
        
        // Foreign key to Patient
        public string PatientId { get; set; } = string.Empty;
        // Navigation property for Patient
        public Patient Patient { get; set; } =new Patient();
        
        // Foreign key to Doctor
        public string DoctorId { get; set; } = string.Empty;
        // Navigation property for Doctor
        public Doctor Doctor { get; set; } =new Doctor();
    }
}