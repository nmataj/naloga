using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Data;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly Context _context;

        public CitiesController(Context context)
        {
            _context = context;
        }

        // GET: api/Cities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<City>>> ListCities()
        {
            return await _context.Cities.OrderBy(c => c.Name).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<City>> CreateCity(string cityName, string countryName)
        {
            City newCity = new City();
            newCity.Name = cityName;

            var country = _context.Countries
                    .Where(c => c.Name == countryName)
                    .FirstOrDefault();

            newCity.CountryId = country.Id;

            if (_context.Cities.Any(c => c.Name == newCity.Name))
            {
                return BadRequest("City already exists");
            }
            _context.Cities.Add(newCity);

            await _context.SaveChangesAsync();

            return CreatedAtAction("CreateCity", newCity);
        }
    }
}
