// using System;
// using System.IO;
// using System.Linq;
// using System.Reflection;
// using System.Runtime.Loader;
// using System.Collections.Generic;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Design;
// using Microsoft.EntityFrameworkCore.Storage;
// using Microsoft.EntityFrameworkCore.Diagnostics;
// using Microsoft.EntityFrameworkCore.Scaffolding;
// using Microsoft.EntityFrameworkCore.Scaffolding.Internal;
// using Microsoft.EntityFrameworkCore.SqlServer.Diagnostics.Internal;
// using Microsoft.EntityFrameworkCore.SqlServer.Scaffolding.Internal;
// using Microsoft.EntityFrameworkCore.SqlServer.Storage.Internal;
// using Microsoft.Extensions.DependencyInjection;
// using Microsoft.CodeAnalysis;
// using Microsoft.CodeAnalysis.CSharp;
// using System.Diagnostics.CodeAnalysis;
// using EFCore.AutomaticMigrations;
   
// namespace FSH.WebApi.Infrastructure.Persistence.DynamicSchemas.Scaffold;
// internal class CreateMssqlScaffolder
// {
//     private readonly DynamicDbContext _dynamicDb;
//     public CreateMssqlScaffolder(DynamicDbContext dynamicDb)
//     {
//         _dynamicDb = dynamicDb;
//     }
//     public void ScaffoldDatabase()
//     {
//         _dynamicDb.MigrateToLatestVersion(new DbMigrationsOptions { ResetDatabaseSchema = false });
//     }
//     public void ScaffoldDatabase2()
//     {
//         var connectionString = "Pass connection string as a first parameter";
   
//         var scaffolder = CreateMssqlScaffolder();
//         // All tables and schemes
//         var dbOpts = new DatabaseModelFactoryOptions();
   
//         // Use the database schema names directly
//         var modelOpts = new ModelReverseEngineerOptions();
   
//         var codeGenOpts = new ModelCodeGenerationOptions()
//         {
//             // Set namespaces
//             RootNamespace = "FSH.WebApi.Infrastructure",
//             ContextName = "DynamicDbContext",
//             ContextNamespace = "FSH.WebApi.Infrastructure.Persistence.DynamicSchemas",
//             ModelNamespace = "FSH.WebApi.Infrastructure.Persistence.DynamicSchemas.Models",
   
//             // We are not afraid of the connection string in the source code, 
//             // because it will exist only in runtime
//             SuppressConnectionStringWarning = true
//         };
   
//         var scaffoldedModelSources = scaffolder.ScaffoldModel(connectionString, dbOpts, modelOpts, codeGenOpts);
//         var sourceFiles = new List<string> { scaffoldedModelSources.ContextFile.Code };
//         sourceFiles.AddRange(scaffoldedModelSources.AdditionalFiles.Select(f => f.Code));
   
//         using var peStream = new MemoryStream();
   
//         var enableLazyLoading = false;
//         var result = GenerateCode(sourceFiles, enableLazyLoading).Emit(peStream);
   
//         if (!result.Success)
//         {
//             var failures = result.Diagnostics
//                 .Where(diagnostic => diagnostic.IsWarningAsError ||
//                                      diagnostic.Severity == DiagnosticSeverity.Error);
   
//             var error = failures.FirstOrDefault();
//             throw new Exception($"{error?.Id}: {error?.GetMessage()}");
//         }
   
//         var assemblyLoadContext = new AssemblyLoadContext("DynamicDbContext", isCollectible: !enableLazyLoading);
   
//         peStream.Seek(0, SeekOrigin.Begin);
//         var assembly = assemblyLoadContext.LoadFromStream(peStream);
   
//         var type = assembly.GetType("FSH.WebApi.Infrastructure.Persistence.DynamicSchemas.DynamicDbContext");
//         _ = type ?? throw new Exception("DataContext type not found");
   
//         var constr = type.GetConstructor(Type.EmptyTypes);
//         _ = constr ?? throw new Exception("DataContext ctor not found");
   
//         DbContext dynamicContext = (DbContext)constr.Invoke(null);
//         var entityTypes = dynamicContext.Model.GetEntityTypes();
   
//         Console.WriteLine($"Context contains {entityTypes.Count()} types");
   
//         foreach (var entityType in dynamicContext.Model.GetEntityTypes())
//         {
//             var items = (IQueryable<object>)dynamicContext.Query(entityType.Name);
   
//             Console.WriteLine($"Entity type: {entityType.Name} contains {items.Count()} items");
//         }
   
   
   
