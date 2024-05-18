using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOs.AccountDto
{
    public class AdminDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Telephone { get; set; }
        public string? Address { get; set; }
        public string? Gender { get; set; }
        public string? Image { get; set; }  
    }
}