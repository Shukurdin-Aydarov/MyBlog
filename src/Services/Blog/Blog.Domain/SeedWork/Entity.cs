namespace Blog.Domain
{
    using MediatR;
    using System.Collections.Generic;

    public abstract class Entity
    {
        public virtual int Id { get; protected set; }

        private readonly List<INotification> events = new List<INotification>();
        public IReadOnlyCollection<INotification> Events => events.AsReadOnly();

        public void AddEvent(INotification e)
        {
            events.Add(e);
        }

        public void RemoveEvent(INotification e)
        {
            events.Remove(e);
        }

        public void ClearEvents()
        {
            events.Clear();
        }

        public bool IsTransient()
        {
            return Id == default;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Entity))
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            if (GetType() != obj.GetType())
                return false;

            var entity = (Entity)obj;

            return entity.IsTransient() || IsTransient()
                ? false
                : entity.Id == Id;
        }

        private int? requestedHashCode;
        public override int GetHashCode()
        {
            if (IsTransient())
                return base.GetHashCode();

            if (!requestedHashCode.HasValue)
                requestedHashCode = Id.GetHashCode() ^ 31; // XOR for random distribution (http://blogs.msdn.com/b/ericlippert/archive/2011/02/28/guidelines-and-rules-for-gethashcode.aspx)

            return requestedHashCode.Value;
        }

        public static bool operator ==(Entity left, Entity right)
        {
            return Equals(left, null) ? Equals(right, null) : left.Equals(right);
        }

        public static bool operator !=(Entity left, Entity right)
        {
            return !(left == right);
        }
    }
}
