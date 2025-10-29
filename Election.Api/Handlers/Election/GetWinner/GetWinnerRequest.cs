using Election.Api.Services;
using MediatR;

namespace Election.Api.Handlers.Election.GetWinner;

public sealed record GetWinnerRequest(int ElectionId) : IRequest<ServiceResponse<int>>;