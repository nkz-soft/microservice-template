namespace NKZSoft.Template.Domain.Common;

public abstract class Enumeration : IComparable
{
    public string Name { get; }

    public int Id { get; }

    protected Enumeration(int id, string name) => (Id, Name) = (id, name);

    public override string ToString() => Name;

    public static IEnumerable<T> GetAll<T>() where T : Enumeration =>
        typeof(T).GetFields(BindingFlags.Public |
                            BindingFlags.Static |
                            BindingFlags.DeclaredOnly)
                    .Select(fieldInfo => fieldInfo.GetValue(null))
                    .Cast<T>();

    public override bool Equals(object? obj)
    {
        if (obj is not Enumeration otherValue)
        {
            return false;
        }

        var typeMatches = GetType() == obj.GetType();
        var valueMatches = Id.Equals(otherValue.Id);

        return typeMatches && valueMatches;
    }

    public override int GetHashCode() => Id.GetHashCode();

    public static int AbsoluteDifference(Enumeration firstValue, Enumeration secondValue) =>
        Math.Abs(firstValue.Id - secondValue.Id);

    public static T FromValue<T>(int value) where T : Enumeration =>
        Parse<T, int>(value, "value", item => item.Id == value);

    public static T FromDisplayName<T>(string displayName) where T : Enumeration =>
        Parse<T, string>(displayName,
            "display name",
            item => item.Name.Equals(displayName, StringComparison.OrdinalIgnoreCase));

    private static T Parse<T, TK>(TK value, string description, Func<T, bool> predicate) where T : Enumeration =>
        GetAll<T>().FirstOrDefault(predicate)
        ?? throw new InvalidOperationException($"'{value}' is not a valid {description} in {typeof(T)}");

    public int CompareTo(object? obj) => Id.CompareTo(((Enumeration)obj!)!.Id);

    public static bool operator ==(Enumeration? left, Enumeration? right)
    {
        if (left is null)
        {
            return right is null;
        }

        return left.Equals(right);
    }

    public static bool operator !=(Enumeration left, Enumeration right) => !(left == right);

    public static bool operator <(Enumeration? left, Enumeration? right)
        => left is null ? right is not null : left.CompareTo(right) < 0;

    public static bool operator <=(Enumeration? left, Enumeration? right)
        => left is null || left.CompareTo(right) <= 0;

    public static bool operator >(Enumeration? left, Enumeration? right)
        => left?.CompareTo(right) > 0;

    public static bool operator >=(Enumeration? left, Enumeration? right)
        => left is null ? right is null : left.CompareTo(right) >= 0;
}
