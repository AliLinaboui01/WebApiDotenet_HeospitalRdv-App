using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class MedicalServiceDoctor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Description { get; set; }
        public string DoctorId { get; set; } =string.Empty;
        public Doctor Doctor { get; set; } = new Doctor();
        public int MedicalServiceId { get; set; }
        public MedicalService MedicalService { get; set; } = new MedicalService();
        
    }  
}