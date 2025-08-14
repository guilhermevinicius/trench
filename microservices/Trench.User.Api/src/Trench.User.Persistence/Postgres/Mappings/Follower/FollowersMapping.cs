using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trench.User.Domain.Aggregates.Follower.Entities;

namespace Trench.User.Persistence.Postgres.Mappings.Follower;

internal sealed class FollowersMapping : IEntityTypeConfiguration<Followers>
{
    public void Configure(EntityTypeBuilder<Followers> builder)
    {
        builder.ToTable(nameof(Followers));

        builder.HasKey(x => x.Id);

        builder.HasIndex(x => x.Id)
            .IsUnique();

        builder.Property(x => x.Id);

        builder.Property(x => x.FollowerId)
            .IsRequired();

        builder.Property(x => x.FollowingId)
            .IsRequired();

        builder.Property(x => x.IsRequired)
            .IsRequired();

        builder.Property(x => x.Accepted)
            .IsRequired();

        builder.Property(x => x.CreatedOnUtc)
            .IsRequired();

        builder.Property(x => x.UpdateOnUtc)
            .IsRequired();
    }
}