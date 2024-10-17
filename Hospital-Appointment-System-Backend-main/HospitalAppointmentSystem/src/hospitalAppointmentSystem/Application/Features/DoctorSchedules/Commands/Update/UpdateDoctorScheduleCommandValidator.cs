using FluentValidation;

namespace Application.Features.DoctorSchedules.Commands.Update;

public class UpdateDoctorScheduleCommandValidator : AbstractValidator<UpdateDoctorScheduleCommand>
{
    public UpdateDoctorScheduleCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty().WithMessage("Id alan� bo� olamaz");

        RuleFor(c => c.DoctorID).NotEmpty().WithMessage("Doktor Id alan� bo� olamaz");

        RuleFor(c => c.Date)
            .NotEmpty().WithMessage("Tarih alan� bo� olamaz.");

        RuleFor(c => c.StartTime)
            .NotEmpty().WithMessage("Ba�lang�� saati alan� bo� olamaz.");

        RuleFor(c => c.EndTime)
            .NotEmpty().WithMessage("Biti� saati alan� bo� olamaz.");
    }
}