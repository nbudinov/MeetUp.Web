namespace MeetUp.Data
{
    using MeetUp.Data.Models;
    using System.Data.Entity;

    public class MeetUpDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Post> Posts { get; set; }

        public MeetUpDbContext() 
            : base("MeetUpDb")
        {

        }

        protected override void OnModelCreating(DbModelBuilder builder)
        {
            builder.Entity<User>()
                .HasMany(u => u.ThisUsersLikes)
                .WithMany(u => u.UsersLikeThisUser)
                .Map(c =>
                {
                    c.MapLeftKey("UserId");
                    c.MapRightKey("OtherUserId");
                    c.ToTable("UserLikes");
                });
        }
    }
}
