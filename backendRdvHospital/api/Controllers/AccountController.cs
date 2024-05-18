using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Threading.Tasks;
using System.Transactions;
using api.DTOs.AccountDto;

using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AcountController : ControllerBase
    {
        private readonly UserManager<User> _userManger;
        private readonly ITokenService _token;
        private readonly SignInManager<User> _signInManager;
        private readonly IImageService _imageService;
        
        public AcountController(UserManager<User> userManger, ITokenService token, SignInManager<User> signInManager, IImageService imageService,IWebHostEnvironment environment)
        {
            _userManger = userManger;
            _token = token;
            _signInManager = signInManager;
            _imageService = imageService;
            
        }

        
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto){
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManger.Users.FirstOrDefaultAsync(x => x.Email == loginDto.Email);
            if (user == null)
                return Unauthorized(new { statusCode = 401, message = "Invalid informations!" });

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password!, false);
            if (!result.Succeeded)
                return Unauthorized(new {StatusCode=401, message ="Email not found and/or password incorrect"});

            return Ok(new NewUserDto
            {
                UserName = user.UserName,
                Email = user.Email,
                Token = await _token.CreateToken(user),
                Role = await _token.CreateTokenWithRole(user)
            });
        }

        [HttpPost("register/doctor")]
        public async Task<IActionResult> RegisterDoctor([FromForm] RegisterDoctorDto rgisterDoctorDto){
            
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                string imageUploaded = "http://localhost:5299/Uploads/DefaultImage/userProfile.png";
                if(rgisterDoctorDto.Image!=null){
                    imageUploaded =  "http://localhost:5299/Uploads/Doctors/"+_imageService.UploadImage("Doctor",rgisterDoctorDto.Image);
                    
                }

                var doctorCreated = new Doctor
                {
                    UserName = rgisterDoctorDto.UserName,
                    FirstName = rgisterDoctorDto.FirstName!,
                    LastName = rgisterDoctorDto.LastName!,
                    Telephone = rgisterDoctorDto.Telephone!,
                    Address = rgisterDoctorDto.Address!,
                    Gender = rgisterDoctorDto.Gender!,
                    DateHiring = rgisterDoctorDto.DateHiring,
                    Speciality = rgisterDoctorDto.Speciality!,
                    Email = rgisterDoctorDto.Email,
                    Image = imageUploaded
                };

                var createUserResult = await _userManger.CreateAsync(doctorCreated, rgisterDoctorDto.Password!);

                if (createUserResult.Succeeded)
                {
                    var addRoleResult = await _userManger.AddToRoleAsync(doctorCreated, "DOCTOR");

                    if (addRoleResult.Succeeded)
                    {
                        return Ok(new NewUserDto
                        {
                            Id = doctorCreated.Id,
                            UserName = doctorCreated.UserName!,
                            Email = doctorCreated.Email!,
                            Token = await _token.CreateToken(doctorCreated),
                            Role = await _token.CreateTokenWithRole(doctorCreated)
                        });
                    }
                    else
                    {
                        return StatusCode(500, addRoleResult.Errors);
                    }
                }
                else
                {
                    return BadRequest(createUserResult.Errors);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }



        [HttpPost("register/patient")]
        public async Task<IActionResult> RegisterPatient([FromForm] RgisterPatientDto rgisterPatientDto){
            
            try{

                if(!ModelState.IsValid)
                    return BadRequest(ModelState);
                string imageUploaded ="http://localhost:5299/Uploads/DefaultImage/userProfile.png";
                if(rgisterPatientDto.Image!=null){
                    imageUploaded =  "http://localhost:5299/Uploads/Patients/"+_imageService.UploadImage("Patient",rgisterPatientDto.Image);
                }

                var createdPatient = new Patient
                {
                    UserName = rgisterPatientDto.UserName,
                    FirstName = rgisterPatientDto.FirstName!,
                    LastName = rgisterPatientDto.LastName!,
                    Telephone = rgisterPatientDto.Telephone!,
                    Address = rgisterPatientDto.Address!,
                    Gender = rgisterPatientDto.Gender!,
                    BirthDate = rgisterPatientDto.BirthDate,
                    Email = rgisterPatientDto.Email,
                    Image = imageUploaded
                };

                var result = await _userManger.CreateAsync(createdPatient, rgisterPatientDto.Password!);

                if(result.Succeeded){
                    var roleResult = await _userManger.AddToRoleAsync(createdPatient, "PATIENT");
                    if(roleResult.Succeeded)
                    {
                        
                        return Ok
                    (
                        new NewUserDto
                        {
                            UserName = createdPatient.UserName!,
                            Email = createdPatient.Email!,
                            Token = await _token.CreateToken(createdPatient),
                            Role = await _token.CreateTokenWithRole(createdPatient)

                        }
                    );
                    }else
                    {
                        return StatusCode(500, roleResult.Errors);
                    }
                    
                }else{
                    return BadRequest(result.Errors);
                }

            }catch(Exception Ex){
                
                return StatusCode(500, Ex.Message);
            }
            
        }
        [HttpPost("register/Admin")]
        public async Task<IActionResult> RegisterAdmin([FromForm] UserDto user){
            
            try{

                if(!ModelState.IsValid)
                    return BadRequest(ModelState);
                string imageUploaded ="http://localhost:5299/Uploads/DefaultImage/userProfile.png";
                if(user.Image!=null){
                    imageUploaded =  "http://localhost:5299/Uploads/Doctors/"+_imageService.UploadImage("Doctor",user.Image);
                }

                var createdPatient = new Patient
                {
                    UserName = user.UserName,
                    FirstName = user.FirstName!,
                    LastName = user.LastName!,
                    Telephone = user.Telephone!,
                    Address = user.Address!,
                    Gender = user.Gender!,
                    Email = user.Email,
                    Image = imageUploaded
                };

                var result = await _userManger.CreateAsync(createdPatient, user.Password!);

                if(result.Succeeded){
                    var roleResult = await _userManger.AddToRoleAsync(createdPatient, "Admin");
                    if(roleResult.Succeeded)
                    {
                        
                        return Ok
                    (
                        new NewUserDto
                        {
                            UserName = createdPatient.UserName!,
                            Email = createdPatient.Email!,
                            Token = await _token.CreateToken(createdPatient),
                            Role = await _token.CreateTokenWithRole(createdPatient)

                        }
                    );
                    }else
                    {
                        return StatusCode(500, roleResult.Errors);
                    }
                    
                }else{
                    return BadRequest(result.Errors);
                }

            }catch(Exception Ex){
                
                return StatusCode(500, Ex.Message);
            }
            
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetAdminById(string id){
            var Admin = await _userManger.FindByIdAsync(id);
            if(Admin==null){
                return NotFound();
            }
            return Ok(Admin.ToAdminDto());
        }
    }
        
}