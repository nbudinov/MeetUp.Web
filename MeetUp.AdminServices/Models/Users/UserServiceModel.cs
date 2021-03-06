﻿namespace MeetUp.AdminServices.Models.Users
{
    using MeetUp.Data.Models;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class UserServiceModel
    {
        public int Id { get; set; }

        public string Email { get; set; }

        [MinLength(6)]
        [MaxLength(512)]
        public string Password { get; set; }

        [MinLength(3)]
        [MaxLength(100)]
        public string FullName { get; set; }

        public DateTime? Birthday { get; set; }

        public UserSex Sex { get; set; }

        public UserRole Role { get; set; }

        public string Description { get; set; }

        public int CityId { get; set; }

        public City City { get; set; }

    }
}
