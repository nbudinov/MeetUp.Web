namespace MeetUp.Services
{
    using System;
    using System.Security.Cryptography;
    using System.Text;

    public static class HelperFunctions
    {
        public static readonly int saltLengthLimit = 16;
        //r43UUohcv338tuX9CHnbyVg0um4PqnilHKQkDf4TKsqGkFXAOyt37PiMIeJuBG1Rd7hpPKx8stqhaZQcVzWz0UpyHfSyhD9mUiSYsozxWvc=
        public static bool CompareHashValue(string password, string username, string OldHASHValue, byte[] SALT)
        {
            string expectedHashString = Get_HASH_SHA512(password, username, SALT);

            return (OldHASHValue == expectedHashString);
        }

        public static string Get_HASH_SHA512(string password, string username, byte[] salt)
        {
            try
            {
                //required NameSpace: using System.Text;
                //Plain Text in Byte
                byte[] plainTextBytes = Encoding.UTF8.GetBytes(password + username);

                //Plain Text + SALT Key in Byte
                byte[] plainTextWithSaltBytes = new byte[plainTextBytes.Length + salt.Length];

                for (int i = 0; i < plainTextBytes.Length; i++)
                {
                    plainTextWithSaltBytes[i] = plainTextBytes[i];
                }

                for (int i = 0; i < salt.Length; i++)
                {
                    plainTextWithSaltBytes[plainTextBytes.Length + i] = salt[i];
                }

                HashAlgorithm hash = new SHA512Managed();
                byte[] hashBytes = hash.ComputeHash(plainTextWithSaltBytes);
                byte[] hashWithSaltBytes = new byte[hashBytes.Length + salt.Length];

                for (int i = 0; i < hashBytes.Length; i++)
                {
                    hashWithSaltBytes[i] = hashBytes[i];
                }

                for (int i = 0; i < salt.Length; i++)
                {
                    hashWithSaltBytes[hashBytes.Length + i] = salt[i];
                }

                return Convert.ToBase64String(hashWithSaltBytes);
            }
            catch
            {
                return string.Empty;
            }
        }


        public static byte[] GetSalt()
        {
            return GetSalt(saltLengthLimit);
        }

        public static byte[] GetSalt(int maximumSaltLength)
        {
            var salt = new byte[maximumSaltLength];

            //Require NameSpace: using System.Security.Cryptography;
            using (var random = new RNGCryptoServiceProvider())
            {
                random.GetNonZeroBytes(salt);
            }

            return salt;
        }


        //old
        public static string HashPassword(string password, string salt)
        {
            using (var sha1 = new SHA1Managed())
            {
                return BitConverter.ToString(sha1.ComputeHash(Encoding.UTF8.GetBytes(password + salt)));
            }
        }
    }
}
