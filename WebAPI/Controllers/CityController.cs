using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
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

        public CityController(IUnitOfWork uow)
        {
            this.uow = uow;

        }

        [HttpGet]
        public async Task<IActionResult> GetCities()
        {
            var city = await uow.CityRepository.GetCities();
            return Ok(city);
        }

        [HttpPost("post")]
        // public async Task<IActionResult> AddCities(string cityName)
        public async Task<IActionResult> AddCities(City city)
        {
            // City city = new City();
            // city.Name = cityName;
            uow.CityRepository.AddCity(city);
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