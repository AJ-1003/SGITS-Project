using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace SGITS.App.CQRS.House.Get;
public class GetHouseByIdQueryValidator : AbstractValidator<GetHouseByIdQuery>
{
    public GetHouseByIdQueryValidator()
    {
        RuleFor(h => h.Id)
            .NotEmpty();
    }
}
