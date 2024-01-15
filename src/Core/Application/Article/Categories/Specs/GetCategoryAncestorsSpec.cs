using Ardalis.Specification;
using FSH.WebApi.Application.Catalog.Brands;
using FSH.WebApi.Domain.Article;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace FSH.WebApi.Application.Article.Categories.Specs;
public class GetCategoryAncestorsSpec : SingleResultSpecification<Category, CategoryDto>
{
    public GetCategoryAncestorsSpec(Guid Id, int maxDepth, string cultureCode)
    {
        Query.EnableCache(nameof(GetCategoryAncestorsSpec), Id);

        Query.Where(e => e.Id == Id);

        //Query.Select( category => new CategoryDto()
        //{
        //    Id = category.Id,
        //    Slug = category.Slug,
        //    Color = category.Color,
        //    DisplayOrder = category.DisplayOrder,
        //    ParentId = category.ParentId,
        //    Locals = category.Locals.Adapt<List<LocalizedCategoryDto>>(),
        //    Parent = (category.ParentId != null)
        //             ? Query.Where(e => e.Id == category.ParentId).AsNoTracking().Adapt<CategoryDto>()
        //             : null
        //});
        Query.Select(GetParent(Query, 1000));
    }


    public static Expression<Func<Category, CategoryDto>> GetParent(ISpecificationBuilder<Category, CategoryDto> Query, int maxDepth, int currentDepth = 0, string cultureCode = "en")
    {
        Expression<Func<Category, CategoryDto>> result = category => new CategoryDto()
        {
            Id = category.Id,
            Slug = category.Slug,
            Color = category.Color,
            DisplayOrder = category.DisplayOrder,
            ParentId = category.ParentId,
            Parent = null,
            Locals = category.Locals.Adapt<List<LocalizedCategoryDto>>(),
        };
        return result;
    }
}
