using Application.Features.Branches.Commands.Update;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.BusinessPartners.Commands.Update;
public class UpdateBusinessPartnerCommandValidator : AbstractValidator<UpdateBusinessPartnerCommand>
{
    public UpdateBusinessPartnerCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty().WithMessage("Id değeri boş olamaz");
    }
}
