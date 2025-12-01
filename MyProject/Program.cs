using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Konfigurer URL'er
builder.WebHost.UseUrls("http://localhost:5000");

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

// Serve static files (CSS, JS, etc.)
app.UseStaticFiles();

// Debug: Vis hvor ASP.NET leder efter filer
Console.WriteLine($"📁 Content Root: {app.Environment.ContentRootPath}");
Console.WriteLine($"📁 Web Root: {app.Environment.WebRootPath}");

// Tjek om login.html eksisterer
var loginPath = Path.Combine(app.Environment.WebRootPath, "login.html");
Console.WriteLine($"🔍 Leder efter: {loginPath}");
Console.WriteLine($"✅ Fil eksisterer: {File.Exists(loginPath)}");

// List alle filer i wwwroot
if (Directory.Exists(app.Environment.WebRootPath))
{
    Console.WriteLine("📄 Filer i wwwroot:");
    foreach (var file in Directory.GetFiles(app.Environment.WebRootPath))
    {
        Console.WriteLine($"  - {Path.GetFileName(file)}");
    }
}

// Route to login page - redirect til static fil
app.MapGet("/", () => Results.Redirect("/login.html"));

// Åbn browser automatisk i Development mode
if (app.Environment.IsDevelopment())
{
    var url = "http://localhost:5000";
    Console.WriteLine($"🌐 Åbner browser på: {url}");

    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
    {
        FileName = url,
        UseShellExecute = true
    });
}

app.Run();

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
}