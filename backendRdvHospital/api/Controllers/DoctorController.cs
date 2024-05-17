using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTOs.DoctorDto;
using api.Interfaces;
using api.Mappers;
using api.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/Doctor")]
    [ApiController]
    
    public class DoctorController : ControllerBase
    {
        private readonly DataContext _dataContext;
        private readonly IDoctorRepository _doctorRepository;
        public DoctorController(DataContext dataContext, IDoctorRepository doctorRepository)
        {
            _dataContext = dataContext;
            _doctorRepository = doctorRepository;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllAsync(){

            var Doctors = await _doctorRepository.GetAllAsync();
            var DoctorDto = Doctors.Select(s=>s.ToDoctorDto()).ToList();
            return Ok(DoctorDto);
        }


        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById([FromRoute] string id){
            var Doctor = await _doctorRepository.GetByIdAsync(id);
            if(Doctor ==null){
                return NotFound();
            }
            return Ok(Doctor);
        }

        [HttpGet("specialities")]
        // [Consumes("JSON")]
        public async Task<IActionResult> GetSpecialities(){
            var Specialities = await _doctorRepository.GetAllAsync();
            var specialityDtos = Specialities.Select(s=>s.ToDoctorSpecialityDto());
            return Ok(specialityDtos);  
        }
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateDoctor([FromRoute] string id, [FromBody] UpdateDto updateDto){
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var doctorModel= await _doctorRepository.UpdateDoctorAsync(id,updateDto);

            if(doctorModel == null)
                return NotFound();
            return Ok(doctorModel);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id){
            var Doctor =await _doctorRepository.DeleteDoctorAync(id);
            
            if(Doctor == null)
                return NotFound();


            return NoContent();
        }
    }
}