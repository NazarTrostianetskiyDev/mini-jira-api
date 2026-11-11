using Microsoft.AspNetCore.Mvc;
using MiniJiraApi.Application.Authentication;

namespace MiniJiraApi.API.Controllers;

[ApiController]
[Route("api/v1/auth")]

public sealed class AuthController : ControllerBase
{
    private readonly AuthService _authService;
    
    public  AuthController(AuthService authService)
    {
        _authService = authService;
    }
    
    [HttpPost("register")]
    public async Task<ActionResult<Guid>> Register(RegisterRequestDto request, CancellationToken cancellationToken)
    {
        var userId = await _authService.RegisterAsync(request, cancellationToken);
        
        return StatusCode(StatusCodes.Status201Created, userId);
    }
}