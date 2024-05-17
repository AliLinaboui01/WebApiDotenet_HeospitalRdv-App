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
    }
}