using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTOs.DoctorDto;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly DataContext _dataContext;
        private readonly UserManager<User> _userManager;
        private readonly IImageService _imageService;
        public DoctorRepository(DataContext dataContext, UserManager<User> userManager, IImageService imageService)
        {
            _dataContext = dataContext;
            _userManager = userManager;
            _imageService = imageService;
        }

        public async Task<Doctor?> DeleteDoctorAync(string id)
        {
            var DocterModel = await _dataContext.Doctors.FirstOrDefaultAsync(x => x.Id == id);

            if (DocterModel == null)
            {
                return null;
            }
            // Fetch all RDV records associated with the doctor
            var rdvs = await _dataContext.RDVs.Where(r => r.DoctorId == id).ToListAsync();
            var doctorSchedules = await _dataContext.DoctorScheduels.Where(ds=>ds.DoctorId==id).ToListAsync();
            // Delete all associated RDV records
            _dataContext.RDVs.RemoveRange(rdvs);
            //Delete All associated Doctor Schedule records
            _dataContext.DoctorScheduels.RemoveRange(doctorSchedules);
            _dataContext.Doctors.Remove(DocterModel);

            //save to database
            await _dataContext.SaveChangesAsync();
            return DocterModel;
        }

        public async Task<List<Doctor>> GetAllAsync()
        {
            return await _dataContext.Doctors
            .Include(d => d.RDVs)
            .ToListAsync();
        }

        

        public async Task<Doctor?> GetByIdAsync(string id)
        {
            return await _dataContext.Doctors.FindAsync(id);
            
        }

        public async Task<Doctor?> UpdateDoctorAsync(string id, UpdateDto updateDto)
        {
            var existingUser = await _dataContext.Users.FirstOrDefaultAsync(x => x.Id ==id);
            if(existingUser==null)
                return null;
            // Check if the user is actually a Doctor
            if (!(existingUser is Doctor existingDoctor))
                return null;

            existingDoctor.FirstName = updateDto.FirstName!;
            existingDoctor.Email = updateDto.Email;
            existingDoctor.LastName = updateDto.LastName!;
            existingDoctor.UserName = updateDto.UserName;
            existingDoctor.Address = updateDto.Address!;
            existingDoctor.Telephone = updateDto.Telephone!;
            var newPasswordHash = _userManager.PasswordHasher.HashPassword(existingDoctor, updateDto.Password!);
            existingDoctor.PasswordHash = newPasswordHash;
            
            var normalizedEmail =  _userManager.NormalizeEmail(updateDto.Email);
            existingDoctor.NormalizedEmail = normalizedEmail;
            var normalizedUserName =  _userManager.NormalizeName(updateDto.UserName);
            existingDoctor.NormalizedUserName = normalizedUserName;
            if(updateDto.Image!=null){
                existingDoctor.Image = "http://localhost:5299/Uploads/Doctors/"+_imageService.UploadImage("Doctor",updateDto.Image);
            }
            await _dataContext.SaveChangesAsync();

            return existingDoctor;
        }
    }
}