namespace MeetUp.Services.Models.Users
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using MeetUp.Data.Models;

    public class UserViewModel
    {
        public int Id { get; set; }

        public string Email { get; set; }

        [MinLength(3, ErrorMessage = "Full name min length is 3 chars")]
        [MaxLength(70, ErrorMessage = "Full name max length is 70 chars")]
        public string FullName { get; set; }

        [MinLength(10, ErrorMessage = "Description min length is 10 chars")]
        [MaxLength(255, ErrorMessage = "Description min length is 255 chars")]
        public string Description { get; set; }

        public DateTime? Birthday { get; set; }

        public DateTime? CreateTime { get; set; }

        public DateTime? LastOnline { get; set; }

        public string Location { get; set; }

        public UserSex Sex { get; set; }

        public int Banned { get; set; }

        public int Active { get; set; }

        public IEnumerable<UserImageModel> Images { get; set; } = new List<UserImageModel>();

        public string City { get; set; }

        public int PeopleYouLikedCount { get; set; }

        public int PeopleLikedYouCount { get; set; }

        public IEnumerable<User> ThisUsersLikes { get; set; } = new List<User>();

        public IEnumerable<User> UsersLikeThisUser { get; set; } = new List<User>();
    }
}
