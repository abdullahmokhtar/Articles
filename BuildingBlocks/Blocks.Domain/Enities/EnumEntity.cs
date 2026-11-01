namespace Blocks.Domain.Enities;

public abstract class EnumEntity<TEnum> : Entity<TEnum>
    where TEnum : struct, Enum
{
    public TEnum Id { get; init; } = default!;

    public TEnum Name { get; init; } = default!;
}
