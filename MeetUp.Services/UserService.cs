namespace MeetUp.Services
{
    using Data;
    using Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class UserService
    {
        public UserService()
        {
        }

        public bool Create(string email, string password, string fullname)
        {
            using (var db = new MeetUpDbContext())
            {
                var mailExists = db.Users.Any(u => u.Email == email);

                if(mailExists)
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
                    FullName = fullname
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
                    .Where(u => u.Email == email)
                    .FirstOrDefault();

                if (user == null)
                {
                    return false;
                }

                realHashedPassword = user.Password;
                salt = user.Salt;

                bool isLogin = HelperFunctions.CompareHashValue(password, email, realHashedPassword, salt);

                if(isLogin)
                {

                    return true;
                }

                return false;
            }
        }

        public List<User> All()
        {
            using (var db = new MeetUpDbContext())
            {
                return db.Users.ToList();
            }
        }

        public User GetUserById(int id)
        {
            using (var db = new MeetUpDbContext())
            {
                return db.Users
                    .Where(u => u.Id == id)
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
            int? banned = null)
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

                db.SaveChanges();
            }
        }

    }
}
