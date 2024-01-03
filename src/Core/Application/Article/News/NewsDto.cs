using FSH.WebApi.Domain.Article;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Application.Article.News;
public class NewsDto : IDto
{
    public Guid Id { get; set; }
    public string? Slug { get; set; }
    public string? MainImage { get; set; }
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string? Body { get; set; }
    public string? SubTitle { get; set; }
    public string? SeoTitle { get; set; }
    public string? SocialTitle { get; set; }
    public string? CultureCode { get; set; }

    public static NewsDto MapFrom(Domain.Article.News news, string? cultureCode = null)
    {
        if(cultureCode is null) cultureCode = news.DefaultCulturCode!;
        LocalizedNews? local = news.Locals.FirstOrDefault(x => x.culturCode == cultureCode);

        if (local == null) throw new NotFoundException("News {0} Not Found.");
        return new NewsDto()
        {
            Id = news.Id,
            Slug = news.Slug,
            MainImage = news.MainImage,
            CultureCode = local.culturCode,
            Title = local.Title,
            SubTitle = local.SocialTitle,
            SeoTitle = local.SocialTitle,
            SocialTitle = local.SocialTitle,
            Description = local.Description,
            Body = local.Body
        };
    }
}