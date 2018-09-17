namespace MeetUp.Services.Models.Users
{
    using MeetUp.Data.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class UserListingModel
    {
        public int Id { get; set; }

        [MinLength(3)]
        [MaxLength(70)]
        public string Name { get; set; }

        [MinLength(10)]
        [MaxLength(1000)]
        public string Description { get; set; }

        public DateTime? Birthday { get; set; }

        public UserSex Sex { get; set; }

        public int Banned { get; set; }

        public IEnumerable<UserImageModel> Images { get; set; } = new List<UserImageModel>();

        public IEnumerable<User> ThisUsersLikes { get; set; } = new List<User>();

        public IEnumerable<User> UsersLikeThisUser { get; set; } = new List<User>();
    }
}