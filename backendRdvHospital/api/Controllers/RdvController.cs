using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using api.Data;
using api.DTOs.RDvDto;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/Rdv")]
    public class RdvController : ControllerBase
    {
        private readonly DataContext _dataContext;
        private readonly IRdvRepository _rdvRepository;
        public RdvController(DataContext dataContext, IRdvRepository rdvRepository)
        {
            _dataContext = dataContext;
            _rdvRepository = rdvRepository;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll(){
            var RDVs = await _rdvRepository.GetAllAsync();
            var RDvDto = RDVs.Select(s=>s.ToRdvDto()).ToList();
            return Ok(RDvDto);
        }


        [HttpPost]
        
        public async Task<IActionResult> CreateRdv([FromBody] CreateRdvDto createRdvDto){

            
            try{
                if(!ModelState.IsValid)
                    return BadRequest(ModelState);

                // Create a new RDV entity
                var newRdv =  new RDV{
                    AppointmentDateTime = createRdvDto.AppointmentDateTime,
                    Reason = createRdvDto.Reason,
                    PatientId = createRdvDto.PatientId,
                    DoctorId = createRdvDto.DoctorId,
                    
                };

                // Add the new RDV to the database
                await _rdvRepository.CreateRdvAsync(newRdv);
                
                // Retrieve the newly created RDV with related patient and doctor entities
                newRdv = await _rdvRepository.GetRdvWithPatientAndDoctor(newRdv.Id);
                

                return Ok(newRdv);
            }catch(Exception Ex){
                 // Rollback transaction
                return StatusCode(500, Ex.Message);
            }
            
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var Rdv = await _rdvRepository.DeleteAsync(id);
            Console.WriteLine(Rdv);
            if(Rdv==null){
                return NotFound();
            }
            return NoContent();
        }    

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var Rdv = await _rdvRepository.GetByIdAsync(id);
            if(Rdv==null){
                return NotFound();
            }
            return Ok(Rdv.ToRdvDto());
        }
        [HttpGet]
        [Route("patients/{idPatient}")]
        public async Task<IActionResult> GetByIdPatient(string idPatient)
        {
            var Rdvs = await _rdvRepository.GetByIdPatientAsync(idPatient);
            if(Rdvs==null){
                return NotFound();
            }
            return Ok(Rdvs);
        }
        [HttpGet]
        [Route("doctors/{idDoctor}")]
        public async Task<IActionResult> GetByIdDoctor(string idDoctor)
        {
            var Rdvs = await _rdvRepository.GetByIdDoctorAsync(idDoctor);
            if(Rdvs==null){
                return NotFound();
            }
            return Ok(Rdvs);
        }
    }
}