using Election.Api.Services;
using MediatR;

namespace Election.Api.Handlers.Candidate.Create;

public sealed record CreateCandidateRequest(CreateCandidateDto Dto) : IRequest<ServiceResponse<int>>;