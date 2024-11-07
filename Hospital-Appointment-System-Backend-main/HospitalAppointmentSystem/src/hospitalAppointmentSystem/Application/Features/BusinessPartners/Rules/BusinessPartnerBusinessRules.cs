using Application.Features.BusinessPartners.Constants;
using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;

namespace Application.Features.BusinessPartners.Rules;
public class BusinessPartnerBusinessRules : BaseBusinessRules
{
    private readonly IBusinessPartnerRepository _businessPartnerRepository;
    private readonly ILocalizationService _localizationService;

    public BusinessPartnerBusinessRules(IBusinessPartnerRepository businessPartnerRepository, ILocalizationService localizationService)
    {
        _businessPartnerRepository = businessPartnerRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, BusinessPartnersBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task<BusinessPartner?> CheckIfBusinessPartnerNameExists(string name)
    {
        BusinessPartner? existingBranch = await _businessPartnerRepository.GetAsync(b => b.Name == name);
        return existingBranch;
    }

    public async Task CheckIfBusinessPartnerNameExistsAndNotDeleted(string name)
    {
        BusinessPartner? existingBranch = await CheckIfBusinessPartnerNameExists(name);
        if (existingBranch != null && existingBranch.DeletedDate == null)
        {
            await throwBusinessException(BusinessPartnersBusinessMessages.BusinessPartnerAlreadyExists);
        }
    }
}

