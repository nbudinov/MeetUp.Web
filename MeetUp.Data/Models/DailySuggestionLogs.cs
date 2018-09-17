namespace MeetUp.Data.Models
{
    using System;

    public class DailySuggestionLogs
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public DateTime Date { get; set; }
    }
}
