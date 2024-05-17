using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class MedicalServiceRepository : IMedicalServiceRepository
    {
        private readonly DataContext _dataContext ;
        public MedicalServiceRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<MedicalService> AddMedicalServiceAsync(MedicalService medicalService)
        {
            await _dataContext.MedicalServices.AddAsync(medicalService);
            return medicalService;
        }

        public async Task<MedicalService> DeleteAsync(int id)
        {
            var medicalService = await _dataContext.MedicalServices.FirstOrDefaultAsync(x=>x.Id ==id) ?? throw new NotImplementedException("medical service doesn't exist") ;
            _dataContext.MedicalServices.Remove(medicalService);
            await _dataContext.SaveChangesAsync();
            return medicalService;
        }

        public async Task<List<MedicalService>> GettAllAsync()
        {
            return await _dataContext.MedicalServices.ToListAsync();
        }
    }
}