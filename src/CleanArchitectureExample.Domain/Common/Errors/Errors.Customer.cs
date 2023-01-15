using CleanArchitectureExample.Domain.Common.Enums;
using CleanArchitectureExample.Domain.Common.Models;

namespace CleanArchitectureExample.Domain.Common.Errors;

public static partial class Errors
{
    public static class Customer
    {
        public static readonly Error EmailAlreadyInUse = new(
            "Customer.EmailAlreadyInUse",
            "The specified email is already in use",
            ErrorType.Conflict);
    }
}
