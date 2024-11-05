using FluentValidation;

namespace Application.Features.Reports.Commands.Update;

public class UpdateReportCommandValidator : AbstractValidator<UpdateReportCommand>
{
    public UpdateReportCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty().WithMessage("Id alanı boş olamaz");
        //RuleFor(c => c.AppointmentID).NotEmpty().WithMessage("Randevu Id alanı boş olamaz");
        RuleFor(c => c.Text)
            .NotEmpty().WithMessage("Metin boş olamaz")
            .MaximumLength(500).WithMessage("Metin en fazla 500 karakter olmalıdır");
    }
}