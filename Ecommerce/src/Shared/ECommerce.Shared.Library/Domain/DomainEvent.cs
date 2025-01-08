using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Shared.Library.Domain
{
    public abstract class DomainEvent : IDomainEvent
    {
        public Guid Id { get; protected set; }

        public DateTime OccurredOn { get; protected set; }

        public DomainEvent()
        {
            Id = Guid.NewGuid();
            OccurredOn = DateTime.UtcNow;
        }
    }
}
