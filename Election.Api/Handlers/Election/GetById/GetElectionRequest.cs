using Election.Api.Services;
using MediatR;

namespace Election.Api.Handlers.Election.GetById;

public sealed record GetElectionRequest(int Id) : IRequest<ServiceResponse<ElectionDetailsDto>>;