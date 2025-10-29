using Election.Api.Handlers.Election.Create;
using Election.Api.Handlers.Election.Get;
using Election.Api.Handlers.Election.GetById;
using Election.Api.Handlers.Election.GetCandidates;
using Election.Api.Handlers.Election.GetWinner;
using Election.Api.Handlers.Election.Start;
using Election.Api.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Election.Api.Controllers;

[ApiController]
[Route("api/elections")]
public sealed class ElectionsController(IMediator mediator) : ApiControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        IEnumerable<ElectionDto> elections = await mediator.Send(new GetElectionsRequest());
        return MapResponse(elections);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        ServiceResponse<ElectionDetailsDto> response = await mediator.Send(new GetElectionRequest(id));
        return MapResponse(response);
    }

    [HttpGet("{id:int}/candidates")]
    public async Task<IActionResult> GetCandidates([FromRoute] int id)
    {
        IEnumerable<CandidateDto> candidates = await mediator.Send(new GetCandidatesRequest(id));
        return MapResponse(candidates);
    }

    [HttpGet("{id:int}/winner")]
    public async Task<IActionResult> GetWinner([FromRoute] int id)
    {
        ServiceResponse<int> response = await mediator.Send(new GetWinnerRequest(id));
        return MapResponse(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateElectionDto dto)
    {
        ServiceResponse<int> response = await mediator.Send(new CreateElectionRequest(dto));
        return MapResponse(response);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Start([FromQuery] int id)
    {
        ServiceResponse response = await mediator.Send(new StartElectionRequest(id));
        return MapResponse(response);
    }
}