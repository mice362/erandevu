using Application.Features.Auth.Constants;
using Application.Features.Doctors.Constants;
using Application.Services.AuthenticatorService;
using Application.Services.Encryptions;
using Application.Services.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using NArchitecture.Core.Security.Enums;
using NArchitecture.Core.Security.Hashing;
using TurkishCitizenIdValidator;

namespace Application.Features.Auth.Rules;

public class AuthBusinessRules : BaseBusinessRules
{
    private readonly IUserRepository _userRepository;
    private readonly IPatientRepository _patientRepository;
    private readonly ILocalizationService _localizationService;
    private readonly IAuthenticatorService _authenticatorService;

    public AuthBusinessRules(IUserRepository userRepository, ILocalizationService localizationService, IPatientRepository patientRepository, IAuthenticatorService authenticatorService)
    {
        _userRepository = userRepository;
        _localizationService = localizationService;
        _patientRepository = patientRepository;
        _authenticatorService = authenticatorService;
    }
    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, AuthMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task EmailAuthenticatorShouldBeExists(EmailAuthenticator? emailAuthenticator)
    {
        if (emailAuthenticator is null)
            await throwBusinessException(AuthMessages.EmailAuthenticatorDontExists);
    }

    public void ValidateNationalIdentityAndBirthYearWithMernis(string nationalIdentity, string firstName, string lastName, int birthYear)
    {
        bool isValid = new TurkishCitizenIdentity(long.Parse(nationalIdentity), firstName, lastName, birthYear).IsValid();
        if (!isValid)
        {
            throw new BusinessException(DoctorsBusinessMessages.InvalidIdentity);
        }
    }

    public async Task OtpAuthenticatorShouldBeExists(OtpAuthenticator? otpAuthenticator)
    {
        if (otpAuthenticator is null)
            await throwBusinessException(AuthMessages.OtpAuthenticatorDontExists);
    }

    public async Task OtpAuthenticatorThatVerifiedShouldNotBeExists(OtpAuthenticator? otpAuthenticator)
    {
        if (otpAuthenticator is not null && otpAuthenticator.IsVerified)
            await throwBusinessException(AuthMessages.AlreadyVerifiedOtpAuthenticatorIsExists);
    }

    public async Task EmailAuthenticatorActivationKeyShouldBeExists(EmailAuthenticator emailAuthenticator)
    {
        if (emailAuthenticator.ActivationKey is null)
            await throwBusinessException(AuthMessages.EmailActivationKeyDontExists);
    }
    public async Task EmailAuthenticatorActivationKeyShouldNotBeExpired(EmailAuthenticator emailAuthenticator)
    {
        if (emailAuthenticator.CreatedDate.AddMinutes(15) < DateTime.UtcNow)
            await throwBusinessException(AuthMessages.EmailActivationKeyExpired);
    }

    public async Task UserShouldBeExistsWhenSelected(User? user)
    {
        if (user == null)
            await throwBusinessException(AuthMessages.UserDontExists);
    }

    public async Task UserShouldNotBeHaveAuthenticator(User user)
    {
        if (user.AuthenticatorType != AuthenticatorType.None)
            await throwBusinessException(AuthMessages.UserHaveAlreadyAAuthenticator);
    }

    public async Task RefreshTokenShouldBeExists(RefreshToken? refreshToken)
    {
        if (refreshToken == null)
            await throwBusinessException(AuthMessages.RefreshDontExists);
    }

    public async Task RefreshTokenShouldBeActive(RefreshToken refreshToken)
    {
        if (refreshToken.RevokedDate != null && DateTime.UtcNow >= refreshToken.ExpirationDate)
            await throwBusinessException(AuthMessages.InvalidRefreshToken);
    }

    public async Task UserEmailShouldBeNotExists(string email)
    {
        bool doesExists = await _userRepository.AnyAsync(predicate: u => u.Email == email);
        if (doesExists)
            await throwBusinessException(AuthMessages.UserMailAlreadyExists);
    }

    public async Task UserPasswordShouldBeMatch(User user, string password)
    {
        if (!HashingHelper.VerifyPasswordHash(password, user!.PasswordHash, user.PasswordSalt))
            await throwBusinessException(AuthMessages.PasswordDontMatch);
    }
    public async Task CheckIfEmailVerifiedOrNot(User user)
    {
        if (user is not Doctor)
        {
            bool isEmailVerified = await _authenticatorService.IsEmailVerified(user!.Id);
            if (!isEmailVerified)
            {
                throw new BusinessException(AuthMessages.EmailActivationDontExist);
            }
        }
    }

    public string EncryptEmailForNonAdmin(string email)
    {
        if (email == "fatmabireltr@gmail.com")
        {
            return email;
        }
        else
        {
            return CryptoHelper.Encrypt(email);
        }
    }

    public async Task UserEmailShouldNotBeExpiredAuthenticator(string email)
    {
        // User ve EmailAuthenticators'� y�kle
        var user = await _userRepository.Query()
                                        .Include(u => u.EmailAuthenticators)
                                        .FirstOrDefaultAsync(u => u.Email == email && u.DeletedDate == null);

        if (user != null)
        {
            var emailAuthenticator = user.EmailAuthenticators?.FirstOrDefault();
            if (emailAuthenticator != null)
            {
                if (emailAuthenticator.CreatedDate.AddMinutes(15) < DateTime.UtcNow)
                {
                    await _userRepository.DeleteAuthenticatorCode(user.Id);
                    // �li�kili Patient kayd�n� manuel olarak sorgulay�n
                    var patient = await _patientRepository.GetAsync(p => p.Id == user.Id);
                    if (patient != null)
                    {
                        // Soft delete i�lemi i�in patient ve ili�kili di�er tablolardan veriyi i�aretle
                        patient.DeletedDate = DateTime.UtcNow;
                        await _patientRepository.UpdateAsync(patient);
                    }

                    // Kullan�c�y� soft delete ile i�aretleyin
                    user.DeletedDate = DateTime.UtcNow;
                    await _userRepository.UpdateAsync(user);
                }
            }
        }
    }




}

