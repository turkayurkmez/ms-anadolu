using ECommerce.Identity.Application.Contracts;
using ECommerce.Identity.Domain.Aggregates;
using ECommerce.Identity.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Identity.Infrastructure.Repositories
{
    public class UserRepository(AccountDbContext accountDbContext) : IUserRepository
    {
        public async Task AddAsync(User entity)
        {
            await accountDbContext.Users.AddAsync(entity);
            await accountDbContext.SaveChangesAsync();
        }

        public Task DeleteAsync(User entity)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<User>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await accountDbContext.Users.AsNoTracking().SingleOrDefaultAsync(x => x.Email == email);
        }

        public Task<User> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
