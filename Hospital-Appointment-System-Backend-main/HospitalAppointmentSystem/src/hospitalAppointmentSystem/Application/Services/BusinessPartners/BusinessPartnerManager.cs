using Application.Features.BusinessPartners.Rules;
using Application.Services.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using NArchitecture.Core.Persistence.Paging;
using System.Linq.Expressions;

namespace Application.Services.BusinessPartners;
public class BusinessPartnerManager : IBusinessPartnerService
{
    private readonly IBusinessPartnerRepository _businessPartnerRepository;
    private readonly BusinessPartnerBusinessRules _businessPartnerBusinessRules;

    public BusinessPartnerManager(IBusinessPartnerRepository businessPartnerRepository, BusinessPartnerBusinessRules businessPartnerBusinessRules)
    {
        _businessPartnerRepository = businessPartnerRepository;
        _businessPartnerBusinessRules = businessPartnerBusinessRules;
    }

    public async Task<BusinessPartner?> GetAsync(
        Expression<Func<BusinessPartner, bool>> predicate,
        Func<IQueryable<BusinessPartner>, IIncludableQueryable<BusinessPartner, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        BusinessPartner? businessPartner = await _businessPartnerRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return businessPartner;
    }

    public async Task<IPaginate<BusinessPartner>?> GetListAsync(
        Expression<Func<BusinessPartner, bool>>? predicate = null,
        Func<IQueryable<BusinessPartner>, IOrderedQueryable<BusinessPartner>>? orderBy = null,
        Func<IQueryable<BusinessPartner>, IIncludableQueryable<BusinessPartner, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<BusinessPartner> businessPartnerList = await _businessPartnerRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return businessPartnerList;
    }

    public async Task<BusinessPartner> AddAsync(BusinessPartner businessPartner)
    {
        BusinessPartner addedBusinessPartner = await _businessPartnerRepository.AddAsync(businessPartner);

        return addedBusinessPartner;
    }

    public async Task<BusinessPartner> UpdateAsync(BusinessPartner businessPartner)
    {
        BusinessPartner updatedBusinessPartner = await _businessPartnerRepository.UpdateAsync(businessPartner);

        return updatedBusinessPartner;
    }

    public async Task<BusinessPartner> DeleteAsync(BusinessPartner businessPartner, bool permanent = false)
    {
        BusinessPartner deletedBusinessPartner = await _businessPartnerRepository.DeleteAsync(businessPartner);

        return deletedBusinessPartner;
    }
}
