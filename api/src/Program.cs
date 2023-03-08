using Microsoft.EntityFrameworkCore;
using twitter_clone;
using twitter_clone.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<TwitterCloneContext>();

var app = builder.Build();

var dbContext = new TwitterCloneContext();
dbContext.Database.EnsureCreated();
if (dbContext.Database.GetPendingMigrations().Any())
{
	dbContext.Database.Migrate();
}

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.MapGet("/", () => "Hello World!");

app.MapGet("/users", () =>
{
	return dbContext.Users.ToList();
});

app.MapGet("/create", () =>
{
	var user = new User { Username = "fabian", Password = "123", IsModerator = true };
	dbContext.Add<User>(user);
	dbContext.SaveChanges();
});

string? port = Environment.GetEnvironmentVariable("API_PORT");
app.Run($"http://+:{port}");
