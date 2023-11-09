using System.ComponentModel.DataAnnotations;

namespace FSH.WebApi.Domain.Common.Localizations;
public class Culture
{
    [MaxLength(2)]
    [Key]
    public string Code { get; set; }
    [MaxLength(50)]
    public string Name { get; set; }
}
