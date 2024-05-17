using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.Models;

namespace api.Repository
{
    public class MedicalServiceDoctorRepository : IMedicalServiceDoctorRepository
    {
        private readonly DataContext _dataContext;
        public MedicalServiceDoctorRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<MedicalServiceDoctor> CreateAsync(MedicalServiceDoctor medicalServiceDoctor)
        {
            await _dataContext.MedicalServiceDoctors.AddAsync(medicalServiceDoctor);
            return medicalServiceDoctor;
        }
    }
}