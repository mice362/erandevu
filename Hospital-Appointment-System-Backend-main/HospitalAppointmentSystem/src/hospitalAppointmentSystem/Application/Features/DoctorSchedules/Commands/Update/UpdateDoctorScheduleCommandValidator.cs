using FluentValidation;

namespace Application.Features.DoctorSchedules.Commands.Update;

public class UpdateDoctorScheduleCommandValidator : AbstractValidator<UpdateDoctorScheduleCommand>
{
    public UpdateDoctorScheduleCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty().WithMessage("Id alanı boş olamaz");

        RuleFor(c => c.DoctorID).NotEmpty().WithMessage("Doktor Id alanı boş olamaz");

        RuleFor(c => c.Date)
            .NotEmpty().WithMessage("Tarih alanı boş olamaz.");

        RuleFor(c => c.StartTime)
            .NotEmpty().WithMessage("Başlangıç saati alanı boş olamaz.");

        RuleFor(c => c.EndTime)
            .NotEmpty().WithMessage("Bitiş saati alanı boş olamaz.");
    }
}