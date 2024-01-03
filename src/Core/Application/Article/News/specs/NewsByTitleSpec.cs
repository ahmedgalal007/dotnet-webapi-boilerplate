using FSH.WebApi.Domain.Article;

namespace FSH.WebApi.Application.Article.News.specs;

internal class NewsByTitleSpec : Specification<Domain.Article.News>, ISingleResultSpecification
{
    public NewsByTitleSpec(string title) => Query.Where(b => b.Locals.Any(c => c.Title.Contains(title)));
}