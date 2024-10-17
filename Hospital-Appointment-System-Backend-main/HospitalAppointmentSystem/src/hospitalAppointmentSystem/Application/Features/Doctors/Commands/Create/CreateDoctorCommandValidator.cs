using FluentValidation;
using System.Text.RegularExpressions;

namespace Application.Features.Doctors.Commands.Create;

public class CreateDoctorCommandValidator : AbstractValidator<CreateDoctorCommand>
{
    public CreateDoctorCommandValidator()
    {
        RuleFor(c => c.BranchID).NotEmpty().WithMessage("Bran� alan� bo� olamaz");

        RuleFor(c => c.Title)
            .NotEmpty().WithMessage("Uzmanl�k alan� bo� olamaz")
            .Length(2, 10).WithMessage("Uzmanl�k alan� en az 2, en fazla 10 karakter olmal�d�r.");


        RuleFor(c => c.SchoolName)
            .NotEmpty().WithMessage("Okul ad� bo� olamaz")
            .Length(3, 50).WithMessage("Okul ad� en az 3, en fazla 50 karakter olabilir");

        RuleFor(c => c.FirstName).NotEmpty().WithMessage("Kullan�c� ad� alan� bo� olamaz")
            .MinimumLength(2).WithMessage("Kullan�c� ad� en az 2 karakter olmal�d�r");

        RuleFor(c => c.LastName).NotEmpty().WithMessage("Kullan�c� soyad� alan� bo� olamaz")
            .MinimumLength(2).WithMessage("Kullan�c� soyad� en az 2 karakter olmal�d�r");

        RuleFor(c => c.DateOfBirth).NotEmpty().WithMessage("Do�um tarihi alan� bo� olamaz");

        RuleFor(c => c.NationalIdentity).NotEmpty().WithMessage("T.C. Kimlik numaras� alan� bo� olamaz").
            MinimumLength(11).WithMessage("T.C. Kimlik numaras� minimum 11 karakter olmal�d�r").MaximumLength(11).WithMessage("T.C. Kimlik numaras� alan� maksimum 11 karakter olmal�d�r");

        RuleFor(c => c.Email).NotEmpty().WithMessage("E-posta alan� bo� olamaz").EmailAddress().WithMessage("Girdi�iniz e-posta adresi istenen formatta de�il!");

        RuleFor(c => c.Phone).NotEmpty().WithMessage("Telefon numaras� alan� bo� olamaz").MinimumLength(11).WithMessage("Telefon numaras� minimum 11 karakter olmal�d�r");

        RuleFor(c => c.Address).NotEmpty().WithMessage("Adres alan� bo� olamaz").MinimumLength(3).WithMessage("Adres en az 3 karakter olmal�d�r");

        RuleFor(c => c.Password).NotEmpty().WithMessage("�ifre alan� bo� olamaz").MinimumLength(8).WithMessage("�ifre en az 8 karakter olmal�")
            .MaximumLength(15).WithMessage("�ifre en az 15 karakter olmal�").Must(StrongPassword).WithMessage(
                "�ifre en az bir b�y�k harf, bir k���k harf, bir rakam ve bir �zel karakter i�ermelidir."
            );
    }

    private bool StrongPassword(string value)
    {
        Regex strongPasswordRegex = new("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&.*-]).{8,}$", RegexOptions.Compiled);

        return strongPasswordRegex.IsMatch(value);
    }
}