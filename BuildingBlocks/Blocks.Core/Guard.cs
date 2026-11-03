namespace Blocks.Core;

public static class Guard
{
    public static void ThrowIfNullOrWhitespace(string value)
        => ArgumentException.ThrowIfNullOrWhiteSpace(value);

    public static void ThrowIfNotEqual<T>(T value, T other) where T : IEquatable<T>
    => ArgumentOutOfRangeException.ThrowIfNotEqual(value, other);

    public static T AgainstNull<T>(T value, string parmeterName)
        => value ?? throw new ArgumentNullException(parmeterName, $"value can not be null: {parmeterName}.");

}
