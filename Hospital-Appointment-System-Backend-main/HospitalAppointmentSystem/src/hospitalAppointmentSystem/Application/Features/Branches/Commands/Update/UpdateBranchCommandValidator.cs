using FluentValidation;

namespace Application.Features.Branches.Commands.Update;

public class UpdateBranchCommandValidator : AbstractValidator<UpdateBranchCommand>
{
    public UpdateBranchCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty().WithMessage("Id deðeri boş olamaz");
        RuleFor(c => c.Name).NotEmpty().WithMessage("ısim alanı boş olamaz").MinimumLength(5).WithMessage("ısim alanı minimum 5 karakter olmalı.");
    }
}