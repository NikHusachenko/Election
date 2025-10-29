namespace Election.Api.EntityFramework.Entities;

public sealed class VoteEntity : EntityBase
{
    public DateTime VotedOn { get; set; }

    public int CandidateId { get; set; }
    public CandidateEntity Candidate { get; set; }
}