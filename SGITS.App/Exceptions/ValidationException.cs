using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.Results;

namespace SGITS.App.Exceptions;
public class ValidationException : ApplicationException
{
    public ValidationException(string message) : base(message)
    {
    }

    public ValidationException(IEnumerable<ValidationFailure> failures) 
        : this($"One or more validation failures have occurred.\n{WriteErrors(failures)}")
    {
    }

    private static string WriteErrors(IEnumerable<ValidationFailure> failures)
    {
        var sb = new StringBuilder();
        foreach (var failure in failures)
        {
            sb.AppendLine($"\t{failure.PropertyName}: {failure.ErrorMessage}");
        }
        return sb.ToString();
    }
}
