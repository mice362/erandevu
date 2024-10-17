using FluentValidation;

namespace Application.Features.Branches.Commands.Update;

public class UpdateBranchCommandValidator : AbstractValidator<UpdateBranchCommand>
{
    public UpdateBranchCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty().WithMessage("Id de�eri bo� olamaz");
        RuleFor(c => c.Name).NotEmpty().WithMessage("�sim alan� bo� olamaz").MinimumLength(5).WithMessage("�sim alan� minimum 5 karakter olmal�.");
    }
}