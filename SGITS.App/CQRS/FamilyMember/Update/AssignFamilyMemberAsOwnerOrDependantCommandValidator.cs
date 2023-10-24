using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace SGITS.App.CQRS.FamilyMember.Update;
public class AssignFamilyMemberAsOwnerOrDependantCommandValidator : AbstractValidator<AssignFamilyMemberAsOwnerOrDependantCommand>
{
    public AssignFamilyMemberAsOwnerOrDependantCommandValidator()
    {
        RuleFor(fm => fm.Id)
            .NotEmpty();

        RuleFor(fm => fm.MemberAssignment)
            .NotEmpty()
            .IsInEnum();
    }
}
