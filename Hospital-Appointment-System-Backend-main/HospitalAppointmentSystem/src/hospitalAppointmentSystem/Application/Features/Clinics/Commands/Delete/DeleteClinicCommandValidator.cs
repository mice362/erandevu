using FluentValidation;

namespace Application.Features.Clinics.Commands.Delete;

public class DeleteClinicCommandValidator : AbstractValidator<DeleteClinicCommand>
{
    public DeleteClinicCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty().WithMessage("Id boş olamaz");
    }
}
