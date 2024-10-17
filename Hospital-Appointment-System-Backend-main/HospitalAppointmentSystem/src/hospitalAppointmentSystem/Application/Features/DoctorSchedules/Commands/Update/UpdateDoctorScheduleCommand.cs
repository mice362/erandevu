using Application.Features.DoctorSchedules.Constants;
using Application.Features.DoctorSchedules.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.DoctorSchedules.Constants.DoctorSchedulesOperationClaims;
using Application.Features.Doctors.Constants;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using Application.Services.Appointments;
using Application.Services.DoctorSchedules;

namespace Application.Features.DoctorSchedules.Commands.Update
{
    public class UpdateDoctorScheduleCommand : IRequest<UpdatedDoctorScheduleResponse>, ISecuredRequest, ILoggableRequest, ITransactionalRequest
    {
        public int Id { get; set; }
        public Guid DoctorID { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }

        public string[] Roles => new[] { Admin, Write, DoctorSchedulesOperationClaims.Update, DoctorsOperationClaims.Update };

        public bool BypassCache { get; }
        public string? CacheKey { get; }
        public string[]? CacheGroupKey => new[] { "GetDoctorSchedules" };

        public class UpdateDoctorScheduleCommandHandler : IRequestHandler<UpdateDoctorScheduleCommand, UpdatedDoctorScheduleResponse>
        {
            private readonly IMapper _mapper;
            private readonly IDoctorScheduleRepository _doctorScheduleRepository;
            private readonly DoctorScheduleBusinessRules _doctorScheduleBusinessRules;
            private readonly IAppointmentService _appointmentService;

            public UpdateDoctorScheduleCommandHandler(IMapper mapper, IDoctorScheduleRepository doctorScheduleRepository,
                                             DoctorScheduleBusinessRules doctorScheduleBusinessRules,IAppointmentService appointmentService)
            {
                _mapper = mapper;
                _doctorScheduleRepository = doctorScheduleRepository;
                _doctorScheduleBusinessRules = doctorScheduleBusinessRules;
                _appointmentService = appointmentService;
            }

            public async Task<UpdatedDoctorScheduleResponse> Handle(UpdateDoctorScheduleCommand request, CancellationToken cancellationToken)
            {
                // �lk olarak g�ncellemek istedi�imiz mevcut kayd� alal�m
                var existingSchedule = await _doctorScheduleBusinessRules.CheckIfDoctorScheduleExists(request.Id, cancellationToken);



                var appointment = await _doctorScheduleBusinessRules.CheckIfAppointmentsExistOnDateDoctor(request.DoctorID, existingSchedule.Date );

                // G�ncellenmek istenen tarih ve doktor ID'si ile silinmi� bir kay�t var m� diye kontrol edelim
                var conflictingSchedule = await _doctorScheduleRepository.GetAsync(ds => ds.DoctorID == request.DoctorID && ds.Date == request.Date);

                await _doctorScheduleBusinessRules.HandleConflictingSchedule(conflictingSchedule, existingSchedule, request);

                // �ak��an bir kay�t yoksa mevcut kayd� g�ncelleyelim
                if (conflictingSchedule == null || conflictingSchedule.Id == request.Id)
                {
                    _mapper.Map(request, existingSchedule);
                    await _doctorScheduleRepository.UpdateAsync(existingSchedule);
                }

                // G�ncellenen veriyi response olarak d�nelim
                UpdatedDoctorScheduleResponse updatedResponse = _mapper.Map<UpdatedDoctorScheduleResponse>(existingSchedule);
                return updatedResponse;


            }
          
        }
    }
}
