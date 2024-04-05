using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using WebAPI.Dto;
using WebAPI.Interfaces;
using WebAPI.Models;
//using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Authorize]
    public class CityController : BaseController 
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper  mapper;

        public CityController(IUnitOfWork uow, IMapper  mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetCities()
        {
            var cities = await uow.CityRepository.GetCities();

            var citiesDto = mapper.Map<IEnumerable<CityDto>>(cities);
            // var citiDto = from c in cities select
            // new CityDto{
            //     Id = c.Id,
            //     Name = c.Name,
            // };
            return Ok(citiesDto);  
        }

        [HttpPost("post")]
        // public async Task<IActionResult> AddCities(string cityName)
        public async Task<IActionResult> AddCities(CityDto cityDto)
        {
            var city = mapper.Map<City>(cityDto);
            city.LastUpdatedBy = 1;
            city.LastUpdatedOn = DateTime.Now;

            uow.CityRepository.AddCity(city);
            await uow.SaveAsync();
            return StatusCode(201);
        }

        [HttpPut("updated/{id}")]
        public async Task<IActionResult> UpdateCity(int id, CityDto cityDto)
        {
            if (id != cityDto.Id)
                return BadRequest("Updated are not allowed");

            var cityFromDB = await uow.CityRepository.FindCity(id);
            
            if (cityFromDB == null)
                return BadRequest("Update not allowed");
                
            mapper.Map(cityDto,cityFromDB);
            await uow.SaveAsync();
            return StatusCode(201);
        }

        [HttpDelete("del/{id}")]
        public async Task<IActionResult> DeleteCities(int id)
        {
            uow.CityRepository.DeleteCity(id);
            await uow.SaveAsync();
            return Ok(id);
        }
    }
}