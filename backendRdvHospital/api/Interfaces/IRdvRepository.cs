using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.RDvDto;
using api.Models;

namespace api.Interfaces
{
    public interface IRdvRepository
    {
        Task<List<RDV>> GetAllAsync();
        Task<RDV> CreateRdvAsync(RDV createRdvDto);
        Task<RDV> GetRdvWithPatientAndDoctor(int id);
        Task<RDV?> DeleteAsync(int id);
        Task<RDV> GetByIdAsync(int id);
        Task<List<RDV>> GetByIdPatientAsync(string id);
        Task<List<RDV>> GetByIdDoctorAsync(string id);
    }
}