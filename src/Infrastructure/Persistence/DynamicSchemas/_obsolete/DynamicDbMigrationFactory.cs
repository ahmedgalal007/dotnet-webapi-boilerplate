// using Microsoft.EntityFrameworkCore.Design;
// using Microsoft.EntityFrameworkCore.Infrastructure;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.Extensions.DependencyInjection;
// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using System.Threading.Tasks;
//
// namespace FSH.WebApi.Infrastructure.Persistence.DynamicSchemas;
// public class MigrationConfiguration : DbMigrationsConfiguration<DynamicDbContext>
// {
//
// }
// public class DynamicDbMigrationFactory
// {
//     private readonly DynamicDbContext _dbContext;
//
//     public DynamicDbMigrationFactory(DynamicDbContext dbContext)
//     {
//         _dbContext = dbContext;
//     }
//
//     public void CreateMigration(string name, string path)
//     {
//         var config = new DbMigrationsConfiguration<DynamicDbContext> { AutomaticMigrationsEnabled = true };
//         var migrator = new DbMigrator(config);
//         migrator.Update();
//
//         // using (_dbContext)
//         // {
//         //     var services = ((IInfrastructure<IServiceProvider>)db).Instance;
//         //     var codeHelper = new CSharpHelper();
//         //     var scaffolder = ActivatorUtilities.CreateInstance<MigrationsScaffolder>(
//         //         services,
//         //         new CSharpMigrationsGenerator(
//         //             codeHelper,
//         //             new CSharpMigrationOperationGenerator(codeHelper),
//         //             new CSharpSnapshotGenerator(codeHelper)));
//         //
//         //     var migration = scaffolder.ScaffoldMigration(
//         //         "MyMigration",
//         //         "MyApp.Data");
//         //
//         //     File.WriteAllText(
//         //         migration.MigrationId + migration.FileExtension,
//         //         migration.MigrationCode);
//         //     File.WriteAllText(
//         //         migration.MigrationId + ".Designer" + migration.FileExtension,
//         //         migration.MetadataCode);
//         //     File.WriteAllText(
//         //         migration.SnapshotName + migration.FileExtension,
//         //         migration.SnapshotCode);
//         // }
//
//     }
// }
