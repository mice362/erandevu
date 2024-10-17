﻿using Application.Features.Doctors.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using static Application.Features.Appointments.Constants.AppointmentsOperationClaims;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Patients.Constants;
using Application.Services.Encryptions;

namespace Application.Features.Appointments.Queries.GetListByDoctorDate;
public class GetListByDoctorDateQuery : IRequest<GetListResponse<GetListByDoctorDateDto>>, ISecuredRequest

{
    public PageRequest PageRequest { get; set; }
    public Guid DoctorId { get; set; }

    public DateOnly Date { get; set; }

    public string[] Roles => [Admin, Read, PatientsOperationClaims.Update];

    public bool BypassCache { get; }
    public string? CacheKey => $"GetListAppointments({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetAppointments";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListByDoctorDateQueryHandler : IRequestHandler<GetListByDoctorDateQuery, GetListResponse<GetListByDoctorDateDto>>
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IMapper _mapper;

        public GetListByDoctorDateQueryHandler(IAppointmentRepository appointmentRepository, IMapper mapper)
        {
            _appointmentRepository = appointmentRepository;
            _mapper = mapper;
        }


        public async Task<GetListResponse<GetListByDoctorDateDto>> Handle(
            GetListByDoctorDateQuery request,
            CancellationToken cancellationToken
        )
        {
            IPaginate<Appointment> appointments = await _appointmentRepository.GetListAsync(
               index: request.PageRequest.PageIndex,
               size: request.PageRequest.PageSize,
               cancellationToken: cancellationToken,
                  orderBy: x => x.OrderByDescending(y => y.Date),
       
                  predicate: x => x.DoctorID == request.DoctorId && x.Date ==request.Date &&x.DeletedDate==null
           );



            GetListResponse<GetListByDoctorDateDto> response = _mapper.Map<GetListResponse<GetListByDoctorDateDto>>(appointments);
            return response;

        }

    }
    }


