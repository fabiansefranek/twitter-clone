using twitter_clone;

var builder = WebApplication.CreateBuilder(args);
var startup = new Startup();
startup.ConfigureServices(builder.Services);
var app = builder.Build();
var db = new TwitterCloneContext();
startup.Configure(app, builder.Environment, db);

// Routes
Routes.Mount(app);

var port = Environment.GetEnvironmentVariable("API_PORT");
app.Run($"http://+:{port}");
