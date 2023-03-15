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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>().HasIndex(u => u.Username).IsUnique();
            builder.Entity<User>().HasMany(u => u.Posts).WithOne(p => p.User).IsRequired();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var port = Environment.GetEnvironmentVariable("DB_PORT");
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
