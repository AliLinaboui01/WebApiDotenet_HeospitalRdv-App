using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.DoctorScheduelDto;
using api.Models;

namespace api.Interfaces
{
    public interface IDoctorScheduelRepository
    {
        Task<DoctorSchedule> GetDoctorScheduleByDoctorIdAndTimeAsync(string DoctorId , DateTime AppoinemmentDateTime);
        Task<DoctorSchedule> CreateDoctorScheduelAsync(DoctorSchedule doctorSchedule);
        Task<Boolean> CheckAvailibilityDoctor(string doctorId);
        Task<Boolean> IsValideTime(DateTime dateTime);
        Task<bool> IsPatientAlreadyReserved(string patientId, DateTime appointmentDateTime);
        Task<List<DoctorSchedule>> GetAllAsync();
        Task<List<DoctorSchedule>> GetByIdAsync(string id);
        
    }
}