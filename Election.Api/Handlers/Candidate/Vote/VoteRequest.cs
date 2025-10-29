using Election.Api.Services;
using MediatR;

namespace Election.Api.Handlers.Candidate.Vote;

public sealed record VoteRequest(int CandidateId) : IRequest<ServiceResponse<int>>;