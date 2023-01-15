using CleanArchitectureExample.Domain.Common.Enums;
using CleanArchitectureExample.Domain.Common.Models;

namespace CleanArchitectureExample.Domain.Common.Errors;

public static partial class Errors
{
    public static class General
    {
        public static Error NotFound(Guid? id = null)
        {
            string forId = id == null ? "" : $" for Id '{id}'";

            return new Error("General.NotFound", $"Record not found{forId}",
                ErrorType.NotFound);
        }
    }
}
