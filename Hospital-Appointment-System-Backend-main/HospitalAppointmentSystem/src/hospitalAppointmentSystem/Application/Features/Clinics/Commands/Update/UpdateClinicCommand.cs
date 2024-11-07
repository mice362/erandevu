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
    public required string Name { get; set; }

    public string[] Roles => [Admin, Write, ClinicsOperationClaims.Update];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetBranches"];

    public class UpdatedClinicCommandHandler : IRequestHandler<UpdateClinicCommand, UpdatedClinicResponse>
    {
        private readonly IMapper _mapper;
        private readonly IBranchRepository _branchRepository;
        private readonly ClinicBusinessRules _clinicBusinessRules;

        public UpdatedClinicCommandHandler(IMapper mapper, IBranchRepository branchRepository,
                                         ClinicBusinessRules clinicBusinessRules)
        {
            _mapper = mapper;
            _branchRepository = branchRepository;
            _clinicBusinessRules = clinicBusinessRules;
        }

        public async Task<UpdatedClinicResponse> Handle(UpdateClinicCommand request, CancellationToken cancellationToken)
        {
            Branch? branch = await _branchRepository.GetAsync(predicate: b => b.Id == request.Id, cancellationToken: cancellationToken);

            branch = _mapper.Map(request, branch);
            await _branchRepository.UpdateAsync(branch!);

            UpdatedClinicResponse response = _mapper.Map<UpdatedClinicResponse>(branch);

            return response;
        }
    }
}
