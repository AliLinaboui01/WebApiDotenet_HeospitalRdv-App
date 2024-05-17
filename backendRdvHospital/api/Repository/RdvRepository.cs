using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTOs.MedicalServiceDto;
using api.DTOs.RDvDto;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class RdvRepository : IRdvRepository
    {
        private readonly DataContext _dataContext;
        private readonly IDoctorScheduelRepository _doctorScheduleRepository;
        private readonly IMedicalServiceRepository _medicalServiceRepository;
        private readonly IMedicalServiceDoctorRepository _medicalServiceDoctorRepository;
        private readonly IRdvService _rdvService;
        public RdvRepository(DataContext dataContext , IDoctorScheduelRepository doctorScheduleRepository, IMedicalServiceRepository medicalServiceRepository, IMedicalServiceDoctorRepository medicalServiceDoctorRepository, IRdvService rdvService)
        {
            _dataContext = dataContext;           
            _doctorScheduleRepository = doctorScheduleRepository;
            _medicalServiceRepository = medicalServiceRepository;
            _medicalServiceDoctorRepository = medicalServiceDoctorRepository;
            _rdvService = rdvService;
        }

        
        public async Task<RDV> CreateRdvAsync(RDV rdv)
        {
            using (var transaction = await _dataContext.Database.BeginTransactionAsync())
            {
                try
                {
                    if(!await _rdvService.Checke(rdv,rdv.PatientId,rdv.DoctorId))
                        throw new ArgumentException("patient or doctore doesn't exist");

                    var isReserved = await _doctorScheduleRepository.IsPatientAlreadyReserved(rdv.PatientId, rdv.AppointmentDateTime);
                    if (isReserved)
                    {
                        throw new InvalidOperationException("The patient already has a reservation for the same day.");
                    }
                    // Check doctor availability
                    var isValid =await _doctorScheduleRepository.IsValideTime(rdv.AppointmentDateTime);
                    if(!isValid){
                        throw new InvalidOperationException("chose a time between 9am and 4pm");
                    }
                    
                    var doctorSchedule = await _doctorScheduleRepository.GetDoctorScheduleByDoctorIdAndTimeAsync(rdv.DoctorId, rdv.AppointmentDateTime);
                    if (doctorSchedule != null)
                    {
                        throw new InvalidOperationException("Doctor is not available at the specified time.");
                    }

                    // Add the RDV to the RDVs DbSet
                    await _dataContext.RDVs.AddAsync(rdv);

                    // Add a DoctorSchedule entry
                    var newDoctorSchedule = new DoctorSchedule
                    {
                        DoctorId = rdv.DoctorId,
                        AvailableStartTime = rdv.AppointmentDateTime.TimeOfDay,
                        AvailableEndTime = rdv.AppointmentDateTime.AddHours(1).TimeOfDay, // Assuming RDVs last for 1 hour, adjust accordingly
                        Doctor = rdv.Doctor,
                        PatientId = rdv.PatientId,
                        Patient = rdv.Patient
                    };
                    await _doctorScheduleRepository.CreateDoctorScheduelAsync(newDoctorSchedule);

                    // Create a new medical service
                    var newServiceMedical = new MedicalService
                    {
                        Name = rdv.Reason,
                        Location = rdv.Doctor.Address,
                        Description = $"{rdv.Patient.FirstName} {rdv.Patient.LastName} meets Dr. {rdv.Doctor.FirstName} {rdv.Doctor.LastName} on {rdv.AppointmentDateTime:dddd, MMMM dd, yyyy}",
                    };
                    await _medicalServiceRepository.AddMedicalServiceAsync(newServiceMedical);

                    // Create a new MedicalServiceDoctor entry
                    var newMedicalServiceDoctor = new MedicalServiceDoctor
                    {
                        Description = newServiceMedical.Description,
                        MedicalServiceId = newServiceMedical.Id,
                        MedicalService = newServiceMedical,
                        DoctorId = rdv.DoctorId,
                        Doctor = rdv.Doctor
                    };
                    await _medicalServiceDoctorRepository.CreateAsync(newMedicalServiceDoctor);

                    await _dataContext.SaveChangesAsync();

                    await transaction.CommitAsync();

                    return rdv;
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }

        public async Task<RDV?> DeleteAsync(int id)
        {
            var Rdv =await _dataContext.RDVs.FirstOrDefaultAsync(x => x.Id ==id);
            if(Rdv==null){
                return null;
            }
            _dataContext.RDVs.Remove(Rdv);
            await _dataContext.SaveChangesAsync();
            return Rdv;
        }

        public async Task<List<RDV>> GetAllAsync()
        {
            return await _dataContext.RDVs.Include(r => r.Patient)
            .Include(r => r.Doctor).ToListAsync();
        }

        public async Task<RDV> GetByIdAsync(int id)
        {
            var Rdv = await _dataContext.RDVs.FindAsync(id);
            return Rdv!;
        }

        public async Task<List<RDV>> GetByIdPatientAsync(string id)
        {
            var RdvsPatienst = await _dataContext.RDVs.Where(x=>x.PatientId==id).Include(p=>p.Patient).Include(d=>d.Doctor).ToListAsync();
            return RdvsPatienst!;
        }
        public async Task<List<RDV>> GetByIdDoctorAsync(string id)
        {
            var RdvsDoctor = await _dataContext.RDVs.Where(x=>x.DoctorId==id).Include(p=>p.Patient).Include(d=>d.Doctor).ToListAsync();
            return RdvsDoctor!;
        }

        public async Task<RDV> GetRdvWithPatientAndDoctor(int id)
        {
            var rdv =  await _dataContext.RDVs
            .Include(r => r.Patient)
            .Include(r => r.Doctor)
            .FirstOrDefaultAsync(r => r.Id == id) ?? throw new ArgumentException("RDV not found");
            return rdv;
        }

        
    }
}