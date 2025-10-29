using Election.Api.EntityFramework.Entities;
using Election.Api.EntityFramework.GenericRepository;
using Election.Api.Hubs;
using Election.Api.Services;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace Election.Api.Handlers.Candidate.Vote;

public sealed class VoteRequestHandler(ILogger<VoteRequestHandler> logger,
    IRepository<CandidateEntity> candidateRepository,
    IRepository<VoteEntity> voteRepository,
    IHubContext<ElectionHub> hubContext)
    : IRequestHandler<VoteRequest, ServiceResponse<int>>
{
    public async Task<ServiceResponse<int>> Handle(VoteRequest request, CancellationToken cancellationToken)
    {
        CandidateEntity? dbRecord = await candidateRepository.Entities
            .Include(_ => _.Election)
            .FirstOrDefaultAsync(_ => _.Id == request.CandidateId);

        if (dbRecord is null)
        {
            return ServiceResponse<int>.Error($"Candidate not found");
        }

        if (dbRecord.ElectionId is null)
        {
            return ServiceResponse<int>.Error($"Candidate does not takes part in any elections");
        }

        if (dbRecord.Election.StartedOn is null)
        {
            return ServiceResponse<int>.Error($"Election have not started yet");
        }

        if (dbRecord.Election.EndedOn < DateTime.Now)
        {
            return ServiceResponse<int>.Error("Election already finished");
        }

        try
        {
            int id = await voteRepository.Add(new VoteEntity()
            {
                CandidateId = request.CandidateId,
                VotedOn = DateTime.Now,
            });

            await hubContext.Clients
                .Group(GetGroupId(request.CandidateId))
                .SendAsync(GetClientMethod(request.CandidateId), request.CandidateId);

            return ServiceResponse<int>.Ok(id);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"VoteRequestHandler -> Handle -> ex: {ex.Message}");
            return ServiceResponse<int>.Error(ex.Message);
        }
    }

    private string GetGroupId(int candidateId) => $"candidate_{candidateId}";
    private string GetClientMethod(int candidateId) => $"handleVoteChanged_{candidateId}";
}