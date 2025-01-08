using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Shared.Library.Domain
{
    public interface IDomainEvent
    {
        public Guid Id { get; }
        public DateTime OccurredOn { get; }
    }
}
