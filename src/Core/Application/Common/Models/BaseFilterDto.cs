
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace FSH.WebApi.Application.Common.Models;
public class BaseFilterDto
{
    [Browsable(false)]
    [JsonIgnore]
    public bool LoadChildren { get; set; } = false;
    [Browsable(false)]
    [JsonIgnore]
    public bool IsPagingEnabled { get; set; } = false;

    public int PageIndex { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
