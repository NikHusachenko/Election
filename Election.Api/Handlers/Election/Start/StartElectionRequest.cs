using Election.Api.Services;
using MediatR;

namespace Election.Api.Handlers.Election.Start;

public sealed record StartElectionRequest(int Id) : IRequest<ServiceResponse>;