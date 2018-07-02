using MeetUp.Services.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetUp.Services
{
    public interface IUserService
    {
        IEnumerable<UserListingModel> All(int page = 1, int pageSize = 10, int? withoutUserId = 0);

        UserViewModel GetUserById(int id);

        UserServiceModel GetUserByEmail(string email);

        void UpdateUser(int id,
            string fullname = null,
            string description = null,
            int? cityId = null,
            DateTime? birthday = null,
            string password = null,
            int? active = null,
            int? deleted = null,
            int? banned = null);


        int Count();

        bool SaveUserImage(int userId, string imagePath, int imageSize, string extension);

        bool Create(string email, string password, string fullname);

        bool Login(string email, string password);






    }
}
