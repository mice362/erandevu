using FluentValidation;

namespace Application.Features.DoctorSchedules.Commands.Delete;

public class DeleteDoctorScheduleCommandValidator : AbstractValidator<DeleteDoctorScheduleCommand>
{
    public DeleteDoctorScheduleCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty().WithMessage("Id alan� bo� olamaz");
    }
}