using FluentValidation;
using System.Text.RegularExpressions;

namespace Application.Features.Patients.Commands.Update;

public class UpdatePatientCommandValidator : AbstractValidator<UpdatePatientCommand>
{
    public UpdatePatientCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty().WithMessage("Id alanı boş olamaz");

        RuleFor(c => c.Age)
           .NotEmpty().WithMessage("Yaş alanı boş olamaz.");

        RuleFor(c => c.Height)
            .NotEmpty().WithMessage("Boy alanı boş olamaz.");

        RuleFor(c => c.Weight)
            .NotEmpty().WithMessage("Kilo alanı boş olamaz.");

        RuleFor(c => c.BloodGroup)
            .NotEmpty().WithMessage("Kan grubu alanı boş olamaz.");

        RuleFor(c => c.FirstName).NotEmpty().WithMessage("Kullanıcı adı alanı boş olamaz")
          .MinimumLength(2).WithMessage("Kullanıcı adı en az 2 karakter olmalıdır");

        RuleFor(c => c.LastName).NotEmpty().WithMessage("Kullanıcı soyadı alanı boş olamaz")
            .MinimumLength(2).WithMessage("Kullanıcı soyadı en az 2 karakter olmalıdır");

        RuleFor(c => c.DateOfBirth).NotEmpty().WithMessage("Doðum tarihi alanı boş olamaz");

        RuleFor(c => c.NationalIdentity).NotEmpty().WithMessage("T.C. Kimlik numarası alanı boş olamaz").
            MinimumLength(11).WithMessage("T.C. Kimlik numarası minimum 11 karakter olmalıdır").MaximumLength(11).WithMessage("T.C. Kimlik numarası alanı maksimum 11 karakter olmalıdır");

        RuleFor(c => c.Email).NotEmpty().WithMessage("E-posta alanı boş olamaz").EmailAddress().WithMessage("Girdiðiniz e-posta adresi istenen formatta deðil!");

        RuleFor(c => c.Phone).NotEmpty().WithMessage("Telefon numarası alanı boş olamaz").MinimumLength(11).WithMessage("Telefon numarası minimum 11 karakter olmalıdır");

        RuleFor(c => c.Address).NotEmpty().WithMessage("Adres alanı boş olamaz").MinimumLength(3).WithMessage("Adres en az 3 karakter olmalıdır");

        //RuleFor(c => c.Password).NotEmpty().WithMessage("şifre alanı boş olamaz").MinimumLength(8).WithMessage("şifre en az 8 karakter olmalı")
        //    .MaximumLength(15).WithMessage("şifre en az 15 karakter olmalı").Must(StrongPassword).WithMessage(
        //        "şifre en az bir büyük harf, bir küçük harf, bir rakam ve bir özel karakter içermelidir."
        //    );
    }
    private bool StrongPassword(string value)
    {
        Regex strongPasswordRegex = new("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&.*-]).{8,}$", RegexOptions.Compiled);

        return strongPasswordRegex.IsMatch(value);
    }
}