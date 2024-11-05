using FluentValidation;

namespace Application.Features.Appointments.Commands.Update;

public class UpdateAppointmentCommandValidator : AbstractValidator<UpdateAppointmentCommand>
{
    public UpdateAppointmentCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty().WithMessage("Id alanı boş bırakılamaz.");
        RuleFor(c => c.Date).NotEmpty().WithMessage("Tarih alanı boş bırakılamaz.");
        RuleFor(c => c.Time).NotEmpty().WithMessage("Saat alanı boş bırakılamaz.");
        RuleFor(c => c.Status).NotEmpty().WithMessage("Durum alanı boş bırakılamaz.");
        RuleFor(c => c.DoctorID).NotEmpty().WithMessage("Doktor Id alanı boş bırakılamaz.");
        RuleFor(c => c.PatientID).NotEmpty().WithMessage("Hasta Id alanı boş bırakılamaz.");

    }
}