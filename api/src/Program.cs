using Microsoft.EntityFrameworkCore;
using twitter_clone;

var builder = WebApplication.CreateBuilder(args);
var startup = new Startup();
startup.ConfigureServices(builder.Services);
var app = builder.Build();
var db = new TwitterCloneContext();
startup.Configure(app, builder.Environment, db);

// Routes
var routes = new Routes(db);
routes.Mount(app);

string? port = Environment.GetEnvironmentVariable("API_PORT");
app.Run($"http://+:{port}");
