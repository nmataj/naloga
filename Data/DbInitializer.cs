using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Models;

namespace TodoApi.Data
{
    public class DbInitializer
    {
        public static void Initialize(Context context)
        {
            context.Database.EnsureCreated();

            // Look for any countries.
            if (context.Countries.Any())
            {
                return;   // DB has been seeded
            }

            var countries = new Country[]
            {
            new Country{Name="Slovenija"},
            new Country{Name="Hrvaška"},
            new Country{Name="Avstrija"}
            };
            foreach (Country country in countries)
            {
                context.Countries.Add(country);
            }
            context.SaveChanges();

            var cities = new City[]
            {
            new City{CountryId=1,Name="Maribor"},
            new City{CountryId=2,Name="Zagreb"},
            new City{CountryId=3,Name="Salzburg"}
            };
            foreach (City city in cities)
            {
                context.Cities.Add(city);
            }
            context.SaveChanges();
        }
    }
}
