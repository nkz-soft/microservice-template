namespace NKZSoft.Template.Domain.Common;

public abstract class ValueObject
{
    protected abstract IEnumerable<object> GetEqualityComponents();

    public static bool operator ==(ValueObject? left, ValueObject? right)
    {
        return left is null && right is null || (left is not null && right is not null && left.Equals(right));
    }

    public static bool operator !=(ValueObject? left, ValueObject? right) => !(left == right);

    public override bool Equals(object? obj)
    {
        if (obj == null || obj.GetType() != GetType())
        {
            return false;
        }

        var other = (ValueObject)obj;

        return this.GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
    }

    public override int GetHashCode() =>
        GetEqualityComponents()
            .Select(obj => obj.GetHashCode())
            .Aggregate((left, right) => left ^ right);

    public ValueObject? GetCopy() => MemberwiseClone() as ValueObject;
}
