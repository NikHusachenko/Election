using Election.Api.EntityFramework.Entities;
using Election.Api.EntityFramework.GenericRepository;
using Election.Api.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Election.Api.Handlers.Candidate.Register;

public sealed class RegisterCandidateRequesHandler(ILogger<RegisterCandidateRequesHandler> logger,
    IRepository<CandidateEntity> repository)
    : IRequestHandler<RegisterCandidateReques, ServiceResponse>
{
    public async Task<ServiceResponse> Handle(RegisterCandidateReques request, CancellationToken cancellationToken)
    {
        CandidateEntity? dbRecord = await repository.Entities.FirstOrDefaultAsync(_ => _.Id == request.Dto.CandidateId);
        if (dbRecord is null)
        {
            return ServiceResponse.Error($"Candidate with id {request.Dto.CandidateId} not found");
        }

        if (dbRecord.ElectionId is not null)
        {
            return ServiceResponse.Error($"Candidate already take part in election");
        }

        dbRecord.ElectionId = request.Dto.ElectionId;

        try
        {
            await repository.Update(dbRecord);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"RegisterCandidateRequesHandler -> Handle -> ex: {ex.Message}");
            return ServiceResponse.Error(ex.Message);
        }

        return ServiceResponse.Ok();
    }
}