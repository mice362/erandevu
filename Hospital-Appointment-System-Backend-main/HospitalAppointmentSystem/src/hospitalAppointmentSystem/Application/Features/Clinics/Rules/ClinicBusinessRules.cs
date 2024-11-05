using Application.Features.Clinics.Constants;
using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;

namespace Application.Features.Clinics.Rules
{
    public class ClinicBusinessRules : BaseBusinessRules
    {
        private readonly IClinicRepository _clinicRepository;
        private readonly ILocalizationService _localizationService;

        public ClinicBusinessRules(IClinicRepository clinicRepository, ILocalizationService localizationService)
        {
            _clinicRepository = clinicRepository;
            _localizationService = localizationService;
        }

        private async Task throwBusinessException(string messageKey)
        {
            string message = await _localizationService.GetLocalizedAsync(messageKey, ClinicsBusinessMessages.SectionName);
            throw new BusinessException(message);
        }

        public async Task<Clinic?> CheckIfClinicNameExists(string name)
        {
            Clinic? existingBranch = await _clinicRepository.GetAsync(b => b.Name == name);
            return existingBranch;
        }

        public async Task CheckIfClinicNameExistsAndNotDeleted(string name)
        {
            Clinic? existingBranch = await CheckIfClinicNameExists(name);
            if (existingBranch != null && existingBranch.DeletedDate == null)
            {
                await throwBusinessException(ClinicsBusinessMessages.ClinicAlreadyExists);
            }
        }
    }
}
