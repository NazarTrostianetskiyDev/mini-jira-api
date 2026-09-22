namespace MiniJiraApi.Application.Boards;

public sealed record BoardDto(
    Guid Id,
    string Title,
    string? Description,
    Guid OwnerId);