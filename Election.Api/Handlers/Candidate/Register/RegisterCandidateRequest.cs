using Election.Api.Services;
using MediatR;

namespace Election.Api.Handlers.Candidate.Register;

public sealed record RegisterCandidateReques(RegisterCandidateDto Dto) : IRequest<ServiceResponse>;