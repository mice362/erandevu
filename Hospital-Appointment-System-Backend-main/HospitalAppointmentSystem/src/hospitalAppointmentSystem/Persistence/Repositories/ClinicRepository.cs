using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class ClinicRepository : EfRepositoryBase<Clinic, int, BaseDbContext>, IClinicRepository
{
    public ClinicRepository(BaseDbContext context) : base(context)
    {
    }
}
