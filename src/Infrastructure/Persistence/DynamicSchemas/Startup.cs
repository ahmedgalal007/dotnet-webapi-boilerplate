using EFCore.AutomaticMigrations;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
namespace FSH.WebApi.Infrastructure.Persistence.DynamicSchemas;
public static class Startup
{

    public static IApplicationBuilder UseDynamicDbContext(this IApplicationBuilder app)
    {
        // var context = app.ApplicationServices.GetRequiredService<DynamicDbContext>();
        // Reset database schema async
        // context.MigrateToLatestVersion(new DbMigrationsOptions { ResetDatabaseSchema = false });

        // begin apply automatic migration database to latest version
        using (AsyncServiceScope serviceScope = app.ApplicationServices.CreateAsyncScope())
        {
            using (DynamicDbContext? dbContext = serviceScope.ServiceProvider.GetService<DynamicDbContext>())
            {
                if (dbContext is not null)
                {
                    // If the database context was successfully resolved from the service provider, we apply migrations.
                    // The DbMigrationsOptions object is used to configure automatic data loss prevention and offers other tools like viewing raw SQL scripts for migrations.
                    // The database is created automatically if it does not exist, if exist will be updated to latest model changes
                    // Pay attention if you are using a PaaS database, like Azure; it will be created automatically using the default SKU and this might affect your costs.
                    dbContext.Database.EnsureCreated();
                    dbContext.MigrateToLatestVersionAsync(new DbMigrationsOptions { AutomaticMigrationDataLossAllowed = true });

                    //at this stage dabatabase containse latest changes
                    // Todo entity was mapped via Fluent API
                    // TodoPoco entity was mapped via data annotations
                }
            }
        }
        return app;
    }
}
