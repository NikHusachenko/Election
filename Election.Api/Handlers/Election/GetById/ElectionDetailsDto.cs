using Election.Api.Handlers.Election.GetCandidates;

namespace Election.Api.Handlers.Election.GetById;

public sealed class ElectionDetailsDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime? StartedOn { get; set; }
    public DateTime? EndedOn { get; set; }
    public IEnumerable<CandidateDto> Candidates { get; set; } = [];
}