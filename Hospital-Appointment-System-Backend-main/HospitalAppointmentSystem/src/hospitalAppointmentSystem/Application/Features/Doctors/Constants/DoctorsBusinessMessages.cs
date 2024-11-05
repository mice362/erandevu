namespace Application.Features.Doctors.Constants;

public static class DoctorsBusinessMessages
{
    public const string SectionName = "Doctor";

    public const string DoctorNotExists = "Böyle bir doktor bulunamadı";

    public const string UserIdentityAlreadyExists = "Böyle bir kimlik numarası zaten var";

    public const string HasFutureAppointments = "Doktorun ileri tarihlerde randevuları bulunduðundan silinemez";

    public const string InvalidIdentity = "Geçersiz TC kimlik numarası veya kimlik bilgileri";
}