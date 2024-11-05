using Application.Features.Branches.Commands.Delete;
using Application.Features.Branches.Rules;
using Application.Features.Clinics.Constants;
using Application.Features.Clinics.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using static Application.Features.Clinics.Constants.ClinicsOperationClaims;

namespace Application.Features.Clinics.Commands.Delete;

public class DeleteClinicCommand : IRequest<DeletedClinicResponse>, ILoggableRequest, ITransactionalRequest, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => [Admin, Write, ClinicsOperationClaims.Delete];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetClinics"];

    public class DeleteClinicCommandHandler : IRequestHandler<DeleteClinicCommand, DeletedClinicResponse>
    {
        private readonly IMapper _mapper;
        private readonly IClinicRepository _clinicRepository;
        private readonly ClinicBusinessRules _clinicBusinessRules;

        public DeleteClinicCommandHandler(IMapper mapper, IClinicRepository clinicRepository,
                                         ClinicBusinessRules clinicBusinessRules)
        {
            _mapper = mapper;
            _clinicRepository = clinicRepository;
            _clinicBusinessRules = clinicBusinessRules;
        }

        public async Task<DeletedClinicResponse> Handle(DeleteClinicCommand request, CancellationToken cancellationToken)
        {
            Clinic? clinic = await _clinicRepository.GetAsync(predicate: b => b.Id == request.Id && b.DeletedDate == null, cancellationToken: cancellationToken);
            
            //TODO: bu klinik altında aktif bir kullanıcı bulunacak mı onu kontrol etmek gerekecek
            //await _clinicBusinessRules.CheckIfDoctorsExistInBranch(request.Id); // dont delete if branch has doctor

            await _clinicRepository.DeleteAsync(clinic!);

            DeletedClinicResponse response = _mapper.Map<DeletedClinicResponse>(clinic);

            return response;
        }
    }
}
