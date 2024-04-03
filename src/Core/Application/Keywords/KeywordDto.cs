using FSH.WebApi.Application.Article.News;
using FSH.WebApi.Domain.Article;
using FSH.WebApi.Domain.Keywords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Application.Keywords;
public class KeywordDto : IDto
{
    public Guid? Id { get; set; }
    public bool? IsCreativeWork { get; set; } = false;
    public bool? IsEvent { get; set; } = false;
    public bool? IsOrganization { get; set; } = false;
    public bool? IsPerson { get; set; } = false;
    public bool? IsPlace { get; set; } = false;
    public bool? IsProduct { get; set; } = false;
    public string? DefaultCultureCode { get; set; } = "ar-EG";
    public string? CultureCode { get; set; } = "ar-EG";
    public ICollection<LocalizedKeyword> Locals { get; set; } = new List<LocalizedKeyword>();
    public List<string> ActiveLanguages { get; set; } = new List<string>();

    public static KeywordDto MapFrom(Keyword keyword, string? cultureCode = null)
    {
        if (cultureCode is null) cultureCode = keyword.DefaultCulturCode!;
        // LocalizedKeyword? local = keyword.Locals.FirstOrDefault(x => x.CulturCode == cultureCode);

        // if (local == null) throw new NotFoundException("Keyword {0} Not Found.");
        return new KeywordDto()
        {
            IsCreativeWork = keyword.IsCreativeWork,
            IsEvent = keyword.IsEvent,
            IsOrganization = keyword.IsOrganization,
            IsPerson = keyword.IsPerson,
            IsPlace = keyword.IsPlace,
            IsProduct = keyword.IsProduct,
            Id = keyword.Id,
            DefaultCultureCode = keyword.DefaultCulturCode,
            // CultureCode = local.CulturCode,
            // CultureCode = local.CulturCode,
            // Title = keyword.Slug,
            Locals = keyword.Locals,
        };
    }
}
