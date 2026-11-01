namespace Blocks.Domain.ValueObjects;

public abstract class StringValueObject : IEquatable<StringValueObject>, IEquatable<string>
{
    public string Value { get; protected set; } = default!;
    protected StringValueObject(string value) => Value = value;

    public override string ToString() => Value;
    public override int GetHashCode() => Value.GetHashCode();

    public bool Equals(StringValueObject? other) => Value.Equals(other?.Value);

    public bool Equals(string? other) => Value.Equals(other);

    public static implicit operator string(StringValueObject @object) => @object.Value;
}
