using MediatR;

namespace Election.Api.Handlers.Election.Get;

public sealed record GetElectionsRequest : IRequest<IEnumerable<ElectionDto>>;