using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/doctorScheduel")]
    public class DoctorScheduelController : ControllerBase
    {
        private readonly IDoctorScheduelRepository _doctorScheduelRepository;
        public DoctorScheduelController(IDoctorScheduelRepository doctorScheduelRepository)
        {
            _doctorScheduelRepository = doctorScheduelRepository;
        }

       [HttpGet]
       public async Task<IActionResult> GetAll(){
            var DoctorScheduel = await _doctorScheduelRepository.GetAllAsync();
            var DoctorScheduelsDto = DoctorScheduel.Select(d=>d.ToDoctorScheduelDto()).ToList();
            return Ok(DoctorScheduelsDto);
       }
       [HttpGet]
       [Route("{id}")]
       public async Task<IActionResult> GetById(string id){
            var scheduels = await _doctorScheduelRepository.GetByIdAsync(id);
            var DoctorScheduelsDto = scheduels.Select(d=>d.ToDoctorScheduelDto());
            return Ok(DoctorScheduelsDto);
       }
    }
}