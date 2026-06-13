using MiniJiraApi.Domain.Common;
using MiniJiraApi.Domain.Enums;

namespace MiniJiraApi.Domain.Entities;

public sealed class Card : BaseEntity
{
    public string Title { get; private set; } = default!;
    public string? Description { get; private set; }
    public int Order { get; private set; }
    public CardPriority Priority { get; private set; }
    public DateTime? DueDate { get; private set; }
    public Guid ColumnId { get; private set; }
    public Column Column { get; private set; } = default!;
    public Guid? AssigneeId { get; private set; }
    public User? Assignee { get; private set; }

    private Card() { }

    public static Card Create(
        string title,
        Guid columnId,
        int order,
        string? description = null,
        CardPriority priority = CardPriority.Medium,
        DateTime? dueDate = null,
        Guid? assigneeId = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(title);

        return new Card
        {
            Title = title,
            ColumnId = columnId,
            Order = order,
            Description = description,
            Priority = priority,
            DueDate = dueDate,
            AssigneeId = assigneeId
        };
    }

    public void Move(Guid targetColumnId, int newOrder)
    {
        ColumnId = targetColumnId;
        Order = newOrder;
        SetUpdatedAt();
    }

    public void Update(string title, string? description, CardPriority priority, DateTime? dueDate)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(title);
        Title = title;
        Description = description;
        Priority = priority;
        DueDate = dueDate;
        SetUpdatedAt();
    }

    public void Assign(Guid? userId)
    {
        AssigneeId = userId;
        SetUpdatedAt();
    }
}