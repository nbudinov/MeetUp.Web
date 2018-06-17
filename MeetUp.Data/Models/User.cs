namespace MeetUp.Data.Models
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class User
    {
        public int Id { get; set; }

        [MinLength(2)]
        [MaxLength(50)]
        [EmailAddress]
        public string Email { get; set; }

        [MinLength(6)]
        [MaxLength(512)]
        public string Password { get; set; }

        [MinLength(10)]
        public byte[] Salt { get; set; }

        [MinLength(3)]
        [MaxLength(100)]
        public string FullName { get; set; }

        public DateTime? Birthday { get; set; }

        public int Sex { get; set; }

        public string Description { get; set; }

        public int Active { get; set; } = 1;

        public int Deleted { get; set; } = 0;

        public int Banned { get; set; } = 0;

        public int CityId { get; set; } = 1;

        public City City { get; set; }

        public List<Post> Posts { get; set; } = new List<Post>();

        public List<User> ThisUsersLikes { get; set; } = new List<User>();

        public List<User> UsersLikeThisUser { get; set; } = new List<User>();

        public List<Image> Images { get; set; } = new List<Image>();
    }
}
