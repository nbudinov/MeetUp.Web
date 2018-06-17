using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetUp.Services.Models.Users
{
    public class UserImageModel
    {
        public int Id { get; set; }

        public string Path { get; set; }

        public string Name {
            get
            {
                var nameWithExt = Path.Split(new char[] { '\\' }).Last();
                var nameWithoutExt = nameWithExt.Split(new char[] { '.' }).First();
                return nameWithoutExt;
            }
        }

        public string Extension { get; set; }

        public int Size { get; set; }
    }
}
