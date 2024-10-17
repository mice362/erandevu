using FluentValidation;

namespace Application.Features.Feedbacks.Commands.Create;

public class CreateFeedbackCommandValidator : AbstractValidator<CreateFeedbackCommand>
{
    public CreateFeedbackCommandValidator()
    {
        RuleFor(c => c.UserID).NotEmpty().WithMessage("User Id alan� bo� olamaz");
        RuleFor(c => c.Text)
         .NotEmpty().WithMessage("Metin alan� bo� olamaz.")
         .Length(5, 500).WithMessage("Metin en az 5, en fazla 500 karakter olmal�d�r.");

    }
}