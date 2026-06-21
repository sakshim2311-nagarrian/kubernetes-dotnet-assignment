using Microsoft.EntityFrameworkCore;
using ServiceApiTier.Models;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();

// Configure Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Service API Tier",
        Version = "v1",
        Description = "A simple ASP.NET Core Web API for Kubernetes deployment"
    });
});

// Configure Database Connection
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add password from environment variable (Kubernetes Secret)
var dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD") ?? "yourpassword123";
connectionString += $";Password={dbPassword}";

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

// Add Health Checks
builder.Services.AddHealthChecks();

// Configure CORS (for development/testing)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Auto-apply database migrations on startup
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<AppDbContext>();
        context.Database.Migrate(); // This creates/updates the database
        app.Logger.LogInformation("Database migration completed successfully.");
    }
    catch (Exception ex)
    {
        app.Logger.LogError(ex, "An error occurred while migrating the database.");
    }
}

// Configure the HTTP request pipeline
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Service API v1");
    c.RoutePrefix = string.Empty; // Swagger at root URL
});

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

// Map health check endpoint
app.MapHealthChecks("/health");

app.Run();