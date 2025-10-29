using AutoMapper;
using Election.Api.EntityFramework.Entities;
using Election.Api.EntityFramework.GenericRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Election.Api.Handlers.Election.GetCandidates;

public sealed class GetCandidatesRequestHandler(IMapper mapper, IRepository<CandidateEntity> repository) : IRequestHandler<GetCandidatesRequest, IEnumerable<CandidateDto>>
{
    public async Task<IEnumerable<CandidateDto>> Handle(GetCandidatesRequest request, CancellationToken cancellationToken)
    {
        List<CandidateEntity> dbRecords = await repository.Entities.ToListAsync();
        return dbRecords.Select(mapper.Map<CandidateDto>);
    }
}