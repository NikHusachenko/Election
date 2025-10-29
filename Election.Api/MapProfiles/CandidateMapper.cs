using AutoMapper;
using Election.Api.EntityFramework.Entities;
using Election.Api.Handlers.Candidate.Create;
using Election.Api.Handlers.Election.GetCandidates;

namespace Election.Api.MapProfiles;

public sealed class CandidateMapper : Profile
{
    public CandidateMapper()
    {
        CreateMap<CreateCandidateDto, CandidateEntity>()
            .ForMember(dest => dest.Name, src => src.MapFrom(_ => _.Name));

        CreateMap<CandidateEntity, CandidateDto>()
            .ForMember(dest => dest.Id, src => src.MapFrom(_ => _.Id))
            .ForMember(dest => dest.Name, src => src.MapFrom(_ => _.Name))
            .ForMember(dest => dest.Votes, src => src.MapFrom(_ => _.Votes.Any() ? _.Votes.Count() : 0));
    }
}