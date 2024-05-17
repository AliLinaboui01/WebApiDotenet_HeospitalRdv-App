using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.DoctorDto;
using api.Models;

namespace api.Mappers
{
    public static class DoctorMapper
    {
        public static SpecialityDoctorDto ToDoctorSpecialityDto(this Doctor DoctorModel){

            return new SpecialityDoctorDto
            {
                Speciality = DoctorModel.Speciality,
            };
        }
        public static DoctorsDto ToDoctorDto(this Doctor doctor){
            return new DoctorsDto{
                Id = doctor.Id,
                FirstName = doctor.FirstName,
                LastName = doctor.LastName,
                UserName = doctor.UserName,
                Email = doctor.Email,
                Telephone =doctor.Telephone,
                Address =doctor.Address,
                DateHiring = doctor.DateHiring,
                Speciality =doctor.Speciality,
                Image =doctor.Image
            };
        }
    }
}