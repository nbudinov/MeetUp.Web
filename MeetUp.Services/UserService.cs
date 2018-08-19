namespace MeetUp.Services
{
    using Data;
    using Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Models.Users;

    public class UserService : IUserService
    {
        public MeetUpDbContext db;

        public UserService(MeetUpDbContext db)
        {
            this.db = db;
        }
        
        public IEnumerable<UserListingModel> All(int page = 1, int pageSize = 10, int? withoutUserId = 0)
        {
            return this.db
                .Users
                .Where(u => u.Id != withoutUserId)
                .OrderByDescending(u => u.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(u => new UserListingModel
                {
                    Id = u.Id,
                    Name = u.FullName,
                    Description = u.Description,
                    Banned = u.Banned,
                    Sex = u.Sex,
                    Images = u.Images.Select(i => new UserImageModel
                    {
                        Id = i.Id,
                        Path = i.Path,
                        Size = i.Size,
                        Extension = i.Extension
                    }),
                    ThisUsersLikes = u.ThisUserLikes.ToList(), //TODO: fix
                    UsersLikeThisUser = u.UsersLikeThisUser.ToList()
                })
                .ToList();
        }

        public IEnumerable<UserListingModel> WhoILike(int userId, int page = 1, int pageSize = 10)
        {
            var users = this.db
                .Users
                .Where(u => u.Id == userId)
                .FirstOrDefault()
                .ThisUserLikes
                .OrderByDescending(u => u.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(u => new UserListingModel
                {
                    Id = u.Id,
                    Name = u.FullName,
                    Description = u.Description,
                    Banned = u.Banned,
                    Sex = u.Sex,
                    Images = u.Images.Select(i => new UserImageModel
                    {
                        Id = i.Id,
                        Path = i.Path,
                        Size = i.Size,
                        Extension = i.Extension
                    }),
                    ThisUsersLikes = u.ThisUserLikes.ToList(), //TODO: fix
                    UsersLikeThisUser = u.UsersLikeThisUser.ToList()
                })
                .ToList();

            return users;
        }

        public IEnumerable<UserListingModel> WhoLikesMe(int userId, int page = 1, int pageSize = 10)
        {
            throw new NotImplementedException();
        }

        public UserViewModel GetUserById(int id)
        {
            //using (var db = new MeetUpDbContext())
            //{
                return db.Users
                    .Where(u => u.Id == id)
                    .Select(u => new UserViewModel
                    {
                        Id = u.Id,
                        FullName = u.FullName,
                        Email = u.Email,
                        City = u.City.Name,
                        Birthday = u.Birthday,
                        Description = u.Description,
                        Sex = u.Sex,
                        Active = u.Active,
                        Banned = u.Banned,
                        Images = u.Images.Select(i => new UserImageModel
                        {
                            Id = i.Id,
                            Path = i.Path,
                            Size = i.Size,
                            Extension = i.Extension
                        })
                    })
                    .FirstOrDefault();
            //}
        }

        public UserServiceModel GetUserByEmail(string email)
        {
            return db.Users
                .Where(u => u.Email == email && u.Active == 1 && u.Deleted == 0)
                .Select(u => new UserServiceModel
                {
                    Id = u.Id,
                    Email = u.Email,
                    FullName = u.FullName
                })
                .FirstOrDefault();
        }

        public void UpdateUser(int id, 
            string fullname = null, 
            string description = null, 
            int? cityId = null, 
            DateTime? birthday = null, 
            string password = null,
            int? active = null,
            int? deleted = null, 
            int? banned = null)
        {
            //using (var db = new MeetUpDbContext())
            //{
                var dbUser = db.Users
                    .Where(u => u.Id == id)
                    .FirstOrDefault();

                if (password != null)
                {
                    var salt = dbUser.Salt;
                    var hashedPass = HelperFunctions.Get_HASH_SHA512(password, dbUser.Email, salt);

                    dbUser.Password = hashedPass;
                }
                
                dbUser.FullName = fullname ?? dbUser.FullName;
                dbUser.Description = description ?? dbUser.Description;
                dbUser.CityId = cityId ?? dbUser.CityId;
                dbUser.Birthday = birthday ?? dbUser.Birthday;
                dbUser.Active = active ?? dbUser.Active;
                dbUser.Deleted = deleted ?? dbUser.Deleted;
                dbUser.Banned = banned ?? dbUser.Banned;

                db.SaveChanges();
            //}
        }

        public int Count()
        {
            return db.Users.Count();
        }

        public int WhoILikeTotal(int userId)
        {
            return db.Users
                .Where(u => u.Id == userId)
                .FirstOrDefault()
                .ThisUserLikes
                .Count();
        }

        public int WhoLikesMeTotal(int userId)
        {
            return db.Users
                .Where(u => u.Id == userId)
                .FirstOrDefault()
                .UsersLikeThisUser
                .Count();
        }

        public bool SaveUserImage(int userId, string imagePath, int imageSize, string extension)
        {
            using (var db = new MeetUpDbContext())
            {
                var user = db.Users
                    .Where(u => u.Id == userId)
                    .FirstOrDefault();

                if(user == null)
                {
                    return false;
                }

                var image = new Image
                {
                    Path = imagePath,
                    UserId = userId,
                    Size = imageSize, 
                    Extension = extension
                };

                user.Images.Add(image);
                db.SaveChanges();

                return true;
            }
        }

        public bool LikeUser(int userLikingId, int userLikedId)
        {
            var likingUser = this.db
                .Users
                .Where(u => u.Id == userLikingId)             
                .FirstOrDefault();

            var likedUser = this.db
                .Users
                .Where(u => u.Id == userLikedId)
                .FirstOrDefault();

            if (likingUser == null || likedUser == null)
            {
                return false;
            }

            var toLike = !likingUser.ThisUserLikes.Any(u => u.Id == likedUser.Id);

            if (toLike)
            {
                likingUser.ThisUserLikes.Add(likedUser);
                likedUser.UsersLikeThisUser.Add(likingUser);
            }
            else
            {
                likingUser.ThisUserLikes.Remove(likedUser);
                likedUser.UsersLikeThisUser.Remove(likingUser);
            }

            db.SaveChanges();

            return true;
        }

        public bool Create(string email, string password, string fullname)
        {
            //using (var db = new MeetUpDbContext())
            //{
                var mailExists = db.Users.Any(u => u.Email == email && u.Deleted == 0);

                if (mailExists)
                {
                    return false;
                }

                var salt = HelperFunctions.GetSalt();
                var hashedPass = HelperFunctions.Get_HASH_SHA512(password, email, salt);

                var user = new User
                {
                    Email = email,
                    Password = hashedPass,
                    Salt = salt,
                    FullName = fullname
                };

                db.Users.Add(user);
                db.SaveChanges();

                return true;
            //}
        }

        public bool Login(string email, string password)
        {
            string realHashedPassword = string.Empty;
            byte[] salt = new byte[HelperFunctions.saltLengthLimit];
          
            var user = db.Users
                .Where(u => u.Email == email && u.Active == 1 && u.Deleted == 0)
                .FirstOrDefault();

            if (user == null)
            {
                return false;
            }

            realHashedPassword = user.Password;
            salt = user.Salt;

            bool isLogin = HelperFunctions.CompareHashValue(password, email, realHashedPassword, salt);

            if (isLogin)
            {
                return true;
            }

            return false;
        }

    }
}
