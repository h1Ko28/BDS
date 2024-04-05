using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models.IEntity
{
    public interface IBaseEntity
    {
        public int Id { get; set; }
    }
}