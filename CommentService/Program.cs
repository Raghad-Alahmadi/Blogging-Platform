using Blogging_Platform.DTO;
using CommentService.Services;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Configuration for PostService URL from environment variable
var postServiceUrl = Environment.GetEnvironmentVariable("POST_SERVICE_URL") ?? "http://localhost:6001";
builder.Services.AddHttpClient("PostService", client =>
{
    client.BaseAddress = new Uri(postServiceUrl);
});

// Register services
builder.Services.AddSingleton<ICommentRepository, InMemoryCommentRepository>();
builder.Services.AddScoped<IPostValidationService, PostValidationService>();
builder.Services.AddControllers();

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

var app = builder.Build();

// Configure middleware
app.UseCors("AllowAll");
app.MapControllers();

app.Run();