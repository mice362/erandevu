namespace Application.Features.Appointments.Constants;

public static class AppointmentsBusinessMessages
{
    public const string SectionName = "Appointment";

    public const string AppointmentNotExists = "Böyle bir randevu bulunmamaktadır";

    public const string PatientCannotHaveMultipleAppointmentsOnSameDayWithSameDoctor = "Bu doktor için aynı güne ait randevunuz zaten bulunmaktadır.";
}