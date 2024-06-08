using System.ComponentModel.DataAnnotations;

namespace FSH.WebApi.Infrastructure.Auth.Jwt;

public class JwtSettings : IValidatableObject
{
    public List<JwtAuthority> JwtAuthorities { get; set; } = new List<JwtAuthority>();
    public string Key { get; set; } = string.Empty;

    public int TokenExpirationInMinutes { get; set; }

    public int RefreshTokenExpirationInDays { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (string.IsNullOrEmpty(Key))
        {
            yield return new ValidationResult("No Key defined in JwtSettings config", new[] { nameof(Key) });
        }
    }
}

public class JwtAuthority
{
    public string Name { get; set; }
    public string Issuer { get; set; }
}