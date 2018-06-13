namespace MeetUp.Services.Models.Users
{
    public class UserListingModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Sex { get; set; }

        public int Banned { get; set; }
    }
}