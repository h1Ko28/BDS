using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models.IEntity;

namespace WebAPI.Models
{
    public class PropertyType : IBaseEntity, IUpdateTracking
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public DateTime LastUpdatedOn { get; set; }
        public int LastUpdatedBy { get; set; }
    }
}