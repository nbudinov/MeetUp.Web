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

        public UserRole Role { get; set; }

        [MinLength(3)]
        [MaxLength(100)]
        public string FullName { get; set; }

        public DateTime? Birthday { get; set; }

        public DateTime? CreateTime { get; set; }

        public DateTime? LastOnline { get; set; }

        public string Location { get; set; }

        public UserSex Sex { get; set; }

        public string Description { get; set; }

        public int Active { get; set; } = 1;

        public int Deleted { get; set; } = 0;

        public int Banned { get; set; } = 0;

        public int? CityId { get; set; }

        public virtual City City { get; set; }

        public virtual ICollection<Post> Posts { get; set; } = new HashSet<Post>();

        public virtual ICollection<User> ThisUserLikes { get; set; } = new HashSet<User>();

        public virtual ICollection<User> UsersLikeThisUser { get; set; } = new HashSet<User>();

        public virtual ICollection<User> ThisUserSuperLikes { get; set; } = new HashSet<User>();

        public virtual ICollection<User> UsersSuperLikeThisUser { get; set; } = new HashSet<User>();

        public virtual ICollection<Image> Images { get; set; } = new HashSet<Image>();
    }
}
