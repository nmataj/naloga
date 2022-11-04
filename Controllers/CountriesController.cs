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
    public class CountriesController : ControllerBase
    {
        private readonly Context _context;

        public CountriesController(Context context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<Country>> CreateCountries([FromBody] Country[] countries)
        {
            List<Country> tmp = new List<Country>();
            foreach (Country country in countries)
            {
                if (_context.Countries.Any(x => x.Name == country.Name))
                {
                    continue;
                }
                _context.Countries.Add(country);
                tmp.Add(country);
            }
            await _context.SaveChangesAsync();

            return CreatedAtAction("CreateCountries", tmp);
        }
    }
}
