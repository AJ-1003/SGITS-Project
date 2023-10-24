using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace SGITS.App.CQRS.House.Create;
public class CreateHouseCommandValidator : AbstractValidator<CreateHouseCommand>
{
    public CreateHouseCommandValidator()
    {
        RuleFor(h => h.Name)
            .NotEmpty();

        RuleFor(h => h.Address)
            .NotEmpty();
    }
}
