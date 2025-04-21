using Blogging_Platform.DTO;
using PostService.Services;

var builder = WebApplication.CreateBuilder(args);

// Register services
builder.Services.AddSingleton<IPostRepository, InMemoryPostRepository>();
builder.Services.AddControllers();

// Add CORS to allow any origin (replacing the previous policy)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowMainApp",
        builder => builder
            .AllowAnyOrigin()  // Updated to allow any origin
            .AllowAnyMethod()
            .AllowAnyHeader());
});

// Build app
var app = builder.Build();

// Configure middleware
app.UseCors("AllowMainApp");
app.MapControllers();

app.Run();