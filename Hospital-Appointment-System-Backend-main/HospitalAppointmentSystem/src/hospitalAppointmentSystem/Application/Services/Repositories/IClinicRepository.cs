using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IClinicRepository : IAsyncRepository<Clinic, int>, IRepository<Clinic, int>
{

}
