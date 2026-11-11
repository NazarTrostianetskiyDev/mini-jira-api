using Microsoft.EntityFrameworkCore;
using MiniJiraApi.Application.Abstractions.Authentication;
using MiniJiraApi.Application.Abstractions.Persistence;
using MiniJiraApi.Application.Common;
using MiniJiraApi.Domain.Entities;

namespace MiniJiraApi.Application.Authentication;

public sealed class AuthService
{
    private readonly IAppDbContext _dbContext;
    private readonly IPasswordHasher _passwordHasher;

    public AuthService(IAppDbContext dbContext, IPasswordHasher passwordHasher)
    {
        _dbContext = dbContext;
        _passwordHasher = passwordHasher;
    }
    
    public async Task<Guid> RegisterAsync(RegisterRequestDto request, CancellationToken cancellationToken)
    {
        string normalizedEmail = request.Email.Trim().ToLowerInvariant();
        
        bool isEmailDuplicate = await _dbContext.Users
            .AnyAsync(user => user.Email == normalizedEmail, cancellationToken);

        if (isEmailDuplicate)
        {
            throw new ConflictException("Email already exists");
        }
        
        var hashedPassword = _passwordHasher.HashPassword(request.Password);
        
        var user = User.Create(normalizedEmail, request.DisplayName, hashedPassword);

        _dbContext.Users.Add(user);
        
        await _dbContext.SaveChangesAsync(cancellationToken);
        
        return user.Id;
    }
}