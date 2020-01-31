namespace Blog.Domain.SeedWork
{
    using System;
    using System.Collections;

    public abstract class Enumeration : IComparable
    {
        protected Enumeration(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; }

        public string Name { get; }

        public override bool Equals(object obj)
        {
            if (!(obj is Enumeration other))
                return false;

            var typeEquals = GetType().Equals(obj.GetType());
            var valueEquals = Id.Equals(other.Id);

            return typeEquals && valueEquals;
        }

        public int CompareTo(object other) => Id.CompareTo(((Enumeration)other).Id);
    }
}
