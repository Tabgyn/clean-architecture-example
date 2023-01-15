namespace CleanArchitectureExample.Domain.Common.Settings;

public class AnyOptions
{
    public const string SectionName = nameof(AnyOptions);

    public string SomeName { get; init; } = null!;
    public int SomeValue { get; init; }
}
