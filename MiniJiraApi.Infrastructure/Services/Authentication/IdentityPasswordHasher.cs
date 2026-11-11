using Microsoft.AspNetCore.Identity;
using MiniJiraApi.Application.Abstractions.Authentication;

namespace MiniJiraApi.Infrastructure.Services.Authentication;

public sealed class IdentityPasswordHasher : IPasswordHasher
{
    private readonly IPasswordHasher<object> _hasher = new PasswordHasher<object>();

    public string HashPassword(string password)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(password);

        return _hasher.HashPassword(new object(), password);
    }

    public bool VerifyPasswordHash(string providedPassword, string hashedPassword)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(providedPassword);
        ArgumentException.ThrowIfNullOrWhiteSpace(hashedPassword);

        var verificationResult = _hasher.VerifyHashedPassword(new object(), hashedPassword, providedPassword);

        if (verificationResult == PasswordVerificationResult.Failed)
        {
            return false;
        }

        return true;
    }
}