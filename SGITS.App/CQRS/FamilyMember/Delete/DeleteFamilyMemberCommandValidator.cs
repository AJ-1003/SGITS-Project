using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace SGITS.App.CQRS.FamilyMember.Delete;
public class DeleteFamilyMemberCommandValidator : AbstractValidator<DeleteFamilyMemberCommand>
{
    public DeleteFamilyMemberCommandValidator()
    {
        RuleFor(fm => fm.Id)
            .NotEmpty();
    }
}