//         [SuppressMessage("Usage", "EF1001:Internal EF Core API usage.", Justification = "We need it")]
//         static IReverseEngineerScaffolder CreateMssqlScaffolder() =>
//                 new ServiceCollection()
//                    .AddEntityFrameworkSqlServer()
//                    .AddLogging()
//                    .AddEntityFrameworkDesignTimeServices()
//                    .AddSingleton<LoggingDefinitions, SqlServerLoggingDefinitions>()
//                    .AddSingleton<IRelationalTypeMappingSource, SqlServerTypeMappingSource>()
//                    .AddSingleton<IAnnotationCodeGenerator, AnnotationCodeGenerator>()
//                    .AddSingleton<IDatabaseModelFactory, SqlServerDatabaseModelFactory>()
//                    .AddSingleton<IProviderConfigurationCodeGenerator, SqlServerCodeGenerator>()
//                    .AddSingleton<IScaffoldingModelFactory, RelationalScaffoldingModelFactory>()
//                    .AddSingleton<IPluralizer, Bricelam.EntityFrameworkCore.Design.Pluralizer>()
//                    .AddSingleton<ProviderCodeGeneratorDependencies>()
//                    .AddSingleton<AnnotationCodeGeneratorDependencies>()
//                    .BuildServiceProvider()
//                    .GetRequiredService<IReverseEngineerScaffolder>();
//     }
   
//     // [SuppressMessage("Usage", "EF1001:Internal EF Core API usage.", Justification = "We need it")]
//     // private IReverseEngineerScaffolder CreatePostgreScaffolder() => new ServiceCollection().AddEntityFrameworkNpgsql().AddLogging().AddEntityFrameworkDesignTimeServices().AddSingleton().AddSingleton().AddSingleton().AddSingleton().AddSingleton().AddSingleton().AddSingleton().BuildServiceProvider().GetRequiredService();
//     // private IReverseEngineerScaffolder CreateSqliteScaffolder() => new ServiceCollection().AddEntityFrameworkSqlite().AddLogging().AddEntityFrameworkDesignTimeServices().AddSingleton().AddSingleton().AddSingleton().AddSingleton().AddSingleton().AddSingleton().AddSingleton().BuildServiceProvider().GetRequiredService();
//     private static List<MetadataReference> CompilationReferences(bool enableLazyLoading)
//     {
//         var refs = new List<MetadataReference>();
//         var referencedAssemblies = Assembly.GetExecutingAssembly().GetReferencedAssemblies();
//         refs.AddRange(referencedAssemblies.Select(a => MetadataReference.CreateFromFile(Assembly.Load(a).Location)));
   
//         refs.Add(MetadataReference.CreateFromFile(typeof(object).Assembly.Location));
//         refs.Add(MetadataReference.CreateFromFile(typeof(BackingFieldAttribute).Assembly.Location));
//         refs.Add(MetadataReference.CreateFromFile(Assembly.Load("netstandard, Version=2.0.0.0").Location));
//         refs.Add(MetadataReference.CreateFromFile(typeof(System.Data.Common.DbConnection).Assembly.Location));
//         refs.Add(MetadataReference.CreateFromFile(typeof(System.Linq.Expressions.Expression).Assembly.Location));
   
//         if (enableLazyLoading)
//         {
//             refs.Add(MetadataReference.CreateFromFile(typeof(ProxiesExtensions).Assembly.Location));
//         }
   
//         return refs;
//     }
   
//     private static CSharpCompilation GenerateCode(List<string> sourceFiles, bool enableLazyLoading)
//     {
//         var options = CSharpParseOptions.Default.WithLanguageVersion(LanguageVersion.CSharp10);
   
//         var parsedSyntaxTrees = sourceFiles.Select(f => SyntaxFactory.ParseSyntaxTree(f, options));
   
//         return CSharpCompilation.Create(
//             $"DataContext.dll",
//             parsedSyntaxTrees,
//             references: CompilationReferences(enableLazyLoading),
//             options: new CSharpCompilationOptions(
//                 OutputKind.DynamicallyLinkedLibrary,
//                 optimizationLevel: OptimizationLevel.Release,
//                 assemblyIdentityComparer: DesktopAssemblyIdentityComparer.Default));
//     }
// }
   
// public static class DynamicContextExtensions
// {
//     public static IQueryable Query(this DbContext context, string entityName) =>
//         context.Query(entityName, context.Model.FindEntityType(entityName).ClrType);
   
//     static readonly MethodInfo SetMethod =
//         typeof(DbContext).GetMethod(nameof(DbContext.Set), 1, new[] { typeof(string) }) ??
//         throw new Exception("Type not found: DbContext.Set");
   
//     public static IQueryable Query(this DbContext context, string entityName, Type entityType) =>
//         SetMethod.MakeGenericMethod(entityType)?.Invoke(obj: context, new[] { entityName }) as IQueryable ??
//         throw new Exception($"Type not found: {entityType.FullName}");
// }