using ECommerce.Identity.Application.Contracts;
using ECommerce.Identity.Domain.Aggregates;

namespace ECommerce.Identity.Application.Services
{
    public class UserService(IUserRepository userRepository, IPasswordHasher passwordHasher, IAccountService accountService) : IUserService
    {
        public async Task<string> Login(string email, string password)
        {
            var isAccepted = await ValidateCredentialsAsync(email, password);
            if (!isAccepted)
            {
                throw new Exception("Invalid credentials");

            }

            var user = await userRepository.GetByEmailAsync(email);

            return accountService.GenerateToken(user);

        }

        public async Task<Guid> RegisterAsync(string email, string password, string userName)
        {
            var hashedPassword = passwordHasher.HashPassword(password);
            var user = new User(email, hashedPassword, userName, Role.User);
            await userRepository.AddAsync(user);
            return user.Id;
        }

        public async Task<bool> ValidateCredentialsAsync(string email, string password)
        {
          var user = await userRepository.GetByEmailAsync(email);
            if (user == null)
            {
                return false;
            }

            return passwordHasher.VerifyHashedPassword(user.PasswordHash, password);
        }
    }
}
