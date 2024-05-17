using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.RDvDto;
using api.Models;

namespace api.Mappers
{
    public static class RDVMapper
    {
        public static CreateRdvDto ToCreateRdvDto(this RDV rDVModel){
            return new CreateRdvDto{
                AppointmentDateTime = rDVModel.AppointmentDateTime,
                Reason = rDVModel.Reason,
                PatientId = rDVModel.PatientId,
                DoctorId = rDVModel.DoctorId,
                
            };
        }
        public static RdvDto ToRdvDto(this RDV rDV){
            return new RdvDto{
                AppointmentDateTime = rDV.AppointmentDateTime,
                Reason = rDV.Reason,
                PatientId = rDV.PatientId,
                DoctorId = rDV.DoctorId,
                Doctor = rDV.Doctor,
                Patient = rDV.Patient,
            };
        }
        public static Rdv ToRdv(this RDV rdv){
            return new Rdv{
                AppointmentDateTime = rdv.AppointmentDateTime,
                Reason = rdv.Reason,
                PatientId = rdv.PatientId,
                DoctorId = rdv.DoctorId,
            };
        }
    }
}