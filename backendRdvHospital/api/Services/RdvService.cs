using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.Models;

namespace api.Services
{
    public class RdvService :IRdvService
    {
        private readonly DataContext _dataContext;
        public RdvService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<bool> Checke(RDV rdv,string PatientId, string DoctorId)
        {
            // Ensure the patient exists
                    var patient = await _dataContext.Patients.FindAsync(PatientId) ;
                    if(patient==null){
                        return false;
                    }

                    // Ensure the doctor exists
                    var doctor = await _dataContext.Doctors.FindAsync(DoctorId) ;
                    if(doctor==null)
                    return false;

                    

                    // Set navigation properties
                    rdv.Patient = patient;
                    rdv.Doctor = doctor;
                    return true;
        }
    }
}