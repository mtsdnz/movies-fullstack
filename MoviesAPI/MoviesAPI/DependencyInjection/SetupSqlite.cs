using Microsoft.EntityFrameworkCore;
using MoviesAPI.Infra;

namespace MoviesAPI.DependencyInjection;

public static class SetupSqlite
{
    public static void AddDbContextForSqlite(this IServiceCollection services)
    {
        services.AddSqlite<ApplicationDbContext>("Data Source=movies.db", options => { });
    }

    public static void EnsureDatabaseSetup(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;
        var db = services.GetRequiredService<ApplicationDbContext>();
        // db.Database.EnsureCreated();
        // On a real scenario we would only do this on development environment
        db.Database.Migrate();

        SeedDb(scope.ServiceProvider, db).GetAwaiter().GetResult();
    }

    private static Task SeedDb(IServiceProvider sp, ApplicationDbContext dbContext)
    {
        var logger = sp.GetRequiredService<ILoggerFactory>();
        var seeder = new ApplicationDbSeeder(logger.CreateLogger<ApplicationDbSeeder>());

        return seeder.SeedAsync(dbContext);
    }
}