namespace FSH.WebApi.Application.Common.Localization;

public class LocalizedDto : LocalizedDto<DefaultIdType> { }
public class LocalizedDto<TID> : IDto
{
    public TID? Id { get; set; }
    public string CulturCode { get; set; }
    public bool Enabled { get; set; } = false;
    public bool IsDefault { get; set; } = false;
}