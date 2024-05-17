using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/medicalService")]
    public class MedicalServiceController: ControllerBase
    {
        private readonly DataContext _dataContext;
        private readonly IMedicalServiceRepository _medicalServiceRepository;
        public MedicalServiceController(DataContext dataContext ,IMedicalServiceRepository medicalServiceRepository)
        {
            _dataContext = dataContext;
            _medicalServiceRepository = medicalServiceRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(){
            var MedicalServices =await _medicalServiceRepository.GettAllAsync();
            var MedicalServicesDto = MedicalServices.Select(s=>s.ToMedicalServiceDto());
            return Ok(MedicalServicesDto);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id){
            var medicalService =  await _medicalServiceRepository.DeleteAsync(id);
            if(medicalService ==null)
                return NotFound();
            return NoContent();
        }
    }
}