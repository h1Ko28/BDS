using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Data;
using WebAPI.Dtos;
using WebAPI.Interfaces;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class PropertyTypeController : BaseController
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;
        public PropertyTypeController(IUnitOfWork uow, IMapper mapper)
        {
            this.mapper = mapper;
            this.uow = uow;
        }   

        [HttpGet("list")]
        public async Task<IActionResult> GetAllPropertyType(){
            var propertiesType = await uow.PropertyTypeRepository.GetPropertyTypesAsync();
            var propertiesTypeDto = mapper.Map<IEnumerable<KeyPairDto>>(propertiesType);
            return Ok(propertiesTypeDto);
        }
    }
}