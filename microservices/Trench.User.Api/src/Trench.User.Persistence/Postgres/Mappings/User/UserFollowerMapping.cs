using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trench.User.Domain.Aggregates.Users.Entities;

namespace Trench.User.Persistence.Postgres.Mappings.User;

internal sealed class UserFollowerMapping : IEntityTypeConfiguration<UserFollower>
{
    public void Configure(EntityTypeBuilder<UserFollower> builder)
    {
        builder.ToTable(nameof(UserFollower));
        
        builder.HasKey(x => x.Id);

        builder.HasIndex(x => new {x.Id, x.UserId, x.FollowerId})
            .IsUnique();

        builder.Property(x => x.Id);

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.FollowerId)
            .IsRequired();

        builder.HasOne(x => x.User)
            .WithMany(x => x.Followers)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}