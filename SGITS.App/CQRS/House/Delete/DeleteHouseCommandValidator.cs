using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace SGITS.App.CQRS.House.Delete;
public class DeleteHouseCommandValidator : AbstractValidator<DeleteHouseCommand>
{
    public DeleteHouseCommandValidator()
    {
        RuleFor(h => h.Id)
            .NotEmpty();
    }
}
