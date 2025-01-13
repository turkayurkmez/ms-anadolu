using ECommerce.Identity.Domain.Aggregates;

namespace ECommerce.Identity.Application.Contracts
{
    public interface IUserRepository : IRepository<Domain.Aggregates.User, Guid>
    {
        Task<User> GetByEmailAsync(string email);
    }
}
