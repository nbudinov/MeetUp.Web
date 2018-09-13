﻿using MeetUp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetUp.AdminServices.Models.Users
{
    public class UserViewModel
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string FullName { get; set; }

        public string Description { get; set; }

        public string City { get; set; }

        public DateTime? Birthday { get; set; }

        public UserSex Sex { get; set; }

        public int Banned { get; set; }

        public int Active { get; set; }

        public IEnumerable<UserImageModel> Images { get; set; } = new List<UserImageModel>();
    }
}
