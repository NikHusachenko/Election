using Election.Api.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Election.Api.EntityFramework.Configurations;

public sealed class VoteConfiguration : IEntityTypeConfiguration<VoteEntity>
{
    public void Configure(EntityTypeBuilder<VoteEntity> builder)
    {
        builder.HasKey(vote => vote.Id);

        builder.HasOne<CandidateEntity>(vote => vote.Candidate)
            .WithMany(candidate => candidate.Votes)
            .HasForeignKey(vote => vote.CandidateId);
    }
}