using MiniJiraApi.Domain.Common;

namespace MiniJiraApi.Domain.Entities;

public sealed class Board : BaseEntity
{
    public string Title { get; private set; } = default!;
    public string? Description { get; private set; }
    public Guid OwnerId { get; private set; }
    public User Owner { get; private set; } = default!;

    private readonly List<Column> _columns = [];
    public IReadOnlyCollection<Column> Columns => _columns.AsReadOnly();

    private Board() { }

    public static Board Create(string title, Guid ownerId, string? description = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(title);

        return new Board
        {
            Title = title,
            Description = description,
            OwnerId = ownerId
        };
    }

    public void Update(string title, string? description)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(title);
        Title = title;
        Description = description;
        SetUpdatedAt();
    }

    public void AddColumn(Column column)
    {
        ArgumentNullException.ThrowIfNull(column);
        _columns.Add(column);
    }
}