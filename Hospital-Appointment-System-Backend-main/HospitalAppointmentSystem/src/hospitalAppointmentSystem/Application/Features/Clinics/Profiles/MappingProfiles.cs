using Application.Features.Clinics.Commands.Create;
using Application.Features.Clinics.Commands.Delete;
using Application.Features.Clinics.Commands.Update;
using Application.Features.Clinics.Queries.GetList;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Features.Clinics.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreateClinicCommand, Clinic>();
        CreateMap<Clinic, CreatedClinicResponse>();

        CreateMap<UpdateClinicCommand, Clinic>();
        CreateMap<Clinic, UpdatedClinicResponse>();

        CreateMap<DeleteClinicCommand, Clinic>();
        CreateMap<Clinic, DeletedClinicResponse>();

        CreateMap<Clinic, GetListClinicListItemDto>();
        CreateMap<IPaginate<Clinic>, GetListResponse<GetListClinicListItemDto>>();
    }
}