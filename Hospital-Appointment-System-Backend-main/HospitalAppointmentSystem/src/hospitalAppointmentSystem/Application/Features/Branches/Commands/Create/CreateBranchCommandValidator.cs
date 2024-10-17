using FluentValidation;

namespace Application.Features.Branches.Commands.Create;

public class CreateBranchCommandValidator : AbstractValidator<CreateBranchCommand>
{
    public CreateBranchCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty().WithMessage("�sim alan� bo� olamaz.");
        RuleFor(c => c.Name).MinimumLength(5).WithMessage("�sim alan� minimum 5 karakter olmal�.");
    }
}