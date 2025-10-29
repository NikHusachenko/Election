using AutoMapper;
using Election.Api.EntityFramework.Entities;
using Election.Api.EntityFramework.GenericRepository;
using Election.Api.Services;
using MediatR;

namespace Election.Api.Handlers.Election.Create;

public sealed class CreateElectionRequestHandler(IRepository<ElectionEntity> repository,
    ILogger<CreateElectionRequestHandler> logger,
    IMapper mapper)
    : IRequestHandler<CreateElectionRequest, ServiceResponse<int>>
{
    public async Task<ServiceResponse<int>> Handle(CreateElectionRequest request, CancellationToken cancellationToken)
    {
        try
        {
            int id = await repository.Add(mapper.Map<ElectionEntity>(request));
            return ServiceResponse<int>.Ok(id);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"CreateElectionRequestHandler -> Handle -> ex: {ex.Message}");
            return ServiceResponse<int>.Error(ex.Message);
        }
    }
}