namespace WasteIntoCity.Core.ValueObjects
{
    public abstract class ValueObject
    {
        protected abstract IEnumerable<object> GetEqualityComponents();

        public override bool Equals(object? obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (GetType() != obj.GetType())
            {
                return false;
            }

            ValueObject valueObject = (ValueObject)obj;

            return GetEqualityComponents().SequenceEqual(valueObject.GetEqualityComponents());
        }

        public override int GetHashCode()
        {
            return GetEqualityComponents().Aggregate(default(int), (hashcode, value) =>
                HashCode.Combine(hashcode, value.GetHashCode()));
        }

        public static bool operator ==(ValueObject? objFirst, ValueObject? objSecond)
        {
            if (objFirst is null && objSecond is null)
            {
                return true;
            }

            if (objFirst is null || objSecond is null)
            {
                return false;
            }

            return objFirst.Equals(objSecond);
        }

        public static bool operator !=(ValueObject? objFirst, ValueObject? objSecond)
        {
            return !(objFirst == objSecond);
        }
    }
}
