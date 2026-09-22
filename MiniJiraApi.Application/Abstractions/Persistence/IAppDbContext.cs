using Microsoft.EntityFrameworkCore;
using MiniJiraApi.Domain.Entities;

namespace MiniJiraApi.Application.Abstractions.Persistence;

public interface IAppDbContext
{
    DbSet<User> Users { get; }
    DbSet<Board> Boards { get; }
    DbSet<Column> Columns { get; }
    DbSet<Card> Cards { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}