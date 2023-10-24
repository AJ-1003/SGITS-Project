using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using SGITS.App.Exceptions;
using SGITS.App.Interfaces;
using SGITS.Core.Entities;

namespace SGITS.App.CQRS.FamilyMember.Update;
public class AssignFamilyMemberToHouseCommand : BaseCommand, IRequest<BaseCommandResponse>
{
    public Guid HouseId { get; set; }

    public AssignFamilyMemberToHouseCommand(Guid houseId)
    {
        HouseId = houseId;
    }

    internal class AssignFamilyMemberHouseCommandHandler : IRequestHandler<AssignFamilyMemberToHouseCommand, BaseCommandResponse>
    {
        private readonly IFamilyMemberRepository _familyMemberRepository;
        private readonly IHouseRepository _houseRepository;

        public AssignFamilyMemberHouseCommandHandler(
            IFamilyMemberRepository familyMemberRepository,
            IHouseRepository houseRepository)
        {
            _familyMemberRepository = familyMemberRepository;
            _houseRepository = houseRepository;
        }

        public async Task<BaseCommandResponse> Handle(AssignFamilyMemberToHouseCommand command, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new AssignFamilyMemberToHouseCommandValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var house = await _houseRepository.GetByIdAsync(command.HouseId, cancellationToken);

            if (house is null)
            {
                throw new NotFoundException(nameof(Core.Entities.House), command.HouseId);
            }

            var familyMember = await _familyMemberRepository.GetByIdAsync(command.Id, cancellationToken);

            if (familyMember is null)
            {
                throw new NotFoundException(nameof(Core.Entities.FamilyMember), command.Id);
            }

            if (familyMember.HouseId.HasValue)
            {
                var currentHouse = await _houseRepository.GetByIdAsync(familyMember.HouseId, cancellationToken);

                if (currentHouse is null)
                {
                    throw new NotFoundException(nameof(Core.Entities.House), familyMember.HouseId);
                }

                familyMember.AssignToHouse(command.HouseId);

                await _familyMemberRepository.UpdateAsync(familyMember, cancellationToken);

                response.Id = familyMember.Id;
                response.Success = true;
                response.Message = $"{familyMember.FullName} has been reassigned from current house: ({currentHouse.Name}) to new house: ({house.Name}), successfully.";

                return response;
            }

            familyMember.AssignToHouse(command.HouseId);

            await _familyMemberRepository.UpdateAsync(familyMember, cancellationToken);

            response.Id = familyMember.Id;
            response.Success = true;
            response.Message = $"{familyMember.FullName} has been assigned to house: ({house.Name}), successfully.";

            return response;
        }
    }
}
