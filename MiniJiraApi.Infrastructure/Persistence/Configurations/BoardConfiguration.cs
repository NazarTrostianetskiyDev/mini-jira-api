using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniJiraApi.Domain.Entities;

namespace MiniJiraApi.Infrastructure.Persistence.Configurations;

public sealed class BoardConfiguration : IEntityTypeConfiguration<Board>
{
    public void Configure(EntityTypeBuilder<Board> builder)
    {
        builder.ToTable("boards");
        
        builder.HasKey(board => board.Id);
        
        builder.Property(board => board.Id)
            .ValueGeneratedNever();

        builder.Property(board => board.OwnerId)
            .IsRequired();
        
        builder.HasOne(board => board.Owner)
            .WithMany(user => user.Boards)
            .HasForeignKey(board => board.OwnerId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(board => board.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(board => board.Description)
            .HasMaxLength(1000);
        
        builder.Property(board => board.CreatedAt)
            .IsRequired();

        builder.Property(board => board.UpdatedAt);
    }
}