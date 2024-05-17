using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.PatientDto;
using api.Models;
using Microsoft.AspNetCore.Mvc;

namespace api.Interfaces
{
    public interface IPatientRepository
    {
        Task<List<Patient>> GetAllAsync(); 
        Task<Patient?> GetByIDAsync(string id);
        Task<Patient?> UpdateDoctorAsync(string id, UpdatePatientDto updateDto);
        Task<Patient?> DeletePatientAync(string id);
    }
}