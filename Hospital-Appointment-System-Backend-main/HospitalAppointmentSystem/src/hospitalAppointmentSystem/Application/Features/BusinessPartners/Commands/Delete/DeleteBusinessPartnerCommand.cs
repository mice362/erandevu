using Application.Features.BusinessPartners.Rules;
using Application.Features.Clinics.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using static Application.Features.BusinessPartners.Constants.BusinessPartnerOperationClaims;

namespace Application.Features.BusinessPartners.Commands.Delete;
public class DeleteBusinessPartnerCommand : IRequest<DeletedBusinessPartnerResponse>, ILoggableRequest, ITransactionalRequest, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => [Admin, Write, ClinicsOperationClaims.Delete];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetBusinessPartners"];

    public class DeleteBusinessPartnerCommandHandler : IRequestHandler<DeleteBusinessPartnerCommand, DeletedBusinessPartnerResponse>
    {
        private readonly IMapper _mapper;
        private readonly IBusinessPartnerRepository _businessPartnerRepository;
        private readonly BusinessPartnerBusinessRules _businessPartnerBusinessRules;

        public DeleteBusinessPartnerCommandHandler(IMapper mapper, IBusinessPartnerRepository businessPartnerRepository,
                                         BusinessPartnerBusinessRules businessPartnerBusinessRules)
        {
            _mapper = mapper;
            _businessPartnerRepository = businessPartnerRepository;
            _businessPartnerBusinessRules = businessPartnerBusinessRules;
        }

        public async Task<DeletedBusinessPartnerResponse> Handle(DeleteBusinessPartnerCommand request, CancellationToken cancellationToken)
        {
            BusinessPartner? businessPartner = await _businessPartnerRepository.GetAsync(predicate: b => b.Id == request.Id && b.DeletedDate == null, cancellationToken: cancellationToken);

            //TODO: bu klinik altında aktif bir kullanıcı bulunacak mı onu kontrol etmek gerekecek
            //await _clinicBusinessRules.CheckIfDoctorsExistInBranch(request.Id); // dont delete if branch has doctor

            await _businessPartnerRepository.DeleteAsync(businessPartner!);

            DeletedBusinessPartnerResponse response = _mapper.Map<DeletedBusinessPartnerResponse>(businessPartner);

            return response;
        }
    }
}
