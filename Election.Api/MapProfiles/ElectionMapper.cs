using AutoMapper;
using Election.Api.EntityFramework.Entities;
using Election.Api.Handlers.Election.Create;
using Election.Api.Handlers.Election.Get;
using Election.Api.Handlers.Election.GetById;

namespace Election.Api.MapProfiles;

public sealed class ElectionMapper : Profile
{
    public ElectionMapper()
    {
        CreateMap<ElectionEntity, ElectionDto>()
            .ForMember(dest => dest.Id, src => src.MapFrom(_ => _.Id))
            .ForMember(dest => dest.Name, src => src.MapFrom(_ => _.Name));

        CreateMap<CreateElectionDto, ElectionEntity>()
            .ForMember(dest => dest.Name, src => src.MapFrom(dest => dest.Name));

        CreateMap<ElectionEntity, ElectionDetailsDto>()
            .ForMember(dest => dest.Id, src => src.MapFrom(_ => _.Id))
            .ForMember(dest => dest.Name, src => src.MapFrom(_ => _.Name))
            .ForMember(dest => dest.StartedOn, src => src.MapFrom(_ => _.StartedOn))
            .ForMember(dest => dest.EndedOn, src => src.MapFrom(_ => _.EndedOn))
            .ForMember(dest => dest.Candidates, src => src.MapFrom(_ => _.Candidates));
    }
}