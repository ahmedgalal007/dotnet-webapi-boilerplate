using FSH.WebApi.Application.Catalog.Products;
using FSH.WebApi.Application.Keywords;
using FSH.WebApi.Domain.Keywords;
using Mapster;

namespace FSH.WebApi.Infrastructure.Mapping;

public class MapsterSettings
{
    public static void Configure()
    {
        // here we will define the type conversion / Custom-mapping
        // More details at https://github.com/MapsterMapper/Mapster/wiki/Custom-mapping

        // This one is actually not necessary as it's mapped by convention
        // TypeAdapterConfig<Product, ProductDto>.NewConfig().Map(dest => dest.BrandName, src => src.Brand.Name);
        // TypeAdapterConfig<Keyword, KeywordDto>.NewConfig().Map(dest => dest.Locals, src => src.Locals);
    }
}