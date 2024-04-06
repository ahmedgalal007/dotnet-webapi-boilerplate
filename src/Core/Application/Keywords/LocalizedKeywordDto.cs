using FSH.WebApi.Application.Article.News;
using FSH.WebApi.Domain.Article;
using FSH.WebApi.Domain.Keywords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Application.Keywords;
public class LocalizedKeywordDto : IDto
{
    public Guid? Id { get; set; }
    public Guid KeywordId { get; private set; }
    public string CulturCode { get; private set; } = string.Empty;
    public string Title { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public bool Enabled { get; private set; } = false;
    public bool IsDefault { get; private set; } = false;

}
