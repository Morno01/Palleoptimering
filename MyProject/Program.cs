using System;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MyProject;

var builder = WebApplication.CreateBuilder(args);

// Register your ApplicationDbContext with the SQL Server connection string from appsettings.json
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Verbose DB connection test (after app is built so app.Services is available)
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var logger = services.GetRequiredService<ILogger<Program>>();
    var dbContext = services.GetRequiredService<ApplicationDbContext>();
    var connection = dbContext.Database.GetDbConnection();

    try
    {
        logger.LogInformation("Attempting to open DB connection to {DataSource}/{Database}", connection.DataSource, connection.Database);
        connection.Open();
        logger.LogInformation("✅ Opened DB connection. State: {State}", connection.State);
        Console.WriteLine("✅ Database forbindelse er OK!");
    }
    catch (Exception ex)
    {
        // Log full exception + inner exceptions for diagnosis
        logger.LogError(ex, "❌ Failed to open DB connection");
        Console.WriteLine("❌ Database fejl: " + ex);
        var inner = ex.InnerException;
        while (inner != null)
        {
            Console.WriteLine("Inner: " + inner);
            inner = inner.InnerException;
        }
    }
    finally
    {
        if (connection.State == ConnectionState.Open)
            connection.Close();
    }
}

app.MapGet("/", () => "Hello World!");

app.Run();
