using FSH.WebApi.Application.Catalog.Products.EventHandlers;
using FSH.WebApi.Domain.Article;
using FSH.WebApi.Domain.Common.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Application.Article.News.EventHandlers;
public class NewsCreatedEventHandler : EventNotificationHandler<EntityCreatedEvent<Domain.Article.News>>
{
    private readonly ILogger<NewsCreatedEventHandler> _logger;
    public NewsCreatedEventHandler(ILogger<NewsCreatedEventHandler> logger) => _logger = logger;
    public override Task Handle(EntityCreatedEvent<Domain.Article.News> @event, CancellationToken cancellationToken)
    {
        _logger.LogInformation("{event} Triggered", @event.GetType().Name);
        return Task.CompletedTask;
    }
}