namespace MeetUp.Data.Models
{
    public class Image
    {
        public int Id { get; set; }

        public string Path { get; set; }

        public string Extension { get; set; }

        public int Size { get; set; }

        public virtual User User { get; set; }

        public int UserId { get; set; }
    }
}
