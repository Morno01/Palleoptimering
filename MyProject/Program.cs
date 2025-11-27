using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Register DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Test database forbindelse
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    try
    {
        var canConnect = dbContext.Database.CanConnect();
        if (canConnect)
        {
            Console.WriteLine("✅ Database forbindelse er OK!");
        }
        else
        {
            Console.WriteLine("❌ Kan ikke forbinde til databasen!");
        }
        }
        catch (Exception ex)
    {
        Console.WriteLine($"❌ Database fejl: {ex.Message}");
    }
}

app.MapGet("/", () => "Hello World!");

app.Run();

// DbContext definition
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    // Add your DbSet properties here
    // Example: public DbSet<YourEntity> YourEntities { get; set; }
}
