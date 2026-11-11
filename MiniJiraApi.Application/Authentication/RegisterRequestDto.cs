using System.ComponentModel.DataAnnotations;

namespace MiniJiraApi.Application.Authentication;

public sealed record RegisterRequestDto(
    [Required]
    [EmailAddress]
    [StringLength(256)]
    string Email,
    
    [Required]
    [MinLength(8)]
    [MaxLength(128)]
    string Password,
    
    [Required]
    [StringLength(100)]
    string DisplayName);