using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace SGITS.App.CQRS.FamilyMember.Get;
public class GetFamilyMemberByIdQueryValidator : AbstractValidator<GetFamilyMemberByIdQuery>
{
    public GetFamilyMemberByIdQueryValidator()
    {
        RuleFor(fm => fm.Id)
            .NotEmpty();
    }
}
