using FluentValidation;
using System.Text.RegularExpressions;

namespace Application.Features.Doctors.Commands.Update;

public class UpdateDoctorCommandValidator : AbstractValidator<UpdateDoctorCommand>
{
    public UpdateDoctorCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty().WithMessage("Id alanı boş olamaz");

        RuleFor(c => c.BranchID).NotEmpty().WithMessage("Branş alanı boş olamaz");

        RuleFor(c => c.Title)
            .NotEmpty().WithMessage("Uzmanlık alanı boş olamaz")
            .Length(2, 10).WithMessage("Uzmanlık alanı en az 2, en fazla 10 karakter olmalıdır.");

        RuleFor(c => c.SchoolName)
             .NotEmpty().WithMessage("Okul adı boş olamaz")
             .Length(3, 50).WithMessage("Okul adı en az 3, en fazla 50 karakter olabilir");

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


    }
    private bool StrongPassword(string value)
    {
        Regex strongPasswordRegex = new("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&.*-]).{8,}$", RegexOptions.Compiled);

        return strongPasswordRegex.IsMatch(value);
    }
}