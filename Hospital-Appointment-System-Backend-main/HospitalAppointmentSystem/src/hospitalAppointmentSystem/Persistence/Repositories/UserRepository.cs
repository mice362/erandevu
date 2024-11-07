using Application.Services.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using NArchitecture.Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories
{
    public class UserRepository : EfRepositoryBase<User, Guid, BaseDbContext>, IUserRepository
    {
        public UserRepository(BaseDbContext context)
            : base(context) { }

        public async Task DeleteAuthenticatorCode(Guid userId)
        {
            var user = await Context.Users
                .Include(u => u.EmailAuthenticators)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user != null && user.EmailAuthenticators != null)
            {
                var emailAuthenticator = user.EmailAuthenticators.FirstOrDefault();
                if (emailAuthenticator != null)
                {
                    Context.EmailAuthenticators.Remove(emailAuthenticator);
                    await Context.SaveChangesAsync();
                }
            }
        }
    }
}
