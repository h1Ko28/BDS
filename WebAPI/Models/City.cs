using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models.IEntity;

namespace WebAPI.Models
{
    public class City : IBaseEntity, IUpdateTracking
    {
        public int Id { get; set; }
        public City(string name, string country)
        {
            this.Name = name;
            this.Country = country;

        }
        [Column(Order = 1)]
        public string Name { get; set; }

        [Required]
        [Column(Order = 2)]
        public string Country { get; set; }
        public DateTime LastUpdatedOn { get; set; }
        public int LastUpdatedBy { get; set; }

    }
}