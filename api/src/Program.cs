var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

string port = Environment.GetEnvironmentVariable("API_PORT");
app.Run($"http://+:{port}");