namespace MeetUp.AdminServices.Models.Users
{
    using MeetUp.Data.Models;
    using System.Collections.Generic;

    public class UserListingModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public UserSex Sex { get; set; }

        public UserRole Role { get; set; }

        public int Banned { get; set; }

        public int Active { get; set; }

        public int Deleted { get; set; } = 0;



        public IEnumerable<UserImageModel> Images { get; set; } = new List<UserImageModel>();
    }
}