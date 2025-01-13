using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Identity.Application.Services
{
    public interface IUserService
    {
        Task<Guid> RegisterAsync(string email, string password, string userName);
        Task<bool> ValidateCredentialsAsync(string email, string password);

        Task<string> Login(string email, string password);

    }
}
