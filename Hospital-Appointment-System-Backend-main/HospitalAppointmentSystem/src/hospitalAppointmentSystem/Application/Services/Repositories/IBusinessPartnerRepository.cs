using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;

namespace Application.Services.Repositories;
public interface IBusinessPartnerRepository : IAsyncRepository<BusinessPartner, int>, IRepository<BusinessPartner, int>
{
}
