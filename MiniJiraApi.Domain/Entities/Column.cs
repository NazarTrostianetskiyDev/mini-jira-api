using MiniJiraApi.Domain.Common;

namespace MiniJiraApi.Domain.Entities;

public sealed class Column : BaseEntity
{
    public string Title { get; private set; } = default!;
    public int Order { get; private set; }
    public Guid BoardId { get; private set; }
    public Board Board { get; private set; } = default!;

    private readonly List<Card> _cards = [];
    public IReadOnlyCollection<Card> Cards => _cards.AsReadOnly();

    private Column() { }

    public static Column Create(string title, int order, Guid boardId)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(title);

        return new Column
        {
            Title = title,
            Order = order,
            BoardId = boardId
        };
    }

    public void Update(string title, int order)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(title);
        Title = title;
        Order = order;
        SetUpdatedAt();
    }
}