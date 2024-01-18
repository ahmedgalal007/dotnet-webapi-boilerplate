using FSH.WebApi.Application.Common.Interfaces;
using FSH.WebApi.Domain.Article;
using FSH.WebApi.Infrastructure.Persistence.Context;
using FSH.WebApi.Infrastructure.Persistence.Initialization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Infrastructure.Article;
public class CategorySeeder : ICustomSeeder
{

    private readonly ISerializerService _serializerService;
    private readonly ApplicationDbContext _db;
    private readonly ILogger<CategorySeeder> _logger;

    public CategorySeeder(ISerializerService serializerService, ILogger<CategorySeeder> logger, ApplicationDbContext db)
    {
        _serializerService = serializerService;
        _logger = logger;
        _db = db;
    }

    public async Task InitializeAsync(CancellationToken cancellationToken)
    {
        if (!_db.Categories.Any() /*&& _db.Cultures.Any()*/)
        {
            _logger.LogInformation("Started to Seed Categories.");

            // Here you can use your own logic to populate the database.
            // As an example, I am using a JSON file to populate the database.

            Category obj1 = Category.Create(cultureCode: "ar", name: "تصنيف 1", description: string.Empty, color: "#fff");
            obj1.Update(cultureCode: "en", name: "Category 1", description: string.Empty, null);

            //Category obj = new Category("تصنيف 1", "", "#fff", "ar")
            //{
            //    Locals = new List<LocalizedCategory>() {
            //        new LocalizedCategory {  Name = "Category 1", culturCode = "en"}
            //    },
            //    Childrens = new List<Category>() {
            //        new Category("تصنيف 11", "","#fff", "ar")
            //        {
            //            Locals = new List<LocalizedCategory>() {
            //                new LocalizedCategory {  Name = "Category 11", culturCode = "en"}
            //            },
            //            Childrens = new List<Category>() {
            //                new Category()
            //                {
            //                    DefaultCulturCode = "ar",
            //                    Color= "#fff",
            //                    Locals = new List<LocalizedCategory>() {
            //                        new LocalizedCategory {  Name = "تصنيف 111", culturCode = "ar"},
            //                        new LocalizedCategory {  Name = "Category 111", culturCode = "en"}
            //                    }
            //                },
            //                new Category()
            //                {
            //                    DefaultCulturCode = "ar",
            //                    Color= "#fff",
            //                    Locals = new List<LocalizedCategory>() {
            //                        new LocalizedCategory {  Name = "تصنيف 112", culturCode = "ar"},
            //                        new LocalizedCategory {  Name = "Category 112", culturCode = "en"}
            //                    }
            //                }
            //            }
            //        },
            //        new Category()
            //        {
            //            DefaultCulturCode = "ar",
            //            Color= "#fff",
            //            Locals = new List<LocalizedCategory>() {
            //                new LocalizedCategory {  Name = "تصنيف 12", culturCode = "ar"},
            //                new LocalizedCategory {  Name = "Category 12", culturCode = "en"}
            //            },
            //            Childrens = new List<Category>() {
            //                new Category()
            //                {
            //                    DefaultCulturCode = "ar",
            //                    Color= "#fff",
            //                    Locals = new List<LocalizedCategory>() {
            //                        new LocalizedCategory {  Name = "تصنيف 121", culturCode = "ar"},
            //                        new LocalizedCategory {  Name = "Category 121", culturCode = "en"}
            //                    }
            //                },
            //                new Category()
            //                {
            //                    DefaultCulturCode = "ar",
            //                    Color= "#fff",
            //                    Locals = new List<LocalizedCategory>() {
            //                        new LocalizedCategory {  Name = "تصنيف 122", culturCode = "ar"},
            //                        new LocalizedCategory {  Name = "Category 122", culturCode = "en"}
            //                    }
            //                }
            //            }
            //        }
            //    }
            //};
            await _db.Categories.AddAsync(obj1, cancellationToken);
            await _db.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Seeded Categories.");
        }
    }
}
