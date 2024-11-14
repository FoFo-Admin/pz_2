using Microsoft.EntityFrameworkCore;
using pz_2.Context;
using pz_2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pz_2.Data
{
    internal static class CityData
    {

        public static List<City> ReadAll()
        {
            using (BusinessContext db = new BusinessContext())
            {
                var cities = (from c in db.Cities select c).ToList();
                return cities;
            }
            
        }

        public static void Create(string cityName)
        {
            using (BusinessContext db = new BusinessContext())
            {

                City city = new City { Name = cityName };
                db.Cities.Add(city);
                db.SaveChanges();
            }
        }

        public static void Update(int oldCityId, string cityName)
        {
            using (BusinessContext db = new BusinessContext())
            {
                City city = db.Cities.FirstOrDefault(d => d.Id == oldCityId);
                if (city != null)
                {
                    city.Name = cityName;
                    db.SaveChanges();
                }
            }
        }

        public static void Delete(int cityId)
        {
            using (BusinessContext db = new BusinessContext())
            {
                City cityDel = (from b in db.Cities
                                        where b.Id == cityId
                                        select b)
                                           .Include(b => b.Businesses)
                                                .ThenInclude(a => a.Contacts)
                                            .Include(b => b.Businesses)
                                                .ThenInclude(p => p.Products)
                                           .ToList()[0];

                if (cityDel != null)
                {
                    foreach (Business business in cityDel.Businesses)
                    {
                        BusinessData.DeleteImage("images", business.Id);
                        BusinessData.DeleteImage("logos", business.Id);

                        foreach (Product product in business.Products)
                            BusinessData.DeleteImage("product-images", product.Id);
                    }

                    db.Cities.Remove(cityDel);
                    db.SaveChanges();
                }
            }
        }
    }
}
