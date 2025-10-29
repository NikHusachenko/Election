using Election.Api.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Election.Api.EntityFramework.Configurations;

public sealed class ElectionConfiguration : IEntityTypeConfiguration<ElectionEntity>
{
    public void Configure(EntityTypeBuilder<ElectionEntity> builder)
    {
        builder.HasKey(election => election.Id);

        builder.HasData(
        [
            new ElectionEntity()
            {
                Id = 1,
                Name = "Best technology company 2025",
                StartedOn = DateTime.Now,
                EndedOn = DateTime.Now.AddDays(7)
            }
        ]);
    }
}