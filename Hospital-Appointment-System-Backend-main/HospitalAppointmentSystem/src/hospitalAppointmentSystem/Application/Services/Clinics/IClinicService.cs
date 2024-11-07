using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using NArchitecture.Core.Persistence.Paging;
using System.Linq.Expressions;

namespace Application.Services.Clinics;
public interface IClinicService
{
    Task<Clinic?> GetAsync(
       Expression<Func<Clinic, bool>> predicate,
       Func<IQueryable<Clinic>, IIncludableQueryable<Clinic, object>>? include = null,
       bool withDeleted = false,
       bool enableTracking = true,
       CancellationToken cancellationToken = default
   );
    Task<IPaginate<Clinic>?> GetListAsync(
        Expression<Func<Clinic, bool>>? predicate = null,
        Func<IQueryable<Clinic>, IOrderedQueryable<Clinic>>? orderBy = null,
        Func<IQueryable<Clinic>, IIncludableQueryable<Clinic, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<Clinic> AddAsync(Clinic clinic);
    Task<Clinic> UpdateAsync(Clinic clinic);
    Task<Clinic> DeleteAsync(Clinic clinic, bool permanent = false);
}
