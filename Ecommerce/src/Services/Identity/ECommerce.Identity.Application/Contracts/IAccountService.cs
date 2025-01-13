using ECommerce.Identity.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Identity.Application.Contracts
{
    public interface IAccountService
    {
        string GenerateToken(User user);
    }
}
