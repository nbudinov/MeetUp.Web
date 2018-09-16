using MeetUp.Data.Models;
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
        IEnumerable<UserListingModel> All(int page = 1, int pageSize = 10, int? withoutUserId = 0, int ageFrom = 10, int ageTo = 120, int sex = 0);

        IEnumerable<UserListingModel> WhoILike(int userId, int page = 1, int pageSize = 10);

        IEnumerable<UserListingModel> WhoLikesMe(int userId, int page = 1, int pageSize = 10);

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
            int? banned = null,
            DateTime? lastOnline = null,
            UserSex? sex = null);


        int Count();

        int WhoILikeTotal(int userId);

        int WhoLikesMeTotal(int userId);

        bool SaveUserImage(int userId, string imagePath, int imageSize, string extension);

        Image GetUserImageById(int userId, int imageId);

        bool DeleteUserImage(int userId, int imageId);

        bool LikeUser(int userLiking, int userLiked);    

        bool Create(string email, string password, string fullname);

        bool Login(string email, string password);






    }
}
