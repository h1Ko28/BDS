using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Interfaces;

namespace WebAPI.Controllers
{
    public class PropertyController : BaseController
    {
        private readonly IUnitOfWork uow;

        public PropertyController(IUnitOfWork uow)
        {
            this.uow = uow;
        }
        
        [HttpGet("type/{sellRent}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetProperties(int sellRent) {
            var properties = await uow.PropertyRepository.GetPropertiesAsync(sellRent);
            return Ok(properties);
        }  
    }
}