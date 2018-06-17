namespace MeetUp.Services.Models.Users
{
    using MeetUp.Data.Models;
    using System.Collections.Generic;

    public class UserListingModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Sex { get; set; }

        public int Banned { get; set; }

        public IEnumerable<UserImageModel> Images { get; set; } = new List<UserImageModel>();
    }
}