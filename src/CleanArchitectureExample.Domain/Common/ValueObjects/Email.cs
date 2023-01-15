using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using CleanArchitectureExample.Domain.Common.Exceptions;
using CleanArchitectureExample.Domain.Common.Models;

namespace CleanArchitectureExample.Domain.Common.ValueObjects;

[ExcludeFromCodeCoverage]
public sealed class Email : ValueObject
{
    private Email(string address)
    {
        if (string.IsNullOrEmpty(address) || address.Length < 5)
            throw new InvalidEmailException();

        const string pattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";

        if (!Regex.IsMatch(address, pattern))
            throw new InvalidEmailException();

        Address = address.ToLower().Trim();
    }

    public string Address { get; } = string.Empty;

    public static Email Create(string address)
    {
        return new Email(address);
    }

    public static implicit operator string(Email email) => email.Address;

    public static implicit operator Email(string address) => new(address);

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Address;
    }

    public override string ToString() => Address;
}
