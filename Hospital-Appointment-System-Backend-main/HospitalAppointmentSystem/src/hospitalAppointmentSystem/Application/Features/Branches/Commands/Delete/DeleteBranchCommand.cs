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
using Org.BouncyCastle.Math.EC.Rfc7748;

namespace Application.Features.Branches.Commands.Delete;

public class DeleteBranchCommand : IRequest<DeletedBranchResponse>, ILoggableRequest, ITransactionalRequest,ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => [Admin, Write, BranchesOperationClaims.Delete];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetBranches"];

    public class DeleteBranchCommandHandler : IRequestHandler<DeleteBranchCommand, DeletedBranchResponse>
    {
        private readonly IMapper _mapper;
        private readonly IBranchRepository _branchRepository;
        private readonly BranchBusinessRules _branchBusinessRules;

        public DeleteBranchCommandHandler(IMapper mapper, IBranchRepository branchRepository,
                                         BranchBusinessRules branchBusinessRules)
        {
            _mapper = mapper;
            _branchRepository = branchRepository;
            _branchBusinessRules = branchBusinessRules;
        }

        public async Task<DeletedBranchResponse> Handle(DeleteBranchCommand request, CancellationToken cancellationToken)
        {
            Branch? branch = await _branchRepository.GetAsync(predicate: b => b.Id == request.Id && b.DeletedDate==null, cancellationToken: cancellationToken);
            await _branchBusinessRules.BranchShouldExistWhenSelected(branch);

            await _branchBusinessRules.CheckIfDoctorsExistInBranch(request.Id); // dont delete if branch has doctor

            await _branchRepository.DeleteAsync(branch!);

            DeletedBranchResponse response = _mapper.Map<DeletedBranchResponse>(branch);
            return response;
        }
    }
}