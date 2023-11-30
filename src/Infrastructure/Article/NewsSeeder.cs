﻿using FSH.WebApi.Application.Common.Interfaces;
using FSH.WebApi.Domain.Catalog;
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
public class NewsSeeder: ICustomSeeder
{

    private readonly ISerializerService _serializerService;
    private readonly ApplicationDbContext _db;
    private readonly ILogger<BrandSeeder> _logger;

    public NewsSeeder(ISerializerService serializerService, ILogger<BrandSeeder> logger, ApplicationDbContext db)
    {
        _serializerService = serializerService;
        _logger = logger;
        _db = db;
    }

    public async Task InitializeAsync(CancellationToken cancellationToken)
    {
        if (!_db.News.Any() /*&& _db.Cultures.Any()*/)
        {
            _logger.LogInformation("Started to Seed News.");

            // Here you can use your own logic to populate the database.
            // As an example, I am using a JSON file to populate the database.

            Domain.Article.News obj = new Domain.Article.News("إختبار", "وصف الخبر", null,null,null,null,"ar");
            await _db.News.AddAsync(obj, cancellationToken);
            await _db.SaveChangesAsync(cancellationToken);

            obj.Update("test", "News Description", null, null, null, null, "en");
            _db.Update(obj);

            await _db.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Seeded News.");
        }
    }
}