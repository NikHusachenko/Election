using Election.Api.EntityFramework.Entities;
using Election.Api.EntityFramework.GenericRepository;
using Election.Api.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Election.Api.Handlers.Election.Start;

public sealed class StartElectionRequestHandler(IRepository<ElectionEntity> repository,
    ILogger<StartElectionRequestHandler> logger)
    : IRequestHandler<StartElectionRequest, ServiceResponse>
{
    public async Task<ServiceResponse> Handle(StartElectionRequest request, CancellationToken cancellationToken)
    {
        ElectionEntity? dbRecord = await repository.Entities.FirstOrDefaultAsync(_ => _.Id == request.Id);
        if (dbRecord is null)
        {
            return ServiceResponse.Error($"Election with id {request.Id} not found");
        }

        dbRecord.StartedOn = DateTime.Now;
        dbRecord.EndedOn = DateTime.Now.AddDays(7);

        try
        {
            await repository.Update(dbRecord);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"StartElectionRequestHandler -> Handle -> ex: {ex.Message}");
            return ServiceResponse.Error(ex.Message);
        }
        return ServiceResponse.Ok();
    }
}