using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.DoctorDto;
using api.Models;

namespace api.Interfaces
{
    public interface IDoctorRepository
    {
        Task<List<Doctor>> GetAllAsync();
        Task<Doctor?> GetByIdAsync(string id);
        
        Task<Doctor?> UpdateDoctorAsync(string id, UpdateDto updateDto);
        Task<Doctor?> DeleteDoctorAync(string id);
    }
}