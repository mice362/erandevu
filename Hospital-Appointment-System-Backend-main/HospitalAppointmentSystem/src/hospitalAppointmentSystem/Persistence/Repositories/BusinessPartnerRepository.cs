using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;
public class BusinessPartnerRepository : EfRepositoryBase<BusinessPartner, int, BaseDbContext>, IBusinessPartnerRepository
{
    public BusinessPartnerRepository(BaseDbContext context) : base(context)
    {
    }
}
