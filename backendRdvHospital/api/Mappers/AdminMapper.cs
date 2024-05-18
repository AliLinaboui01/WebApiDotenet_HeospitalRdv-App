using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.AccountDto;
using api.Models;

namespace api.Mappers
{
    public static class AdminMapper
    {
        public static AdminDto ToAdminDto(this User user){
            return new AdminDto(){
                FirstName =user.FirstName,
                LastName =user.LastName,
                UserName =user.UserName,
                Email =user.Email,
                Telephone = user.Telephone, 
                Address =user.Address,
                Gender =user.Gender,
                Image =user.Image
            };
        }
    }
}