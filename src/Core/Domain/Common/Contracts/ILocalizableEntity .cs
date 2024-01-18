using System.ComponentModel.DataAnnotations;

namespace FSH.WebApi.Domain.Common.Contracts;

public interface ILocalizableEntity
{
    public string CulturCode { get; }
    // abstract static ILocalizableEntity Create { get; }
}
