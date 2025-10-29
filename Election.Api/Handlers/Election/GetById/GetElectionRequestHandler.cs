using AutoMapper;
using Election.Api.EntityFramework.Entities;
using Election.Api.EntityFramework.GenericRepository;
using Election.Api.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Election.Api.Handlers.Election.GetById;

public sealed class GetElectionRequestHandler(IMapper mapper,
    IRepository<ElectionEntity> repository)
    : IRequestHandler<GetElectionRequest, ServiceResponse<ElectionDetailsDto>>
{
    public async Task<ServiceResponse<ElectionDetailsDto>> Handle(GetElectionRequest request, CancellationToken cancellationToken)
    {
        ElectionEntity? dbRecord = await repository.Entities
            .Include(_ => _.Candidates)
                .ThenInclude(_ => _.Votes)
            .FirstOrDefaultAsync(_ => _.Id == request.Id);

        if (dbRecord is null)
        {
            return ServiceResponse<ElectionDetailsDto>.Error($"Election with id {request.Id} not found");
        }

        return ServiceResponse<ElectionDetailsDto>.Ok(mapper.Map<ElectionDetailsDto>(dbRecord));
    }
}