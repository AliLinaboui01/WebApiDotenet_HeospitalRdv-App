using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    [Table("patients")]
    public class Patient :User
    {
        public DateTime BirthDate { get; set; }
        public List<RDV> RDVs { get; set; } = new List<RDV>();
    }
}