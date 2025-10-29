namespace Election.Api.Handlers.Election.GetCandidates;

public sealed class CandidateDto
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public int Votes { get; set; }
}