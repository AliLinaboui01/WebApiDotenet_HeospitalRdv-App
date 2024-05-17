using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class MedicalService
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        
        public string Location { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<MedicalServiceDoctor> MedicalServiceDoctors { get; set; } = new List<MedicalServiceDoctor>();


    }
}