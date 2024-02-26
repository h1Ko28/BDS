using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Dto
{
    public class CityDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is mandatory field")]
        [StringLength(15, MinimumLength = 2)]
        [RegularExpression(".*[a-zA-Z]+.*", ErrorMessage ="Numeric are not allow")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Country is mandatory field")]
        public string Country { get; set; }
    }
}