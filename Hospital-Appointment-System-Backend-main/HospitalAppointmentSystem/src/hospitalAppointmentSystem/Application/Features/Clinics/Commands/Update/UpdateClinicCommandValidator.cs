using FluentValidation;

namespace Application.Features.Clinics.Commands.Update;
public class UpdateClinicCommandValidator : AbstractValidator<UpdateClinicCommand>
{
    public UpdateClinicCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty().WithMessage("Id değeri boş olamaz");
    }
}