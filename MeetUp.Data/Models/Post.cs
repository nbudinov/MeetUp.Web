namespace MeetUp.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Post
    {
        public int Id { get; set; }

        [MinLength(2)]
        [MaxLength(50)]
        public string Title { get; set; }

        [MinLength(10)]
        [MaxLength(1000)]
        public string Text { get; set; }

        public int UserId { get; set; }

        public virtual User User { get; set; }
    }
}
