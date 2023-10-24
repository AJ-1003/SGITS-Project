using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using SGITS.App.Exceptions;
using SGITS.App.Interfaces;
using SGITS.Core.Constants;
using SGITS.Core.Entities;

namespace SGITS.App.CQRS.FamilyMember.Create;
public class CreateFamilyMemberCommand : IRequest<BaseCommandResponse>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string ContactNumber { get; set; }

    public CreateFamilyMemberCommand(
        string firstName,
        string lastName,
        string contactNumber)
    {
        FirstName = firstName;
        LastName = lastName;
        ContactNumber = contactNumber;
    }

    internal class CreateFamilyMemberCommandHandler : IRequestHandler<CreateFamilyMemberCommand, BaseCommandResponse>
    {
        private readonly IFamilyMemberRepository _familyMemberRepository;

        public CreateFamilyMemberCommandHandler(IFamilyMemberRepository familyMemberRepository)
        {
            _familyMemberRepository = familyMemberRepository;
        }

        public async Task<BaseCommandResponse> Handle(CreateFamilyMemberCommand command, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateFamilyMemberCommandValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var familyMember = Core.Entities.FamilyMember.Create(
                command.FirstName,
                command.LastName,
                command.ContactNumber);

            var createdFamilyMember = await _familyMemberRepository.CreateAsync(familyMember, cancellationToken);

            if (createdFamilyMember is null)
            {
                response.Success = false;
                response.Message = "Family member could not be created.";

                return response;
            }

            response.Id = createdFamilyMember.Id;
            response.Success = true;
            response.Message = "Family member created successfully.";

            return response;
        }
    }
}
