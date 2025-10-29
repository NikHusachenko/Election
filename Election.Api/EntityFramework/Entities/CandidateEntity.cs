namespace Election.Api.EntityFramework.Entities;

public sealed class CandidateEntity : EntityBase
{
    public string Name { get; set; } = default!;

    public int? ElectionId { get; set; }
    public ElectionEntity Election { get; set; }

    public ICollection<VoteEntity> Votes { get; set; } = [];
}