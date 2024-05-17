using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Interfaces;
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
            return Ok(DoctorScheduel);
       }
       [HttpGet]
       [Route("{id}")]
       public async Task<IActionResult> GetById(string id){
            var scheduels = await _doctorScheduelRepository.GetByIdAsync(id);
            return Ok(scheduels);
       }
    }
}