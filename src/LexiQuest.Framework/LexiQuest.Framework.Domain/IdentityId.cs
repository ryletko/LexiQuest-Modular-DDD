namespace LexiQuest.Framework.Domain;

public abstract class IdentityId : IEquatable<IdentityId>
{
    public string Value { get; }

    protected IdentityId(string value)
    {
        if (String.IsNullOrWhiteSpace(value))
            throw new InvalidOperationException("Id value cannot be empty!");

        Value = value;
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj))
        {
            return false;
        }

        return obj is IdentityId other && Equals(other);
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    public bool Equals(IdentityId? other)
    {
        return this.Value == other?.Value;
    }

    public static bool operator ==(IdentityId? obj1, IdentityId? obj2)
    {
        if (object.Equals(obj1, null))
        {
            if (object.Equals(obj2, null))
            {
                return true;
            }

            return false;
        }

        return obj1.Equals(obj2);
    }

    public static bool operator !=(IdentityId x, IdentityId y)
    {
        return !(x == y);
    }
    
}