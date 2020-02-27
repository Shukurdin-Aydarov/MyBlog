namespace Blog.Domain.SeedWork
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

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

        public override int GetHashCode() => Id.GetHashCode();

        public override string ToString() => Name;

        public static int AbsoluteDifference(Enumeration left, Enumeration right)
        {
            return Math.Abs(left.Id - right.Id);
        }

        public static IEnumerable<T> GetAll<T>() where T : Enumeration
        {
            var flags = BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly;
            var fields = typeof(T).GetFields(flags);

            return fields.Select(f => f.GetValue(null)).Cast<T>();
        }

        public static T FromValue<T>(int value) where T : Enumeration
        {
            return Parse<T, int>(value, "value", i => i.Id == value);
        }

        public static TEnum FromDisplayName<TEnum>(string name) where TEnum : Enumeration
        {
            return Parse<TEnum, string>(name, "display name", i => i.Name == name);
        }

        private static TEnum Parse<TEnum, TKey>(TKey value, string description, Func<TEnum, bool> predicate) where TEnum : Enumeration
        {
            var matchingItem = GetAll<TEnum>().FirstOrDefault(predicate);

            if (matchingItem == null)
                throw new InvalidOperationException($"'{value}' is not a valid {description} in {typeof(TEnum)}");

            return matchingItem;
        }

        public int CompareTo(object other) => Id.CompareTo(((Enumeration)other).Id);
    }
}
