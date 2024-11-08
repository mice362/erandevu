using FluentValidation;

namespace Application.Features.BusinessPartners.Commands.Delete;
public class DeleteBusinessPartnerCommandValidator : AbstractValidator<DeleteBusinessPartnerCommand>
{
    public DeleteBusinessPartnerCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty().WithMessage("Id boş olamaz");
    }
}
