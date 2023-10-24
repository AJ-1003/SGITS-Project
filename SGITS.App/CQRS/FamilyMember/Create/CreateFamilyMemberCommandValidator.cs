using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace SGITS.App.CQRS.FamilyMember.Create;
public class CreateFamilyMemberCommandValidator : AbstractValidator<CreateFamilyMemberCommand>
{
    public CreateFamilyMemberCommandValidator()
    {
        RuleFor(fm => fm.FirstName)
            .NotEmpty();

        RuleFor(fm => fm.LastName)
            .NotEmpty();

        RuleFor(fm => fm.ContactNumber)
            .NotEmpty();
    }
}
