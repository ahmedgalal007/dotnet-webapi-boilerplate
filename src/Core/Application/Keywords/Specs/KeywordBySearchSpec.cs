using FSH.WebApi.Domain.Keywords;
using Mapster;

namespace FSH.WebApi.Application.Keywords.Specs;

public class KeywordBySearchSpec : EntitiesByPaginationFilterSpec<Keyword, KeywordDto>
{
    public KeywordBySearchSpec(SearchKeywordRequest request)
        : base(request)
    {
        Query.OrderBy(c => c.Id, !request.HasOrderBy());
        Query.Select(e => KeywordDto.MapFrom(e, request.CultureCode));
    }
}