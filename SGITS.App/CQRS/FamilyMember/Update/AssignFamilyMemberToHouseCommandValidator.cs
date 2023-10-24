using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace SGITS.App.CQRS.FamilyMember.Update;
public class AssignFamilyMemberToHouseCommandValidator : AbstractValidator<AssignFamilyMemberToHouseCommand>
{
    public AssignFamilyMemberToHouseCommandValidator()
    {
        RuleFor(fm => fm.Id)
            .NotEmpty();

        RuleFor(fm => fm.HouseId)
            .NotEmpty();
    }
}
