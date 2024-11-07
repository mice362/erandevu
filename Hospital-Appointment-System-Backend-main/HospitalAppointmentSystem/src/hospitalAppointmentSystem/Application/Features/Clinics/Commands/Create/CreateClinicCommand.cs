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

namespace Application.Features.Clinics.Commands.Create;
public class CreateClinicCommand : IRequest<CreatedClinicResponse>, ILoggableRequest, ITransactionalRequest, ISecuredRequest
{
    public string Name { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }
    public string About { get; set; }
    public byte[] Logo { get; set; }
    public string LogoName { get; set; }

    public string[] Roles => new[] { Admin, Write, ClinicsOperationClaims.Create };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => new[] { "GetClinics" };

    public class CreatedClinicCommandHandler : IRequestHandler<CreateClinicCommand, CreatedClinicResponse>
    {
        private readonly IMapper _mapper;
        private readonly IClinicRepository _clinicRepository;
        private readonly ClinicBusinessRules _clinicBusinessRules;

        public CreatedClinicCommandHandler(IMapper mapper, IClinicRepository clinicRepository,
                                         ClinicBusinessRules clinicBusinessRules)
        {
            _mapper = mapper;
            _clinicRepository = clinicRepository;
            _clinicBusinessRules = clinicBusinessRules;
        }

        public async Task<CreatedClinicResponse> Handle(CreateClinicCommand request, CancellationToken cancellationToken)
        {
            await _clinicBusinessRules.CheckIfClinicNameExists(request.Name);

            await _clinicBusinessRules.CheckIfClinicNameExistsAndNotDeleted(request.Name);

            Clinic clinic = _mapper.Map<Clinic>(request);
            await _clinicRepository.AddAsync(clinic);

            CreatedClinicResponse response = _mapper.Map<CreatedClinicResponse>(clinic);

            return response;
        }
    }
}
