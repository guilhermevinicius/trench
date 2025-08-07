using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Trench.User.Persistence.Postgres.Mappings.User;

internal sealed class UserMapping 
    : IEntityTypeConfiguration<Domain.Aggregates.Users.Entities.User>
{
    public void Configure(EntityTypeBuilder<Domain.Aggregates.Users.Entities.User> builder)
    {
        builder.ToTable(nameof(User));
        
        builder.HasKey(x => x.Id);

        builder.HasIndex(x => x.Id)
            .IsUnique();
        
        builder.Property(x => x.Id);
        
        builder.Property(x => x.FirstName)
            .HasColumnType("varchar(100)")
            .IsRequired();
        
        builder.Property(x => x.LastName)
            .HasColumnType("varchar(100)")
            .IsRequired();
        
        builder.Property(x => x.Username)
            .HasColumnType("varchar(100)")
            .IsRequired();

        builder.HasIndex(x => x.Username)
            .IsUnique();

        builder.Property(x => x.Email)
            .HasColumnType("varchar(100)")
            .IsRequired();

        builder.HasIndex(x => x.Email)
            .IsUnique();

        builder.Property(x => x.IdentityId);

        builder.Property(x => x.Birthdate);

        builder.Property(x => x.Bio)
            .HasColumnType("text");

        builder.Property(x => x.IsActive);

        builder.Property(x => x.CreatedOnUtc)
            .IsRequired()
            .HasColumnType("timestamp with time zone");

        builder.Property(x => x.UpdateOnUtc)
            .HasColumnType("timestamp with time zone");
    }
}