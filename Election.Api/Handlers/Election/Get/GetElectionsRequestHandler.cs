using AutoMapper;
using Election.Api.EntityFramework.Entities;
using Election.Api.EntityFramework.GenericRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Election.Api.Handlers.Election.Get;

public sealed class GetElectionsRequestHandler(IRepository<ElectionEntity> repository, IMapper mapper)
    : IRequestHandler<GetElectionsRequest, IEnumerable<ElectionDto>>
{
    public async Task<IEnumerable<ElectionDto>> Handle(GetElectionsRequest request, CancellationToken cancellationToken)
    {
        List<ElectionEntity> elections = await repository.Entities.ToListAsync();
        return elections.Select(mapper.Map<ElectionDto>);
    }
}