using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using NArchitecture.Core.Persistence.Paging;
using System.Linq.Expressions;

namespace Application.Services.BusinessPartners;
public interface IBusinessPartnerService
{
    Task<BusinessPartner?> GetAsync(
      Expression<Func<BusinessPartner, bool>> predicate,
      Func<IQueryable<BusinessPartner>, IIncludableQueryable<BusinessPartner, object>>? include = null,
      bool withDeleted = false,
      bool enableTracking = true,
      CancellationToken cancellationToken = default
  );
    Task<IPaginate<BusinessPartner>?> GetListAsync(
        Expression<Func<BusinessPartner, bool>>? predicate = null,
        Func<IQueryable<BusinessPartner>, IOrderedQueryable<BusinessPartner>>? orderBy = null,
        Func<IQueryable<BusinessPartner>, IIncludableQueryable<BusinessPartner, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<BusinessPartner> AddAsync(BusinessPartner businessPartner);
    Task<BusinessPartner> UpdateAsync(BusinessPartner businessPartner);
    Task<BusinessPartner> DeleteAsync(BusinessPartner businessPartner, bool permanent = false);
}
