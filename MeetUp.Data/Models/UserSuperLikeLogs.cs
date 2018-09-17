namespace MeetUp.Data.Models
{
    using System;

    public class UserSuperLikeLogs
    {
        public int Id { get; set; }

        public int? UserLikingId { get; set; }

        public virtual User UserLiking { get; set; }

        public int? UserLikedId { get; set; }

        public virtual User UserLiked { get; set; }

        public DateTime Date { get; set; }
    }
}
