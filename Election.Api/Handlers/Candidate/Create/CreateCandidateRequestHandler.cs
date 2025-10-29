using AutoMapper;
using Election.Api.EntityFramework.Entities;
using Election.Api.EntityFramework.GenericRepository;
using Election.Api.Services;
using MediatR;

namespace Election.Api.Handlers.Candidate.Create;

public sealed class CreateCandidateRequestHandler(IMapper mapper,
    IRepository<CandidateEntity> repository,
    ILogger<CreateCandidateRequestHandler> logger)
    : IRequestHandler<CreateCandidateRequest, ServiceResponse<int>>
{
    public async Task<ServiceResponse<int>> Handle(CreateCandidateRequest request, CancellationToken cancellationToken)
    {
        try
        {
            int id = await repository.Add(mapper.Map<CandidateEntity>(request));
            return ServiceResponse<int>.Ok(id);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"CreateCandidateRequestHandler -> Handle -> ex: {ex.Message}");
            return ServiceResponse<int>.Error(ex.Message);
        }
    }
}