using Election.Api.Handlers.Candidate.Vote;
using Election.Api.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Election.Api.Controllers;

[Route("/api/candidates/{id:int}/vote")]
public sealed class VotesController(IMediator mediator) : ApiControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Vote([FromRoute] int id)
    {
        ServiceResponse<int> response = await mediator.Send(new VoteRequest(id));
        return MapResponse(response);
    }
}