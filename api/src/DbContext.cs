using Microsoft.EntityFrameworkCore;
using twitter_clone.Models;

namespace twitter_clone
{
    public class TwitterCloneContext : DbContext
    {
        public TwitterCloneContext() { }

        public TwitterCloneContext(DbContextOptions<TwitterCloneContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Follow> Follows { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Report> Reports { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>().HasIndex(u => u.Username).IsUnique();
            builder.Entity<User>().HasMany(u => u.Posts).WithOne(p => p.User).IsRequired();
            builder.Entity<User>().HasMany(u => u.Likes).WithOne(p => p.User).IsRequired(false);
            builder.Entity<Post>().HasMany(r => r.Likes).WithOne(p => p.Post).IsRequired(false);
            builder
                .Entity<Follow>()
                .HasOne(f => f.Follower)
                .WithMany(u => u.Follows)
                .IsRequired(false);
            builder.Entity<User>().Property(u => u.Fullname).IsRequired(false);
            builder.Entity<UserDTO>().Property(u => u.Fullname).IsRequired(false);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var host = Environment.GetEnvironmentVariable("DB_HOST");
            var database = Environment.GetEnvironmentVariable("DB_DATABASE");
            var username = Environment.GetEnvironmentVariable("DB_USERNAME");
            var password = Environment.GetEnvironmentVariable("DB_PASSWORD");
            optionsBuilder.UseNpgsql(
                $"Host={host};Database={database};Username={username};Password={password}"
            );
        }
    }
}
