using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace MiniJiraApi.Infrastructure.Services.Authentication;

public sealed class JwtOptions
{
    public const string SectionName = "Jwt";
        
    [Required]
    public string Audience { get; init; } = string.Empty;
    
    [Required]
    public string Issuer { get; init; } = string.Empty;
    
    [Required]
    [Range(1, 1440)]
    public int ExpirationMinutes { get; init; }
    
    [Required]
    public string SecretKey { get; init; } = string.Empty;

    public SymmetricSecurityKey GetSigningKey()
    {
        return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
    }
}
