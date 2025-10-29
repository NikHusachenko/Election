using Election.Api.Handlers.Candidate.Create;
using Election.Api.Handlers.Candidate.Register;
using Election.Api.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Election.Api.Controllers;

[Route("api/candidates")]
public sealed class CandidateController(IMediator mediator) : ApiControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCandidateDto dto)
    {
        ServiceResponse<int> response = await mediator.Send(new CreateCandidateRequest(dto));
        return MapResponse(response);
    }

    [HttpPut("{id:int}/register")]
    public async Task<IActionResult> Register([FromBody] RegisterCandidateDto dto)
    {
        ServiceResponse response = await mediator.Send(new RegisterCandidateReques(dto));
        return MapResponse(response);
    }
}