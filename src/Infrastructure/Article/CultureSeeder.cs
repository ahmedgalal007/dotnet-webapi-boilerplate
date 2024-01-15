using FSH.WebApi.Application.Common.Interfaces;
using FSH.WebApi.Domain.Catalog;
using FSH.WebApi.Domain.Common.Localizations;
using FSH.WebApi.Infrastructure.Catalog;
using FSH.WebApi.Infrastructure.Persistence.Context;
using FSH.WebApi.Infrastructure.Persistence.Initialization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Infrastructure.Article;
public class CultureSeeder: ICustomSeeder
{

    private readonly ISerializerService _serializerService;
    private readonly ApplicationDbContext _db;
    private readonly ILogger<BrandSeeder> _logger;

    public CultureSeeder(ISerializerService serializerService, ILogger<BrandSeeder> logger, ApplicationDbContext db)
    {
        _serializerService = serializerService;
        _logger = logger;
        _db = db;
    }

    public async Task InitializeAsync(CancellationToken cancellationToken)
    {
        //if (!_db.Cultures.Any())
        //{
        //    _logger.LogInformation("Started to Seed Cultures.");

        //    // Here you can use your own logic to populate the database.
        //    await _db.Cultures.AddRangeAsync(
        //        new List<Culture> {
        //            new Culture { Code = "ar", Name = "العربية" },
        //            new Culture { Code = "en", Name = "English" }
        //        },
        //        cancellationToken
        //    );
        //    await _db.SaveChangesAsync(cancellationToken);

        //    await _db.SaveChangesAsync(cancellationToken);
        //    _logger.LogInformation("Seeded Cultures.");
        //}
    }
}
