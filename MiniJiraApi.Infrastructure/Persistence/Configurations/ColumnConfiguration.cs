using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniJiraApi.Domain.Entities;

namespace MiniJiraApi.Infrastructure.Persistence.Configurations;

public sealed class ColumnConfiguration : IEntityTypeConfiguration<Column>
{
    public void Configure(EntityTypeBuilder<Column> builder)
    {
        builder.ToTable("columns");
        
        builder.HasKey(column => column.Id);

        builder.Property(column => column.Id)
            .ValueGeneratedNever();
        
        builder.Property(column => column.BoardId)
            .IsRequired();

        builder.HasOne(column => column.Board)
            .WithMany(board => board.Columns)
            .HasForeignKey(column => column.BoardId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(column => column.Title)
            .IsRequired()
            .HasMaxLength(200);
        
        builder.Property(column => column.Order)
            .IsRequired();
        
        builder.Property(column => column.CreatedAt)
            .IsRequired();

        builder.Property(column => column.UpdatedAt);
    }
}