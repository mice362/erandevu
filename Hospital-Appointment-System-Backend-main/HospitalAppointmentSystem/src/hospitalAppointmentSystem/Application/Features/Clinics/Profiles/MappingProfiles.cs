using Application.Features.Doctors.Commands.Create;
using Application.Features.Doctors.Commands.Delete;
using Application.Features.Doctors.Commands.Update;
using Application.Features.Doctors.Queries.GetById;
using Application.Features.Doctors.Queries.GetList;
using AutoMapper;
using NArchitecture.Core.Application.Responses;
using Domain.Entities;
using NArchitecture.Core.Persistence.Paging;
using Application.Features.DoctorSchedules.Queries.GetListByDoctorId;
using Application.Features.Doctors.Queries.GetListByBranchId;
using Application.Features.Clinics.Commands.Create;
using Application.Features.Clinics.Commands.Update;
using Application.Features.Clinics.Commands.Delete;
using Application.Features.Clinics.Queries.GetList;

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