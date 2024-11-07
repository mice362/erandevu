using Application.Features.Doctors.Constants;
using Application.Features.Patients.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using static Application.Features.DoctorSchedules.Constants.DoctorSchedulesOperationClaims;

namespace Application.Features.DoctorSchedules.Queries.GetListByDoctorId;
public class GetListByDoctorIdQuery : IRequest<GetListResponse<GetListByDoctorIdDto>>, ISecuredRequest
{
    public Guid DoctorId { get; set; }
    public PageRequest PageRequest { get; set; }

    public string[] Roles => [Admin, Read, DoctorsOperationClaims.Update, PatientsOperationClaims.Update]; //hastayı şuan ekledim bakıcam -merve

    public bool BypassCache { get; }
    public string? CacheKey => $"GetListDoctorSchedules({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetDoctorSchedules";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListByDoctorIdQueryHandler : IRequestHandler<GetListByDoctorIdQuery, GetListResponse<GetListByDoctorIdDto>>
    {
        private readonly IDoctorScheduleRepository _doctorScheduleRepository;
        private readonly IMapper _mapper;

        public GetListByDoctorIdQueryHandler(IDoctorScheduleRepository doctorScheduleRepository, IMapper mapper)
        {
            _doctorScheduleRepository = doctorScheduleRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListByDoctorIdDto>> Handle(GetListByDoctorIdQuery request, CancellationToken cancellationToken)
        {
            IPaginate<DoctorSchedule> doctorSchedules = await _doctorScheduleRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken,
                orderBy: x => x.OrderByDescending(x => x.Date),
                include: x => x.Include(x => x.Doctor),
                predicate: x => x.DoctorID == request.DoctorId && x.DeletedDate == null

            );

            GetListResponse<GetListByDoctorIdDto> response = _mapper.Map<GetListResponse<GetListByDoctorIdDto>>(doctorSchedules);
            return response;
        }
    }
}

