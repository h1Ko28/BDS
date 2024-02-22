using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Interfaces
{
    public interface ICityRepository
    {
        Task<IEnumerable<City>> GetCities();
        void AddCity(City city);
        void DeleteCity(int id);
        Task<City> FindCity(int id);
    }
}