using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetUp.Services.Models.Users
{
    public class UserViewModel
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string FullName { get; set; }

        public string Description { get; set; }

        public string City { get; set; }

        public DateTime? Birthday { get; set; }

        public int Sex { get; set; }

        public int Banned { get; set; }

        public IEnumerable<UserImageModel> Images { get; set; } = new List<UserImageModel>();
    }
}
