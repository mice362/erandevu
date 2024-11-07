using Application.Features.DoctorSchedules.Commands.Update;
using Application.Features.DoctorSchedules.Constants;
using Application.Services.Appointments;
using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;

namespace Application.Features.DoctorSchedules.Rules;

public class DoctorScheduleBusinessRules : BaseBusinessRules
{
    private readonly IDoctorScheduleRepository _doctorScheduleRepository;
    private readonly IAppointmentService _appointmentService;
    private readonly ILocalizationService _localizationService;

    public DoctorScheduleBusinessRules(IDoctorScheduleRepository doctorScheduleRepository, ILocalizationService localizationService,
       IAppointmentService appointmentService)
    {
        _doctorScheduleRepository = doctorScheduleRepository;
        _localizationService = localizationService;
        _appointmentService = appointmentService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, DoctorSchedulesBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task DoctorScheduleShouldExistWhenSelected(DoctorSchedule? doctorSchedule)
    {
        if (doctorSchedule == null)
            await throwBusinessException(DoctorSchedulesBusinessMessages.DoctorScheduleNotExists);
    }

    public async Task DoctorScheduleIdShouldExistWhenSelected(int id, CancellationToken cancellationToken)
    {
        DoctorSchedule? doctorSchedule = await _doctorScheduleRepository.GetAsync(
            predicate: ds => ds.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await DoctorScheduleShouldExistWhenSelected(doctorSchedule);
    }

    //o randevu seçilmişse hastalrdan silmesine izin vermemek için oluşturulan bir kural
    public async Task DoctorScheduleShouldNotBeDeletedIfAppointmentsExist(int doctorScheduleId, CancellationToken cancellationToken)
    {
        DoctorSchedule? doctorSchedule = await _doctorScheduleRepository.GetAsync(
            predicate: ds => ds.Id == doctorScheduleId && ds.DeletedDate == null,
            enableTracking: false,
            cancellationToken: cancellationToken
        );

        if (doctorSchedule == null)
            await throwBusinessException(DoctorSchedulesBusinessMessages.DoctorScheduleNotExists);

        var hasAppointments = await _appointmentService.GetAsync(
            predicate: a => a.DoctorID == doctorSchedule.DoctorID && a.Date == doctorSchedule.Date,
            cancellationToken: cancellationToken
        );

        if (hasAppointments != null)
            await throwBusinessException(DoctorSchedulesBusinessMessages.DoctorScheduleCannotBeDeletedDueToExistingAppointments);
    }


    public async Task CheckIfDoctorScheduleDateIsUniqueForDoctor(Guid doctorId, DateOnly date)
    {
        var existingSchedule = await _doctorScheduleRepository.GetAsync(ds => ds.DoctorID == doctorId && ds.Date == date && ds.DeletedDate == null);

        if (existingSchedule != null)
        {
            throw new BusinessException(DoctorSchedulesBusinessMessages.DoctorScheduleAlreadyExistsForThisDate);
        }
    }



    public async Task<DoctorSchedule?> CheckAndRetrieveSoftDeletedSchedule(Guid doctorId, DateOnly date)
    {
        return await _doctorScheduleRepository.GetAsync(ds => ds.DoctorID == doctorId && ds.Date == date && ds.DeletedDate != null);
    }

    //güncelleme işlemi için soft delete
    public async Task<DoctorSchedule> CheckIfDoctorScheduleExists(int doctorScheduleId, CancellationToken cancellationToken)
    {
        var existingSchedule = await _doctorScheduleRepository.GetAsync(
            predicate: ds => ds.Id == doctorScheduleId && ds.DeletedDate == null,
            enableTracking: false,
            cancellationToken: cancellationToken
        );

        if (existingSchedule == null)
        {
            throw new BusinessException(DoctorSchedulesBusinessMessages.DoctorScheduleNotExists);
        }
        return existingSchedule;

    }

    public async Task CheckIfDoctorScheduleDateIsAvailable(Guid doctorId, DateOnly date, int existingId)
    {
        var conflictingSchedule = await _doctorScheduleRepository.GetAsync(ds => ds.DoctorID == doctorId && ds.Date == date);

        if (conflictingSchedule != null && conflictingSchedule.Id != existingId)
        {
            if (conflictingSchedule.DeletedDate == null)
                throw new BusinessException("Bu doktorun belirtilen tarihteki programı zaten mevcut.");

        }
    }

    public async Task<Appointment> CheckIfAppointmentsExistOnDateDoctor(Guid doctorId, DateOnly currentDate)
    {
        var appointment = await _appointmentService.CheckIfAppointmentsExistOnDate(doctorId, currentDate);
        if (appointment != null)
        {
            throw new BusinessException(DoctorSchedulesBusinessMessages.CheckIfAppointmentsExistOnDate);
        }
        return appointment;


    }



    public async Task HandleConflictingSchedule(DoctorSchedule conflictingSchedule, DoctorSchedule existingSchedule, UpdateDoctorScheduleCommand request)
    {
        if (conflictingSchedule != null && conflictingSchedule.Id != request.Id)
        {
            if (conflictingSchedule.DeletedDate == null)
            {
                // Silinmemiş bir kayıtta çakışma var, hata fırlatalım
                throw new BusinessException(DoctorSchedulesBusinessMessages.DoctorScheduleAlreadyExistsForThisDate);
            }
            else
            {
                // Silinmiş bir kayıtta çakışma var, bu kaydı güncelleyelim
                conflictingSchedule.Date = request.Date;
                conflictingSchedule.StartTime = request.StartTime;
                conflictingSchedule.EndTime = request.EndTime;
                conflictingSchedule.UpdatedDate = null;
                conflictingSchedule.DeletedDate = null;
                await _doctorScheduleRepository.UpdateAsync(conflictingSchedule);

                // ıstek yapılan kaydı silindi olarak işaretleyelim
                existingSchedule.DeletedDate = DateTime.UtcNow;
                await _doctorScheduleRepository.UpdateAsync(existingSchedule);
            }
        }
    }

}