namespace MiniJiraApi.Application.Abstractions.Authentication;

public interface ITokenService
{
    AccessToken CreateAccessToken(Guid userId, string email);
}