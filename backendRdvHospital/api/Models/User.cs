using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace api.Models
{
    [Table("users")]
    public class User : IdentityUser
    {
        public string FirstName { get; set; } =string.Empty;
        public string LastName { get; set; } =string.Empty;
        public string Telephone { get; set; } =string.Empty;
        public string Address { get; set; } =string.Empty;
        public string Gender { get; set; } =string.Empty;
        public string Image { get; set; } = string.Empty;
        
    }
}