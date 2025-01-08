using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Shared.Library.Domain
{
    public interface IAggregateRoot
    {
        //Dikkat! Sadece aggrgate event fırlatabilir.
        IReadOnlyCollection<IDomainEvent> DomainEvents { get; }
        void ClearDomainEvents();
    }

}
