using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using WebAPI.Dto;
using WebAPI.Interfaces;
using WebAPI.Models;
//using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper  mapper;

        public CityController(IUnitOfWork uow, IMapper  mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }

        [HttpGet]
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
            // var city = new City{
            //     Name = cityDto.Name,
            //     LastUpdatedBy = 10,
            //     LastUpdatedOn = DateTime.Now
            // };
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
            var cityFromDB = await uow.CityRepository.FindCity(id);
            cityFromDB.LastUpdatedBy = 1;
            cityFromDB.LastUpdatedOn = DateTime.Now;
            mapper.Map(cityDto,cityFromDB);
            await uow.SaveAsync();
            return StatusCode(201);
        }

        [HttpDelete("del/{id}")]
        public async Task<IActionResult> DeleteCities(int id)
        {
            uow.CityRepository.DeleteCity(id);
            // City city = new City();
            // city.Name = cityName;
            await uow.SaveAsync();
            return Ok(id);
        }
    }
}