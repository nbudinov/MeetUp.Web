namespace MeetUp.Services
{
    using Data;
    using MeetUp.Data.Models;
    using System.Linq;

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
    }
}
