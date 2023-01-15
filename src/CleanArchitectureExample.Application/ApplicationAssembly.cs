using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace CleanArchitectureExample.Application;

[ExcludeFromCodeCoverage]
public static class ApplicationAssembly
{
    public static readonly Assembly Assembly = typeof(ApplicationAssembly).Assembly;
}
