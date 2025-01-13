using ECommerce.Shared.Library.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Identity.Domain.Aggregates
{
    public class User : AggregateRoot<Guid>
    {
        public string Email { get; private set; }
        public string PasswordHash { get; private set; }
        public string UserName { get; private set; }
        public bool IsActive { get; set; }

        public Role Role { get; private set; }

        public User(string email, string passwordHash, string userName, Role role)
        {
            Email = email;
            PasswordHash = passwordHash;
            UserName = userName;
            Role = role;
            IsActive = true;
        }

        public User()
        {
            
        }


    }
}
