namespace Blog.Domain.SeedWork
{
    using System.Linq;
    using System.Collections.Generic;

    public abstract class ValueObject
    {
        protected bool EqualOperator(ValueObject left, ValueObject right)
        {
            if (left is null ^ right is null)
                return false;

            return left is null || left.Equals(right);
        }

        protected bool NotEqualOperator(ValueObject left, ValueObject right)
        {
            return !EqualOperator(left, right);
        }

        protected abstract IEnumerable<object> GetAtomicValues();

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
                return false;

            var other = (ValueObject)obj;
            var thisValues = GetAtomicValues().GetEnumerator();
            var otherValues = other.GetAtomicValues().GetEnumerator();

            while(thisValues.MoveNext() && otherValues.MoveNext())
            {
                var thisCurrent = thisValues.Current;
                var otherCurrent = otherValues.Current;

                if (thisCurrent is null ^ otherCurrent is null)
                    return false;

                if (thisCurrent?.Equals(otherCurrent) == false)
                    return false;
            }

            return !thisValues.MoveNext() && !otherValues.MoveNext();
        }

        public override int GetHashCode()
        {
            return GetAtomicValues()
                .Select(x => x?.GetHashCode() ?? 0)
                .Aggregate((x, y) => x ^ y);
        }
    }
}
