using Microsoft.EntityFrameworkCore;

namespace FSH.WebApi.Infrastructure.Persistence.DynamicSchemas;

public class DynamicDbContextOptions : DatabaseSettings
{
    // public override Type ContextType => typeof(DynamicDbContext);



    ////
    //// Summary:
    ////     Adds the given extension to the underlying options and creates a new Microsoft.EntityFrameworkCore.DbContextOptions
    ////     with the extension added.
    ////
    //// Parameters:
    ////   extension:
    ////     The extension to be added.
    ////
    //// Type parameters:
    ////   TExtension:
    ////     The type of extension to be added.
    ////
    //// Returns:
    ////     The new options instance with the given extension added.
    //// public abstract DbContextOptions WithExtension<TExtension>(TExtension extension) where TExtension : class, IDbContextOptionsExtension;
    //public override DbContextOptions WithExtension<TExtension>(TExtension extension)
    //{
    //    throw new NotImplementedException();
    //}
}