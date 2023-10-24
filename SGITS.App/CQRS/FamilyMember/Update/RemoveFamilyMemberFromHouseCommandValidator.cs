using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace SGITS.App.CQRS.FamilyMember.Update;
public class RemoveFamilyMemberFromHouseCommandValidator : AbstractValidator<RemoveFamilyMemberFromHouseCommand>
{
    public RemoveFamilyMemberFromHouseCommandValidator()
    {
        RuleFor(fm => fm.Id)
             .NotEmpty();
    }
}
