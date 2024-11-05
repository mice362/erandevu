using FluentValidation;

namespace Application.Features.Notifications.Commands.Update;

public class UpdateNotificationCommandValidator : AbstractValidator<UpdateNotificationCommand>
{
    public UpdateNotificationCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty().WithMessage("Id alanı boş olamaz");

        RuleFor(c => c.AppointmentID)
            .NotEmpty().WithMessage("Randevu ID alanı boş olamaz.");

        RuleFor(c => c.Message)
            .NotEmpty().WithMessage("Mesaj alanı boş olamaz.");

        RuleFor(c => c.EmailStatus)
            .NotEmpty().WithMessage("E-posta durumu alanı boş olamaz.");

        RuleFor(c => c.SmsStatus)
            .NotEmpty().WithMessage("SMS durumu alanı boş olamaz.");
    }
}