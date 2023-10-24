using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using SGITS.App.CQRS.FamilyMember.Update;
using SGITS.App.Exceptions;
using SGITS.App.Interfaces;

namespace SGITS.App.CQRS.FamilyMember.Delete;
public class DeleteFamilyMemberCommand : BaseCommand, IRequest
{
    public DeleteFamilyMemberCommand(Guid id)
    {
        Id = id;
    }

    internal class DeleteFamilyMemberCommandHandler : IRequestHandler<DeleteFamilyMemberCommand>
    {
        private readonly IFamilyMemberRepository _familyMemberRepository;

        public DeleteFamilyMemberCommandHandler(IFamilyMemberRepository familyMemberRepository)
        {
            _familyMemberRepository = familyMemberRepository;
        }

        public async Task Handle(DeleteFamilyMemberCommand command, CancellationToken cancellationToken)
        {
            var validator = new DeleteFamilyMemberCommandValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var familyMember = await _familyMemberRepository.GetByIdAsync(command.Id, cancellationToken);

            if (familyMember == null)
            {
                throw new NotFoundException(nameof(Core.Entities.FamilyMember), command.Id);
            }

            await _familyMemberRepository.DeleteAsync(familyMember, cancellationToken);
        }
    }
}
