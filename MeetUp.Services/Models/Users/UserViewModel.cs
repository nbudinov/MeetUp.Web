namespace MeetUp.Services.Models.Users
{
    using System;
    using System.Collections.Generic;
    using MeetUp.Data.Models;

    public class UserViewModel
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string FullName { get; set; }

        public string Description { get; set; }

        public string City { get; set; }

        public DateTime? Birthday { get; set; }

        public DateTime? CreateTime { get; set; }

        public DateTime? LastOnline { get; set; }

        public UserSex Sex { get; set; }

        public int Banned { get; set; }

        public int Active { get; set; }

        public IEnumerable<UserImageModel> Images { get; set; } = new List<UserImageModel>();

        public int PeopleYouLikedCount { get; set; }

        public int PeopleLikedYouCount { get; set; }
    }
}
