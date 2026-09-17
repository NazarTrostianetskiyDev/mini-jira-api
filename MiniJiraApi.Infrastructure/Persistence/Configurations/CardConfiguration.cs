using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniJiraApi.Domain.Entities;

namespace MiniJiraApi.Infrastructure.Persistence.Configurations;

public sealed class CardConfiguration : IEntityTypeConfiguration<Card>
{
    public void Configure(EntityTypeBuilder<Card> builder)
    {
        builder.ToTable("cards");
        
        builder.HasKey(card => card.Id);
        
        builder.Property(card => card.Id)
            .ValueGeneratedNever();

        builder.Property(card => card.ColumnId)
            .IsRequired();
        
        builder.HasOne(card => card.Column)
            .WithMany(column => column.Cards)
            .HasForeignKey(card => card.ColumnId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(card => card.AssigneeId);
        
        builder.HasOne(card => card.Assignee)
            .WithMany()
            .HasForeignKey(card => card.AssigneeId)
            .OnDelete(DeleteBehavior.SetNull);
        
        builder.Property(card => card.Title)
            .IsRequired()
            .HasMaxLength(200);
        
        builder.Property(card => card.Description)
            .HasMaxLength(2000);

        builder.Property(card => card.Order)
            .IsRequired();

        builder.Property(card => card.Priority)
            .IsRequired();

        builder.Property(card => card.DueDate);
        
        builder.Property(card => card.CreatedAt)
            .IsRequired();

        builder.Property(card => card.UpdatedAt);
    }
}