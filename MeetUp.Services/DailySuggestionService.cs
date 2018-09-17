namespace MeetUp.Services
{
    using MeetUp.Data;
    using MeetUp.Data.Models;
    using System;
    using System.Linq;

    public class DailySuggestionService : IDailySuggestionService
    {
        public MeetUpDbContext db;

        public DailySuggestionService(MeetUpDbContext db)
        {
            this.db = db;
        }

        public void SaveDailySuggestionLog(int userId)
        {
            var d = new DailySuggestionLogs
            {
                Date = DateTime.Now,
                UserId = userId
            };

            this.db.DailySuggestionLogs.Add(d);
            db.SaveChanges();
        }

        public bool ShouldPopupToday(int userId)
        {
            var today = DateTime.Now.Date;

            return this.db
                .DailySuggestionLogs
                .Where(l => l.UserId == userId)
                .Where(l => l.Date.Year == today.Year && l.Date.Month == today.Month && l.Date.Day == today.Day)
                .FirstOrDefault() == null;
                
        }
    }
}
