using System.ComponentModel.DataAnnotations;

namespace MiniJiraApi.Application.Authentication;

public sealed record LoginRequestDto(
    [Required]
    [EmailAddress]
    [StringLength(256)]
    string Email,

    [Required]
    [MaxLength(128)]
    string Password);
