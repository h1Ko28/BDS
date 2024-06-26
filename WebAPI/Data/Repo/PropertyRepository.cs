using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAPI.Interfaces;
using WebAPI.Models;
namespace WebAPI.Data.Repo
{
    public class PropertyRepository : IPropertyRepository
    {
        private readonly DataContext dc;

        public PropertyRepository(DataContext dc)
        {
            this.dc = dc;
        }

        public void AddProperty(Property property)
        {
            dc.Properties.Add(property);
        }


        public void DeleteProperty(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Property>> GetPropertiesAsync(int sellRent)
        {
            var properties = await dc.Properties
            .Include(p => p.PropertyType)
            .Include(p => p.FurnishingType)     
            .Include(p => p.City)
            .Where(p => p.SellRent == sellRent)
            .ToListAsync();
            return properties;
        }

        public async Task<Property> GetPropertyDetailAsync(int id)
        {
            var property = await dc.Properties
            .Include(p => p.PropertyType)
            .Include(p => p.FurnishingType)
            .Include(p => p.Photos)
            .Include(p => p.City)
            .Where(p => p.Id == id)
            .FirstAsync();
            return property;
        }
    }
}