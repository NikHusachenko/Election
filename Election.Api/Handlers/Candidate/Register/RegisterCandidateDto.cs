namespace Election.Api.Handlers.Candidate.Register;

public sealed class RegisterCandidateDto
{
    public int ElectionId { get; set; }
    public int CandidateId { get; set; }
}