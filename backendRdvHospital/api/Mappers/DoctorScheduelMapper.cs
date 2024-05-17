using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using api.DTOs.DoctorScheduelDto;
using api.Models;

namespace api.Mappers
{
    public static class DoctorScheduelMapper
    {
        public static CreateDoctorScheduelDto ToCreateDoctorScheduelDto(this DoctorSchedule createDoctorScheduelDto){
            return new CreateDoctorScheduelDto{
                DoctorId = createDoctorScheduelDto.DoctorId,
                
                AvailableStartTime = createDoctorScheduelDto.AvailableStartTime,
                
            };
        }
        public static doctorScheduelDto ToDoctorScheduelDto(this DoctorSchedule doctorSchedule){
            return new doctorScheduelDto{
                DoctorId = doctorSchedule.DoctorId,
                AvailableStartTime = doctorSchedule.AvailableStartTime,
                AvailableEndTime = doctorSchedule.AvailableEndTime,
                PatientName = doctorSchedule.Patient!.FirstName + " "+ doctorSchedule.Patient!.LastName,
                DoctorName = doctorSchedule.Doctor!.FirstName + " "+ doctorSchedule.Doctor!.LastName
            };
        }
    }
}