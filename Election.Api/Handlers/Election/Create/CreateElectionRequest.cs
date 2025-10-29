using Election.Api.Services;
using MediatR;

namespace Election.Api.Handlers.Election.Create;

public sealed record CreateElectionRequest(CreateElectionDto Dto) : IRequest<ServiceResponse<int>>;