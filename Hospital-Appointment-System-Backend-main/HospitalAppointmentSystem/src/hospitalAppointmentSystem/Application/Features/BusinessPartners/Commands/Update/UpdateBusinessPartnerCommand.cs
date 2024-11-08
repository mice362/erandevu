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

namespace Application.Features.BusinessPartners.Commands.Update;
public class UpdateBusinessPartnerCommand : IRequest<UpdatedBusinessPartnerResponse>, ISecuredRequest, ILoggableRequest, ITransactionalRequest
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }
    public string About { get; set; }
    public byte[] Logo { get; set; }
    public string LogoName { get; set; }

    public string[] Roles => [Admin, Write, BusinessPartnerOperationClaims.Update];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetBusinessPartners"];

    public class UpdatedBusinessPartnerCommandHandler : IRequestHandler<UpdateBusinessPartnerCommand, UpdatedBusinessPartnerResponse>
    {
        private readonly IMapper _mapper;
        private readonly IBusinessPartnerRepository _businessPartnerRepository;
        private readonly BusinessPartnerBusinessRules _businessPartnerBusinessRules;

        public UpdatedBusinessPartnerCommandHandler(IMapper mapper, IBusinessPartnerRepository branchRepository,
                                         BusinessPartnerBusinessRules businessPartnerBusinessRules)
        {
            _mapper = mapper;
            _businessPartnerRepository = branchRepository;
            _businessPartnerBusinessRules = businessPartnerBusinessRules;
        }

        public async Task<UpdatedBusinessPartnerResponse> Handle(UpdateBusinessPartnerCommand request, CancellationToken cancellationToken)
        {
            BusinessPartner? branch = await _businessPartnerRepository.GetAsync(predicate: b => b.Id == request.Id, cancellationToken: cancellationToken);

            branch = _mapper.Map(request, branch);
            await _businessPartnerRepository.UpdateAsync(branch!);

            UpdatedBusinessPartnerResponse response = _mapper.Map<UpdatedBusinessPartnerResponse>(branch);

            return response;
        }
    }
}
