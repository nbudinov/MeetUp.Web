namespace MeetUp.Data
{
    using MeetUp.Data.Models;
    using System.Collections.Generic;
    using System.Data.Entity;

    public class MeetUpDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<DailySuggestionLogs> DailySuggestionLogs { get; set; }
        public DbSet<UserSuperLikeLogs> UserSuperLikeLogs { get; set; }

        //public DbSet<UserLike> UserLikes { get; set; }

        public MeetUpDbContext() 
            : base("MeetUpDb")
        {

        }

        protected override void OnModelCreating(DbModelBuilder builder)
        {
            builder.Entity<User>()
                .HasMany(u => u.ThisUserLikes)
                .WithMany(u => u.UsersLikeThisUser)
                .Map(c =>
                {
                    c.MapLeftKey("UserId");
                    c.MapRightKey("OtherUserId");
                    c.ToTable("UserLikes");
                });

            builder.Entity<User>()
               .HasMany(u => u.ThisUserSuperLikes)
               .WithMany(u => u.UsersSuperLikeThisUser)
               .Map(c =>
               {
                   c.MapLeftKey("LikingUserId");
                   c.MapRightKey("LikedUserId");
                   c.ToTable("UserSuperLikes");
               });
        }
    }
}
