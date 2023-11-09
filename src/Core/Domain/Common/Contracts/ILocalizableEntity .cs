using System.ComponentModel.DataAnnotations;

namespace FSH.WebApi.Domain.Common.Contracts;

public interface ILocalizableEntity
{
    [MaxLength(6)]
    public string culturCode { get; set; }
}
