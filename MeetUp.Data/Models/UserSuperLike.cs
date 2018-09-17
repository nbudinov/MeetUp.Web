namespace MeetUp.Data.Models
{
    public class UserSuperLike
    {
        public int Id { get; set; }

        public int? SuperLikedUserId { get; set; }

        public virtual User SuperLikedUser { get; set; }
    }
}
