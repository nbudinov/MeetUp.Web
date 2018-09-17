namespace MeetUp.AdminServices.Models.Cities
{
    using MeetUp.Data.Models;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CityServiceModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
