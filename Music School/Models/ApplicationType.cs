using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Music_School.Models
{
    public class ApplicationType
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        [DisplayName ("DisplayOrder")]
        public int DisplayOrder { get; set; }
        public int Password { get; set; }
        public string Email { get; set; }



    }
}
