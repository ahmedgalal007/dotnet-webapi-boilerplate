using FSH.WebApi.Infrastructure.Persistence.DynamicSchemas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Internal;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

public class MigrationService
{
    public static void Main(string[] args)
    {
        var serviceProvider = new ServiceCollection()
            .AddDbContext<DynamicDbContext>(options =>
                options.UseSqlServer("YourConnectionString"))
            .AddScoped<IMigrationsAssembly, MigrationsAssembly>()
            .AddScoped<IMigrator, Migrator>()
            .BuildServiceProvider();

        using (var scope = serviceProvider.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<DynamicDbContext>();

            // Apply pending migrations
            dbContext.Database.Migrate();

            // Create a new migration
            var migrationsAssembly = scope.ServiceProvider.GetRequiredService<IMigrationsAssembly>();
            var migrator = scope.ServiceProvider.GetRequiredService<IMigrator>();

            // var migrations = migrationsAssembly.GetMigrator();
            var migrations = migrationsAssembly.Migrations;
            var migrationName = "AddNewFieldMigration"; // Provide the name of your migration here

            // Generate a migration script (optional)
            var migrationSql = migrator.GenerateScript(migrationName);
            Console.WriteLine("Generated Migration SQL:");
            Console.WriteLine(migrationSql);

            // Apply the migration (only necessary if not applying all pending migrations)
            migrator.Migrate(migrationName);
        }
    }
}
