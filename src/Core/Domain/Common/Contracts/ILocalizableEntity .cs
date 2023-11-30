using System.ComponentModel.DataAnnotations;

namespace FSH.WebApi.Domain.Common.Contracts;

public interface ILocalizableEntity
{
    public string culturCode { get; set; }
}
