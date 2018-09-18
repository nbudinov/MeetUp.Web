namespace MeetUp.AdminServices
{
    using Data;
    using MeetUp.Data.Models;
    using System.Linq;
    using Models.Cities;
    using System.Collections.Generic;

    public class CityService
    {
        public void Populate()
        {
            using (var db = new MeetUpDbContext())
            {
                if(!db.Cities.Any())
                {
                    var city = new City
                    {
                        Name = "Sofia"
                    };

                    db.Cities.Add(city);
                    db.SaveChanges();
                }

            }
        }

        public IEnumerable<CityListingModel> All(int page = 1, int pageSize = 10)
        {
            using (var db = new MeetUpDbContext())
            {
                return db
                .Cities
                .OrderByDescending(u => u.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Where(u => u.Deleted == 0)
                .Select(u => new CityListingModel
                {
                    Id = u.Id,
                    Name = u.Name,
                })
                .ToList();
            }
        }



        public void UpdateCity(int id,
            string name = null,
            int? deleted = null)
        {
            using (var db = new MeetUpDbContext())
            {
                var dbCity = db.Cities
                    .Where(u => u.Id == id)
                    .FirstOrDefault();

                dbCity.Name = name ?? dbCity.Name;
                dbCity.Deleted = deleted ?? dbCity.Deleted;
                                
                db.SaveChanges();
            }
        }



        public CityViewModel GetCityById(int id)
        {
            using (var db = new MeetUpDbContext())
            {
                return db.Cities
                    .Where(u => u.Id == id)
                    .Select(u => new CityViewModel
                    {
                        Id = u.Id,
                        Name = u.Name,
                    })
                    .FirstOrDefault();
            }
        }



        public bool Create(string name)
        {
            using (var db = new MeetUpDbContext())
            {
                var cityExists = db.Cities.Any(u => u.Name == name);

                if (cityExists)
                {
                    return false;
                }

                var city = new City
                {
                    Name = name,
                };

                db.Cities.Add(city);
                db.SaveChanges();

                return true;
            }
        }



    }
}
