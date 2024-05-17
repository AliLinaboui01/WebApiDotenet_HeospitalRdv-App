using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    [Table("doctors")]
    public class Doctor :User
    {
        public DateTime DateHiring { get; set; }
        public string Speciality { get; set; } = string.Empty;
        // Navigation property for DoctorSchedules
        public List<DoctorSchedule> DoctorSchedules { get; set; } = new List<DoctorSchedule>();
        public List<MedicalServiceDoctor> MedicalServiceDoctors { get; set; } = new List<MedicalServiceDoctor>();
        public List<RDV> RDVs { get; set; } = new List<RDV>();
    }
}