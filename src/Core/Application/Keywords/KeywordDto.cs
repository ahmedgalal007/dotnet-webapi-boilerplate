using FSH.WebApi.Application.Common.Localization;
using FSH.WebApi.Domain.Keywords;

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
    public List<string>? Languages { get; set; } = new List<string>();
    public List<LocalizedKeywordDto> Locals { get; set; } = new ();

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
            Languages = keyword.GetLanguages(),
            // CultureCode = local.CulturCode,
            // CultureCode = local.CulturCode,
            // Title = keyword.Slug,
            Locals = keyword.Locals.Select(e => LocalizedKeywordDto.MapFrom(e)).ToList(),
        };
    }
}
