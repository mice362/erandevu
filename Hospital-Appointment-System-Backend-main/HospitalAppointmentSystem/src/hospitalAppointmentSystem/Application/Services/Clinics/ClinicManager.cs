using Application.Features.Clinics.Rules;
using Application.Services.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using NArchitecture.Core.Persistence.Paging;
using System.Linq.Expressions;

namespace Application.Services.Clinics;

public class ClinicManager : IClinicService
{
    private readonly IClinicRepository _clinicRepository;
    private readonly ClinicBusinessRules _clinicBusinessRules;

    public ClinicManager(IClinicRepository clinicRepository, ClinicBusinessRules clinicBusinessRules)
    {
        _clinicRepository = clinicRepository;
        _clinicBusinessRules = clinicBusinessRules;
    }

    public async Task<Clinic?> GetAsync(
        Expression<Func<Clinic, bool>> predicate,
        Func<IQueryable<Clinic>, IIncludableQueryable<Clinic, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        Clinic? clinic = await _clinicRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return clinic;
    }

    public async Task<IPaginate<Clinic>?> GetListAsync(
        Expression<Func<Clinic, bool>>? predicate = null,
        Func<IQueryable<Clinic>, IOrderedQueryable<Clinic>>? orderBy = null,
        Func<IQueryable<Clinic>, IIncludableQueryable<Clinic, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<Clinic> clinicList = await _clinicRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return clinicList;
    }

    public async Task<Clinic> AddAsync(Clinic clinic)
    {
        Clinic addedClinic = await _clinicRepository.AddAsync(clinic);

        return addedClinic;
    }

    public async Task<Clinic> UpdateAsync(Clinic clinic)
    {
        Clinic updatedClinic = await _clinicRepository.UpdateAsync(clinic);

        return updatedClinic;
    }

    public async Task<Clinic> DeleteAsync(Clinic clinic, bool permanent = false)
    {
        Clinic deletedClinic = await _clinicRepository.DeleteAsync(clinic);

        return deletedClinic;
    }
}
