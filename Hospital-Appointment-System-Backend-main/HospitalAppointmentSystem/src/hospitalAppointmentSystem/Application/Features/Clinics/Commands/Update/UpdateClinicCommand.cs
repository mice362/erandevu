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

namespace Application.Features.Clinics.Commands.Update;

public class UpdateClinicCommand : IRequest<UpdatedClinicResponse>, ISecuredRequest, ILoggableRequest, ITransactionalRequest
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }
    public string About { get; set; }
    public byte[] Logo { get; set; }
    public string LogoName { get; set; }

    public string[] Roles => [Admin, Write, ClinicsOperationClaims.Update];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetClinics"];

    public class UpdatedClinicCommandHandler : IRequestHandler<UpdateClinicCommand, UpdatedClinicResponse>
    {
        private readonly IMapper _mapper;
        private readonly IClinicRepository _clinicRepository;
        private readonly ClinicBusinessRules _clinicBusinessRules;

        public UpdatedClinicCommandHandler(IMapper mapper, IClinicRepository clinicRepository,
                                         ClinicBusinessRules clinicBusinessRules)
        {
            _mapper = mapper;
            _clinicRepository = clinicRepository;
            _clinicBusinessRules = clinicBusinessRules;
        }

        public async Task<UpdatedClinicResponse> Handle(UpdateClinicCommand request, CancellationToken cancellationToken)
        {
            Clinic? clinic = await _clinicRepository.GetAsync(predicate: b => b.Id == request.Id, cancellationToken: cancellationToken);

            clinic = _mapper.Map(request, clinic);
            await _clinicRepository.UpdateAsync(clinic!);

            UpdatedClinicResponse response = _mapper.Map<UpdatedClinicResponse>(clinic);

            return response;
        }
    }
}
