using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTOs.DoctorScheduelDto;
using api.DTOs.RDvDto;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class DoctorScheduelRepository : IDoctorScheduelRepository
    
    {
        private readonly DataContext _dataContext;
        public DoctorScheduelRepository(DataContext dataContext)
        {
            _dataContext=dataContext;
        }

        public Task<bool> CheckAvailibilityDoctor(string doctorId)
        {
            throw new NotImplementedException();
        }
        public async Task<bool> IsPatientAlreadyReserved(string patientId, DateTime appointmentDateTime)
        {
            // Assuming you have a method in your repository to retrieve all appointments for a patient
            var allAppointments = await _dataContext.RDVs.Where(x=>x.PatientId==patientId).ToListAsync();

            // Filter the appointments to find any appointment on the same day
            var appointmentsOnSameDay = allAppointments.Where(appointment =>
                appointment.PatientId == patientId && // Ensure appointment belongs to the patient
                appointment.AppointmentDateTime.Date == appointmentDateTime.Date);

            // Check if any appointment exists for the given patient on the same day
            return appointmentsOnSameDay.Any();
        }

        public async Task<DoctorSchedule> CreateDoctorScheduelAsync(DoctorSchedule doctorSchedule)
        {
            await _dataContext.DoctorScheduels.AddAsync(doctorSchedule);
            return doctorSchedule;
        }

        public async Task<DoctorSchedule> GetDoctorScheduleByDoctorIdAndTimeAsync(string DoctorId, DateTime AppoinemmentDateTime)
        {
            var DoctorSchedule =  await _dataContext.DoctorScheduels
                        .FirstOrDefaultAsync(ds => ds.DoctorId == DoctorId &&
                                    
                                    ds.AvailableStartTime <= AppoinemmentDateTime.TimeOfDay &&
                                    ds.AvailableEndTime >= AppoinemmentDateTime.TimeOfDay);
            return DoctorSchedule!;
        }

        public Task<bool> IsValideTime(DateTime dateTime)
        {
            bool isDifferentDay = dateTime.Date != DateTime.Today;
            bool isWithinTimeRange = dateTime.TimeOfDay >= TimeSpan.FromHours(9) && dateTime.TimeOfDay < TimeSpan.FromHours(16);
            
            bool isValid = isDifferentDay && isWithinTimeRange;
            return Task.FromResult(isValid);
        }

        public async Task<List<DoctorSchedule>> GetAllAsync()
        {
            return await _dataContext.DoctorScheduels.Include(d=>d.Doctor).Include(p=>p.Patient).ToListAsync();
        }

        public async Task<List<DoctorSchedule>> GetByIdAsync(string id)
        {
            var DoctorScheduels =await _dataContext.DoctorScheduels.Where(s => s.DoctorId==id).Include(d=>d.Doctor).Include(p=>p.Patient).ToListAsync();
            return DoctorScheduels;
        }
    }
}