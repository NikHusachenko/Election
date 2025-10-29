using Election.Api.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Election.Api.EntityFramework.Configurations;

public sealed class CandidateConfiguration : IEntityTypeConfiguration<CandidateEntity>
{
    public void Configure(EntityTypeBuilder<CandidateEntity> builder)
    {
        builder.HasKey(candidate => candidate.Id);

        builder.HasOne<ElectionEntity>(candidate => candidate.Election)
            .WithMany(election => election.Candidates)
            .HasForeignKey(candidate => candidate.ElectionId);

        builder.HasData(
        [
            new CandidateEntity()
            {
                Id = 1,
                ElectionId = 1,
                Name = "NVIDIA",
            },
            new CandidateEntity()
            {
                Id = 2,
                ElectionId = 1,
                Name = "SpaceX",
            },
            new CandidateEntity()
            {
                Id = 3,
                ElectionId = 1,
                Name = "OpenAI",
            },
            new CandidateEntity()
            {
                Id = 4,
                ElectionId = 1,
                Name = "Intel",
            },
            new CandidateEntity()
            {
                Id = 5,
                ElectionId = 1,
                Name = "Xiaomi",
            },
        ]);
    }
}