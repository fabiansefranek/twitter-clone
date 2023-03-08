using Microsoft.EntityFrameworkCore;
using twitter_clone.Models;

namespace twitter_clone
{
		public class TwitterCloneContext : DbContext
		{
			public TwitterCloneContext() {}
			public TwitterCloneContext(DbContextOptions<TwitterCloneContext> options) : base(options) {}

			public DbSet<User> Users{ get; set; }

			protected override void OnModelCreating(ModelBuilder builder)
			{
			}

			protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
				var port = Environment.GetEnvironmentVariable("DB_PORT");
				var host = Environment.GetEnvironmentVariable("DB_HOST");
				var database = Environment.GetEnvironmentVariable("DB_DATABASE");
				var username = Environment.GetEnvironmentVariable("DB_USERNAME");
				var password = Environment.GetEnvironmentVariable("DB_PASSWORD");
				optionsBuilder.UseNpgsql($"Host={host};Database={database};Username={username};Password={password}");
			}
		}
}

