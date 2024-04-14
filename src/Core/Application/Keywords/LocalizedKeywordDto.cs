using FSH.WebApi.Application.Article.News;
using FSH.WebApi.Application.Common.Localization;
using FSH.WebApi.Domain.Article;
using FSH.WebApi.Domain.Keywords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Application.Keywords;

public class LocalizedKeywordDto: LocalizedDto
{
    public Guid KeywordId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public static LocalizedKeywordDto MapFrom(LocalizedKeyword localizedKeyword, string? cultureCode = null) =>
        new LocalizedKeywordDto()
        {
            Id = localizedKeyword.Id,
            IsDefault = localizedKeyword.IsDefault,
            KeywordId = localizedKeyword.KeywordId,
            CulturCode = localizedKeyword.CulturCode,
            Description = localizedKeyword.Description,
            Enabled = localizedKeyword.Enabled,
            Title = localizedKeyword.Title,
        };
}
