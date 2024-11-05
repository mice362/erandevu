using FluentValidation;

namespace Application.Features.Branches.Commands.Create;

public class CreateBranchCommandValidator : AbstractValidator<CreateBranchCommand>
{
    public CreateBranchCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty().WithMessage("ısim alanı boş olamaz.");
        RuleFor(c => c.Name).MinimumLength(5).WithMessage("ısim alanı minimum 5 karakter olmalı.");
    }
}