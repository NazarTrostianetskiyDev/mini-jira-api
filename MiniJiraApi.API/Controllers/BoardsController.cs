using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniJiraApi.Application.Boards;

namespace MiniJiraApi.API.Controllers;

[Authorize]
[ApiController]
[Route("api/v1/boards")]
public sealed class BoardsController : ControllerBase
{
    private readonly BoardService _boardService;

    public BoardsController(BoardService boardService)
    {
        _boardService = boardService;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<BoardDto>>> GetAll()
    {
        var boards = await _boardService.GetAllAsync();

        return Ok(boards);
    }
}