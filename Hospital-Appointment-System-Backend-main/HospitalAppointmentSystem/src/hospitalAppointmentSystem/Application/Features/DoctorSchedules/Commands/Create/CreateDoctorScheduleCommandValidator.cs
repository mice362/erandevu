using FluentValidation;

namespace Application.Features.DoctorSchedules.Commands.Create;

public class CreateDoctorScheduleCommandValidator : AbstractValidator<CreateDoctorScheduleCommand>
{
    public CreateDoctorScheduleCommandValidator()
    {
        RuleFor(c => c.DoctorID).NotEmpty().WithMessage("Id alan� bo� olamaz");

        RuleFor(c => c.Date)
            .NotEmpty().WithMessage("Tarih alan� bo� olamaz.");

        RuleFor(c => c.StartTime)
            .NotEmpty().WithMessage("Ba�lang�� saati alan� bo� olamaz.");

        RuleFor(c => c.EndTime)
            .NotEmpty().WithMessage("Biti� saati alan� bo� olamaz.");

    }
}