using FSH.WebApi.Domain.Article;
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
    public LocalizedNews? Local { get; set; } = new LocalizedNews();
}
