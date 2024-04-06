using System.ComponentModel.DataAnnotations;

namespace FSH.WebApi.Domain.Common.Contracts;

public interface ILocalizableEntity : IEntity<DefaultIdType>
{
    public string CulturCode { get; }
    // abstract static ILocalizableEntity Create { get; }
}
