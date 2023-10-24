using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using SGITS.App.Exceptions;
using SGITS.App.Interfaces;
using SGITS.Core.Constants;

namespace SGITS.App.CQRS.FamilyMember.Update;
public class UpdateFamilyMemberCommand : BaseCommand, IRequest<BaseCommandResponse>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string ContactNumber { get; set; }

    public UpdateFamilyMemberCommand(
        string firstName,
        string lastName,
        string contactNumber)
    {
        FirstName = firstName;
        LastName = lastName;
        ContactNumber = contactNumber;
    }

    internal class UpdateFamilyMemberCommandHandler : IRequestHandler<UpdateFamilyMemberCommand, BaseCommandResponse>
    {
        private readonly IFamilyMemberRepository _familyMemberRepository;

        public UpdateFamilyMemberCommandHandler(IFamilyMemberRepository familyMemberRepository)
        {
            _familyMemberRepository = familyMemberRepository;
        }

        public async Task<BaseCommandResponse> Handle(UpdateFamilyMemberCommand command, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new UpdateFamilyMemberCommandValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var familyMember = await _familyMemberRepository.GetByIdAsync(command.Id, cancellationToken);

            if (familyMember is null)
            {
                throw new NotFoundException(nameof(Core.Entities.FamilyMember), command.Id);
            }

            familyMember.Update(
                command.FirstName,
                command.LastName,
                command.ContactNumber);

            await _familyMemberRepository.UpdateAsync(familyMember, cancellationToken);

            response.Id = familyMember.Id;
            response.Success = true;
            response.Message = "Family member updated successfully.";

            return response;
        }
    }
}
