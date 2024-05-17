using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Interfaces
{
    public interface IMedicalServiceRepository
    {
        Task<List<MedicalService>> GettAllAsync();
        Task<MedicalService> AddMedicalServiceAsync(MedicalService medicalService);
        Task<MedicalService> DeleteAsync(int id);
    }
}