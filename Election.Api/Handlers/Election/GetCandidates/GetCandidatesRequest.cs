using MediatR;

namespace Election.Api.Handlers.Election.GetCandidates;

public sealed record GetCandidatesRequest(int ElectionId) : IRequest<IEnumerable<CandidateDto>>;