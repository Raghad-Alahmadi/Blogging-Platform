// PostService/Program.cs
using Blogging_Platform.DTO;
using PostService.Services;

var builder = WebApplication.CreateBuilder(args);

// Register services
builder.Services.AddSingleton<IPostRepository, InMemoryPostRepository>();
builder.Services.AddControllers();

// Add CORS to allow the main project to call this service
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowMainApp",
        builder => builder
            .WithOrigins("https://localhost:5001") // Main app URL
            .AllowAnyMethod()
            .AllowAnyHeader());
});

// Build app
var app = builder.Build();

// Configure middleware
app.UseCors("AllowMainApp");
app.MapControllers();

app.Run();
