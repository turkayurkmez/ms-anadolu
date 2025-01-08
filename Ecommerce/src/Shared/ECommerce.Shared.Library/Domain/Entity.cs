using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Shared.Library.Domain
{
    /*
     * Entity elbette generic olmak zorunda değil. Tüm entity'lerin Id özelliğinin int ya da Guid olması kararı alınsaydı, doğrudan int ya da Guid tipinde bir Id özelliği tanımlanabilirdi.
     */
    public abstract class Entity<T> where T: IEquatable<T>  
    {
        public T Id { get; set; }
        public DateTime CreatedDate { get; protected set; }

        public DateTime? LastModifiedDate { get; protected set; }

        public Entity()
        {
            Id = typeof(T) == typeof(Guid) ? (T)(object)Guid.NewGuid() : default;
            CreatedDate = DateTime.UtcNow;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Entity<T>))
                return false;
            if (Object.ReferenceEquals(this, obj))
                return true;
            if (this.GetType() != obj.GetType())
                return false;
            Entity<T> item = (Entity<T>)obj;
            return item.Id.Equals(this.Id);
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        public static bool operator ==(Entity<T> left, Entity<T> right)
        {
            if (left is null && right is null)
                return true;
            if (left is null || right is null)
                return false;

            return left.Equals(right);
        }

        public static bool operator !=(Entity<T> left, Entity<T> right)
        {
            return !(left == right);
        }

    }
}
