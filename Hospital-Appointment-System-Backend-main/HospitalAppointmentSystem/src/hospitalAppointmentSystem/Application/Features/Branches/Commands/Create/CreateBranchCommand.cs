using Application.Features.Branches.Constants;
using Application.Features.Branches.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.Branches.Constants.BranchesOperationClaims;

namespace Application.Features.Branches.Commands.Create;

public class CreateBranchCommand : IRequest<CreatedBranchResponse>, ILoggableRequest, ITransactionalRequest, ISecuredRequest
{
    public required string Name { get; set; }

    public string[] Roles => new[] { Admin, Write, BranchesOperationClaims.Create };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => new[] { "GetBranches" };

    public class CreateBranchCommandHandler : IRequestHandler<CreateBranchCommand, CreatedBranchResponse>
    {
        private readonly IMapper _mapper;
        private readonly IBranchRepository _branchRepository;
        private readonly BranchBusinessRules _branchBusinessRules;

        public CreateBranchCommandHandler(IMapper mapper, IBranchRepository branchRepository,
                                         BranchBusinessRules branchBusinessRules)
        {
            _mapper = mapper;
            _branchRepository = branchRepository;
            _branchBusinessRules = branchBusinessRules;
        }

        public async Task<CreatedBranchResponse> Handle(CreateBranchCommand request, CancellationToken cancellationToken)
        {
            await _branchBusinessRules.CheckIfBranchNameExistsAndHandle(request.Name);

            await _branchBusinessRules.CheckIfBranchNameExistsAndNotDeleted(request.Name);

            Branch branch = _mapper.Map<Branch>(request);
            await _branchRepository.AddAsync(branch);

            CreatedBranchResponse newResponse = _mapper.Map<CreatedBranchResponse>(branch);
            return newResponse;
        }

    }
}
