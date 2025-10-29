namespace Election.Api.EntityFramework.Entities;

public sealed class ElectionEntity : EntityBase
{
    public string Name { get; set; } = default!;

    public DateTime? StartedOn { get; set; }
    public DateTime? EndedOn { get; set; }

    public ICollection<CandidateEntity> Candidates { get; set; } = [];
}