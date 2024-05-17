using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.MedicalServiceDto;
using api.Models;

namespace api.Mappers
{
    public static class MedicalServiceMapper
    {
        public static CreateServiceMedicalDto ToCreateServiceMedicalDto(this MedicalService medicalService){
            return new CreateServiceMedicalDto{
                Name = medicalService.Name,
                Location = medicalService.Location,
                Description = medicalService.Description
            };
        }

        public static MedicalServiceDto ToMedicalServiceDto(this MedicalService medicalService){
            return new MedicalServiceDto{
                Id =medicalService.Id,
                Name = medicalService.Name,
                Location = medicalService.Location,
                Description = medicalService.Description
            };
        }
    }
}