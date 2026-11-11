namespace MiniJiraApi.Application.Abstractions.Authentication;

public interface IPasswordHasher
{
    string HashPassword(string password);
    bool VerifyPasswordHash(string password, string hashedPassword);
}