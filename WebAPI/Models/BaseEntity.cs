using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class BaseEntity
    {
        [Column(Order = 0)]
        public int Id { get; set; }

    }
}