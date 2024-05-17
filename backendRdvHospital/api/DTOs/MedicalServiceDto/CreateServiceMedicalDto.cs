using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOs.MedicalServiceDto
{
    public class CreateServiceMedicalDto
    {
        
        public string Location { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? Name { get; set; }
    }
}