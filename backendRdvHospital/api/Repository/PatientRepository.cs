using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTOs.PatientDto;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class PatientRepository : IPatientRepository
    {
        private readonly DataContext _dataContext;
        private readonly UserManager<User> _userManager;
        public PatientRepository(DataContext dataContext, UserManager<User> userManager)
        {
            _dataContext = dataContext;
             _userManager = userManager;
        }

        public async Task<Patient?> DeletePatientAync(string id)
        {
            var PatientModel = await _dataContext.Patients.FirstOrDefaultAsync(x => x.Id == id);

            if (PatientModel == null)
            {
                return null;
            }
            // Fetch all RDV records associated with the doctor
            var rdvs = await _dataContext.RDVs.Where(r => r.PatientId == id).ToListAsync();

            // Delete all associated RDV records
            _dataContext.RDVs.RemoveRange(rdvs);

            _dataContext.Patients.Remove(PatientModel);
            await _dataContext.SaveChangesAsync();
            return PatientModel;
        }

        public async Task<List<Patient>> GetAllAsync()
        {
            return await _dataContext.Patients
            .Include(p=>p.RDVs)
            .ToListAsync();
        }

        public async Task<Patient?> GetByIDAsync(string id)
        {
            return await _dataContext.Patients.FindAsync(id);
        }

        public async Task<Patient?> UpdateDoctorAsync(string id, UpdatePatientDto updateDto)
        {
            var existingUser = await _dataContext.Users.FirstOrDefaultAsync(x => x.Id ==id);
            if(existingUser==null)
                return null;
            // Check if the user is actually a Doctor
            if (!(existingUser is Patient existingPatient))
                return null;

            existingPatient.FirstName = updateDto.FirstName!;
            existingPatient.Email = updateDto.Email;
            existingPatient.LastName = updateDto.LastName!;
            existingPatient.UserName = updateDto.UserName;
            existingPatient.Address = updateDto.Address!;
            existingPatient.Telephone = updateDto.Telephone!;
            var newPasswordHash = _userManager.PasswordHasher.HashPassword(existingPatient, updateDto.Password!);
            existingPatient.PasswordHash = newPasswordHash;

            var normalizedEmail =  _userManager.NormalizeEmail(updateDto.Email);
            existingPatient.NormalizedEmail = normalizedEmail;
            var normalizedUserName =  _userManager.NormalizeName(updateDto.UserName);
            existingPatient.NormalizedUserName = normalizedUserName;
            await _dataContext.SaveChangesAsync();

            return existingPatient;
        }
    }
}