using Election.Api.EntityFramework.Entities;
using Election.Api.EntityFramework.GenericRepository;
using Election.Api.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Election.Api.Handlers.Election.GetWinner;

public sealed class GetWinnerRequestHandler(IRepository<ElectionEntity> repository) : IRequestHandler<GetWinnerRequest, ServiceResponse<int>>
{
    public async Task<ServiceResponse<int>> Handle(GetWinnerRequest request, CancellationToken cancellationToken)
    {
        ElectionEntity? dbRecord = await repository.Entities
            .Include(_ => _.Candidates)
                .ThenInclude(_ => _.Votes)
            .FirstOrDefaultAsync(_ => _.Id == request.ElectionId);

        if (dbRecord is null)
        {
            return ServiceResponse<int>.Error($"Election with id {request.ElectionId} not found");
        }

        if (!dbRecord.Candidates.Any())
        {
            return ServiceResponse<int>.Error($"No one winner");
        }

        if (dbRecord.StartedOn is null)
        {
            return ServiceResponse<int>.Error("Election have not started yet");
        }

        if (dbRecord.EndedOn < DateTime.Now)
        {
            return ServiceResponse<int>.Error("Election already finished");
        }

        int winnerId = dbRecord.Candidates
            .OrderByDescending(_ => _.Votes.Count())
            .First()
            .Id;

        return ServiceResponse<int>.Ok(winnerId);
    }
}