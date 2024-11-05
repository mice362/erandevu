using FluentValidation;

namespace Application.Features.DoctorSchedules.Commands.Create;

public class CreateDoctorScheduleCommandValidator : AbstractValidator<CreateDoctorScheduleCommand>
{
    public CreateDoctorScheduleCommandValidator()
    {
        RuleFor(c => c.DoctorID).NotEmpty().WithMessage("Id alanı boş olamaz");

        RuleFor(c => c.Date)
            .NotEmpty().WithMessage("Tarih alanı boş olamaz.");

        RuleFor(c => c.StartTime)
            .NotEmpty().WithMessage("Başlangıç saati alanı boş olamaz.");

        RuleFor(c => c.EndTime)
            .NotEmpty().WithMessage("Bitiş saati alanı boş olamaz.");

    }
}