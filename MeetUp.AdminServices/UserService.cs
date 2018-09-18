namespace MeetUp.AdminServices
{
    using System;
    using Data;
    using Data.Models;
    using System.Collections.Generic;
    using System.Linq;
    using Models.Users;
    using System.Windows.Media.Imaging;

    public class UserService
    {
        public IEnumerable<Image> getAllPhotos(int userId)
           {

            //var result = (List<Image>)null;
            using (var db = new MeetUpDbContext())
            {
                return (from image in db.Images where image.UserId == userId && image.Deleted == false select image).ToList();
            }

            //return result;
           }

        public IEnumerable<UserListingModel> All(int page = 1, int pageSize = 10, int? withoutUserId = 0)
        {
            using (var db = new MeetUpDbContext())
            {
                return db
                    .Users
                    .Where(u => (u.Id != withoutUserId && u.Deleted == 0))
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
                        Role = u.Role,
                        Active = u.Active,
                        Images = u.Images.Select(i => new UserImageModel
                        {
                            Id = i.Id,
                            Path = i.Path,
                            Size = i.Size,
                            Extension = i.Extension
                        })
                    })
                    .ToList();
            }
        }

 
        public UserViewModel GetUserById(int id)
        {
            using (var db = new MeetUpDbContext())
            {
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
                        Role = u.Role,
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
            }
        }

        public void DeleteImage(int id)
        {

            using (var db = new MeetUpDbContext())
            {
                var image = db.Images
                    .Where(u => u.Id == id)
                    .FirstOrDefault();

                image.Deleted = true;
                db.SaveChanges();
            }

        }

        public UserServiceModel GetUserByEmail(string email)
        {
            using (var db = new MeetUpDbContext())
            {
                return db.Users
                    .Where(u => u.Email == email)
                    .Select(u => new UserServiceModel
                    {
                        Id = u.Id,
                        Email = u.Email,
                        FullName = u.FullName
                    })
                    .FirstOrDefault();
            }
        }

        public void UpdateUser(int id,
            string fullname = null,
            string description = null,
            int? cityId = null,
            DateTime? birthday = null,
            string password = null,
            int? active = null,
            int? deleted = null,
            int? banned = null,
            UserRole? role = null,
            UserSex? sex = null)
        {
            using (var db = new MeetUpDbContext())
            {
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
                dbUser.Sex = sex ?? dbUser.Sex;
                dbUser.Role = role ?? dbUser.Role;

                db.SaveChanges();
            }
        }

        public int Count()
        {
            using (var db = new MeetUpDbContext())
            {
                return db.Users.Count();
            }
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

        public bool Create(string email, string password, string fullname, string description, DateTime? birthday = null, UserRole role = UserRole.User, UserSex sex = UserSex.Other)
        {
            using (var db = new MeetUpDbContext())
            {
                var mailExists = db.Users.Any(u => u.Email == email);

                if (mailExists)
                {
                    return false;
                }

                var salt = HelperFunctions.Get_SALT();
                var hashedPass = HelperFunctions.Get_HASH_SHA512(password, email, salt);

                var user = new User
                {
                    Email = email,
                    Password = hashedPass,
                    Salt = salt,
                    FullName = fullname,
                    Description = description,
                    Birthday = birthday,
                    Sex = sex,
                    Role = role,
                    CreateTime = DateTime.Today,
                    LastOnline = DateTime.Today
                };

                db.Users.Add(user);
                db.SaveChanges();

                return true;
            }
        }

        public bool Login(string email, string password)
        {
            string realHashedPassword = string.Empty;
            byte[] salt = new byte[HelperFunctions.saltLengthLimit];

            using (var db = new MeetUpDbContext())
            {
                var user = db.Users
                    .Where(u => u.Email == email && u.Deleted == 0 && u.Banned == 0 && u.Role == UserRole.Admin)
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
}
