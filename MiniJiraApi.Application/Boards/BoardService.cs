using Microsoft.EntityFrameworkCore;
using MiniJiraApi.Application.Abstractions.Persistence;

namespace MiniJiraApi.Application.Boards;

public sealed class BoardService
{
    private readonly IAppDbContext _dbContext;

    public BoardService(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyList<BoardDto>> GetAllAsync()
    {
        return await _dbContext.Boards
            .AsNoTracking()
            .Select(board => new BoardDto(
                board.Id,
                board.Title,
                board.Description,
                board.OwnerId))
            .ToListAsync();
    }
}