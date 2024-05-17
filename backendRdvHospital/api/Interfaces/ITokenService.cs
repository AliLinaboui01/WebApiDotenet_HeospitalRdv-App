using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.AccountDto;
using api.Models;
using api.Services;

namespace api.Interfaces
{
    public interface ITokenService
    {
        Task<string>  CreateToken(User user);
        Task<string> CreateTokenWithRole(User user);
    }
}