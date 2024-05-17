using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTOs.PatientDto;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/patient")]
    [ApiController]

    public class PatientController :ControllerBase
    {
        private readonly DataContext _dataContext;
        private readonly IPatientRepository _patientRepository;
        public PatientController(DataContext dataContext, IPatientRepository patientRepository)
        {
            _dataContext = dataContext;
            _patientRepository= patientRepository; 
        }
        [HttpGet]
        public async Task<IActionResult> GetAll(){
            var Doctors = await _patientRepository.GetAllAsync();
            //var stockDto = stocks.Select(s => s.ToStockDto()).ToList();
            var patientDto = Doctors.Select(s=>s.ToPatientDto()).ToList();
            return Ok(patientDto);

        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetByID([FromRoute] string id){
            var Doctor =await _patientRepository.GetByIDAsync(id);
            if(Doctor == null)
                return NotFound();
            return Ok(Doctor);
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] string id){
            var Patient =await _patientRepository.DeletePatientAync(id);

            if(Patient == null)
                return NotFound();


            return NoContent();
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] string id, [FromBody] UpdatePatientDto patientDto){
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var doctorModel= await _patientRepository.UpdateDoctorAsync(id,patientDto);

            if(doctorModel == null)
                return NotFound();
            return Ok(doctorModel);
        }
    }
}