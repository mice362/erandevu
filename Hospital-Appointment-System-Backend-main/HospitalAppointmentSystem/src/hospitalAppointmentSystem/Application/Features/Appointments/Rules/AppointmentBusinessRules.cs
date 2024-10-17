using Application.Features.Appointments.Constants;
using Application.Services.Repositories;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using Domain.Entities;
using Application.Services.Encryptions;
using MailKit.Security;
using MimeKit;
using MailKit.Net.Smtp;
using Org.BouncyCastle.Asn1.Ocsp;
using Application.Features.Appointments.Commands.Create;

namespace Application.Features.Appointments.Rules;

public class AppointmentBusinessRules : BaseBusinessRules
{
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly ILocalizationService _localizationService;

    public AppointmentBusinessRules(IAppointmentRepository appointmentRepository, ILocalizationService localizationService)
    {
        _appointmentRepository = appointmentRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, AppointmentsBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task AppointmentShouldExistWhenSelected(Appointment? appointment)
    {
        if (appointment == null)
            await throwBusinessException(AppointmentsBusinessMessages.AppointmentNotExists);
    }

    public async Task AppointmentIdShouldExistWhenSelected(int id, CancellationToken cancellationToken)
    {
        Appointment? appointment = await _appointmentRepository.GetAsync(
            predicate: a => a.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await AppointmentShouldExistWhenSelected(appointment);
    }

    public async Task<Appointment> CheckForExistingDeletedAppointment(CreateAppointmentCommand request,Appointment appointment)
    {
        var existingDeletedAppointment = await _appointmentRepository.GetAsync(a =>
            a.PatientID == request.PatientID &&
            a.DoctorID == request.DoctorID &&
            a.Date == request.Date &&
            a.DeletedDate != null);

        if (existingDeletedAppointment != null)
        {
            // Silinmi� randevuyu g�ncelle
            existingDeletedAppointment.Time = request.Time;
            existingDeletedAppointment.Status = request.Status;
            existingDeletedAppointment.DeletedDate = null; // Silinmi� durumu kald�r
            await _appointmentRepository.UpdateAsync(existingDeletedAppointment);

            return existingDeletedAppointment;

        }
        else
        {
            await _appointmentRepository.AddAsync(appointment);
            return appointment;
        }
    }

    public async Task PatientCannotHaveMultipleAppointmentsOnSameDayWithSameDoctor(Guid patientId, Guid doctorId, DateOnly date)
    {
        bool exists = await _appointmentRepository.AnyAsync(a => a.PatientID == patientId && a.DoctorID == doctorId && a.Date == date && a.DeletedDate==null);
        if (exists)
        {
            await throwBusinessException(AppointmentsBusinessMessages.PatientCannotHaveMultipleAppointmentsOnSameDayWithSameDoctor);
        }
    }

    public async Task SendAppointmentConfirmationMail(Appointment appointment)
    {
        // Mail i�eri�ini haz�rla
        var mailMessage = new MimeMessage();
        mailMessage.From.Add(new MailboxAddress("Pair 5 Hastanesi", "fatmabireltr@gmail.com")); // G�nderen bilgisi
        appointment.Patient.Email = CryptoHelper.Decrypt(appointment.Patient.Email);
        appointment.Patient.FirstName = CryptoHelper.Decrypt(appointment.Patient.FirstName);
        appointment.Patient.LastName = CryptoHelper.Decrypt(appointment.Patient.LastName);
        appointment.Doctor.FirstName = CryptoHelper.Decrypt(appointment.Doctor.FirstName);
        appointment.Doctor.LastName = CryptoHelper.Decrypt(appointment.Doctor.LastName);

        mailMessage.To.Add(new MailboxAddress("Pair 5 Hastanesi", appointment.Patient.Email)); // Al�c� bilgisi 
        mailMessage.Subject = "Randevu Bilgilendirme"; // Mail konusu

        // HTML ve CSS i�eri�i olu�tur
        var bodyBuilder = new BodyBuilder();
        bodyBuilder.HtmlBody = $@"
       <html>
        <head>
            <style>
                body {{ font-family: Arial, sans-serif; }}
                .container {{ border: 1px solid red; padding: 10px; }}
            </style>
        </head>
        <body>
            <div class='container'>
                <p>Say�n {appointment.Patient.FirstName} {appointment.Patient.LastName},</p>
                <p>{appointment.Date} tarihinde, saat {appointment.Time} i�in bir randevu ald�n�z.</p>
                <p>Doktor: {appointment.Doctor.Title} {appointment.Doctor.FirstName} {appointment.Doctor.LastName}</p>
                <p>Bran�: {appointment.Doctor.Branch.Name}</p>
            </div>
        </body>
        </html>";

        // MimeKit'e g�vdeyi ayarla
        mailMessage.Body = bodyBuilder.ToMessageBody();

        // SMTP ile ba�lant� kur ve maili g�nder
        using (var smtp = new SmtpClient())
        {
            smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("fatmabireltr@gmail.com", "rxuv hpfv wlqq htpa");
            await smtp.SendAsync(mailMessage);
            smtp.Disconnect(true);
        }
    }


    public async Task SendAppointmentConfirmationMailDelete(Appointment appointment)
    {

        // Mail i�eri�ini haz�rla
        var mailMessage = new MimeMessage();
        mailMessage.From.Add(new MailboxAddress("Pair 5 Hastanesi", "fatmabireltr@gmail.com")); // G�nderen bilgisi
        appointment.Patient.Email = CryptoHelper.Decrypt(appointment.Patient.Email);
        appointment.Patient.FirstName = CryptoHelper.Decrypt(appointment.Patient.FirstName);
        appointment.Patient.LastName = CryptoHelper.Decrypt(appointment.Patient.LastName);
        appointment.Doctor.FirstName = CryptoHelper.Decrypt(appointment.Doctor.FirstName);
        appointment.Doctor.LastName = CryptoHelper.Decrypt(appointment.Doctor.LastName);

        mailMessage.To.Add(new MailboxAddress("Pair 5 Hastanesi", appointment.Patient.Email)); // Al�c� bilgisi 
        mailMessage.Subject = "Randevu Bilgilendirme"; // Mail konusu

        // HTML ve CSS i�eri�i olu�tur
        var bodyBuilder = new BodyBuilder();
        bodyBuilder.HtmlBody = $@"
       <html>
        <head>
            <style>
                body {{ font-family: Arial, sans-serif; }}
                .container {{ border: 1px solid red; padding: 10px; }}
            </style>
        </head>
        <body>
            <div class='container'>
                <p>Say�n {appointment.Patient.FirstName} {appointment.Patient.LastName},</p>
                <p>{appointment.Date} tarihinde, saat {appointment.Time} olan randevunuz iptal edildi.</p>
                <p>Doktor:{appointment.Doctor.Title} {appointment.Doctor.FirstName} {appointment.Doctor.LastName}</p>
                <p>Bran�: {appointment.Doctor.Branch.Name}</p>
            </div>
        </body>
        </html>";

        // MimeKit'e g�vdeyi ayarla
        mailMessage.Body = bodyBuilder.ToMessageBody();

        // SMTP ile ba�lant� kur ve maili g�nder
        using (var smtp = new SmtpClient())
        {
            smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("fatmabireltr@gmail.com", "rxuv hpfv wlqq htpa");
            await smtp.SendAsync(mailMessage);
            smtp.Disconnect(true);
        }
    }
}