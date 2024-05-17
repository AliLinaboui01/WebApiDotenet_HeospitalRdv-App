using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.PatientDto;
using api.Models;

namespace api.Mappers
{
    public static class PatientMapper
    {
        public static PatientDto ToPatientDto(this Patient patient){
            return new PatientDto{
                Id = patient.Id,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                UserName = patient.UserName,
                Email = patient.Email,
                Telephone = patient.Telephone,
                Address = patient.Address,
                Age = DateTime.Today.Year-patient.BirthDate.Year,
                BirthDate = patient.BirthDate.Date.Date,
                Image = patient.Image,
            };
        }
    }
}