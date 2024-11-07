using Application.Features.BusinessPartners.Constants;
using Application.Features.BusinessPartners.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using static Application.Features.BusinessPartners.Constants.BusinessPartnerOperationClaims;

namespace Application.Features.BusinessPartners.Commands.Create;
public class CreateBusinessPartnerCommand : IRequest<CreatedBusinessPartnerResponse>, ILoggableRequest, ITransactionalRequest, ISecuredRequest
{
    public string Name { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }
    public string About { get; set; }
    public byte[] Logo { get; set; }
    public string LogoName { get; set; }

    public string[] Roles => new[] { Admin, Write, BusinessPartnerOperationClaims.Create };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => new[] { "GetBusinessPartners" };

    public class CreatedBusinessPartnerCommandHandler : IRequestHandler<CreateBusinessPartnerCommand, CreatedBusinessPartnerResponse>
    {
        private readonly IMapper _mapper;
        private readonly IBusinessPartnerRepository _businessPartnerRepository;
        private readonly BusinessPartnerBusinessRules _businessPartnerBusinessRules;

        public CreatedBusinessPartnerCommandHandler(IMapper mapper, IBusinessPartnerRepository businessPartnerRepository,
                                         BusinessPartnerBusinessRules businessPartnerBusinessRules)
        {
            _mapper = mapper;
            _businessPartnerRepository = businessPartnerRepository;
            _businessPartnerBusinessRules = businessPartnerBusinessRules;
        }

        public async Task<CreatedBusinessPartnerResponse> Handle(CreateBusinessPartnerCommand request, CancellationToken cancellationToken)
        {
            await _businessPartnerBusinessRules.CheckIfBusinessPartnerNameExists(request.Name);

            await _businessPartnerBusinessRules.CheckIfBusinessPartnerNameExistsAndNotDeleted(request.Name);

            BusinessPartner businessPartner = _mapper.Map<BusinessPartner>(request);
            await _businessPartnerRepository.AddAsync(businessPartner);

            CreatedBusinessPartnerResponse response = _mapper.Map<CreatedBusinessPartnerResponse>(businessPartner);

            return response;
        }
    }
}
