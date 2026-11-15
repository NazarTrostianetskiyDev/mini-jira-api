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
    private readonly ITokenService _tokenService;

    public AuthService(IAppDbContext dbContext, IPasswordHasher passwordHasher, ITokenService tokenService)
    {
        _dbContext = dbContext;
        _passwordHasher = passwordHasher;
        _tokenService = tokenService;
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

    public async Task<AccessToken> LoginAsync(LoginRequestDto request, CancellationToken cancellationToken)
    {
        string normalizedEmail = request.Email.Trim().ToLowerInvariant();
        
        var user = await _dbContext.Users
            .FirstOrDefaultAsync(user => user.Email == normalizedEmail, cancellationToken);

        if (user is null || !_passwordHasher.VerifyPasswordHash(request.Password, user.PasswordHash))
        {
            throw new UnauthorizedException("Invalid email or password");
        }
        
        return _tokenService.CreateAccessToken(user.Id, user.Email);
    }
}