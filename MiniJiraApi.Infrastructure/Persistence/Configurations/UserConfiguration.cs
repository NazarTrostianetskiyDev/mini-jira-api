using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniJiraApi.Domain.Entities;

namespace MiniJiraApi.Infrastructure.Persistence.Configurations;

public sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");

        builder.HasKey(user => user.Id);

        builder.Property(user => user.Id)
            .ValueGeneratedNever();

        builder.Property(user => user.Email)
            .IsRequired()
            .HasMaxLength(256);

        builder.HasIndex(user => user.Email)
            .IsUnique();

        builder.Property(user => user.DisplayName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(user => user.PasswordHash)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(user => user.RefreshToken)
            .HasMaxLength(500);

        builder.Property(user => user.RefreshTokenExpiresAt);

        builder.Property(user => user.CreatedAt)
            .IsRequired();

        builder.Property(user => user.UpdatedAt);
    }
}