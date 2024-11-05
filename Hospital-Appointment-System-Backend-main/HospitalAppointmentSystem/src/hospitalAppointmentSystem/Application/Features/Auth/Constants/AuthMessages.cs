namespace Application.Features.Auth.Constants;

public static class AuthMessages
{
    public const string SectionName = "Auth";

    public const string EmailAuthenticatorDontExists = "Böyle bir e-posta doðrulayıcı yok";
    public const string OtpAuthenticatorDontExists = "Böyle bir OTP doðrulayıcı yok";
    public const string AlreadyVerifiedOtpAuthenticatorIsExists = "Bu OTP doðrulayıcı zaten doðrulandı";
    public const string EmailActivationKeyDontExists = "Böyle bir e-posta aktivasyon anahtarı yok";
    public const string UserDontExists = "Böyle bir kullanıcı bulunmamaktadır";
    public const string UserHaveAlreadyAAuthenticator = "Kullanıcının zaten bir doðrulayıcısı var";
    public const string RefreshDontExists = "Böyle bir yenileme yok";
    public const string InvalidRefreshToken = "Geçersiz yenileme belirteci";
    public const string UserMailAlreadyExists = "Böyle bir mail adresi zaten var";
    public const string InvalidIdentity = "Geçersiz TC kimlik numarası veya kimlik bilgileri";
    public const string PasswordDontMatch = "şifreler eşleşmiyor";
    public static string EmailActivationKeyExpired = "Aktivasyon kodunun süresi 15 dakikadır. Lütfen tekrar üye olun!";
    public static string EmailActivationDontExist = "E-posta doðrulaması yapılmamış. Lütfen e-posta hesabınızı doðrulayın";
}
