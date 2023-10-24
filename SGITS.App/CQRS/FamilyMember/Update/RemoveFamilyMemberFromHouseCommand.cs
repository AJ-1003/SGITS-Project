using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using SGITS.App.Exceptions;
using SGITS.App.Interfaces;

namespace SGITS.App.CQRS.FamilyMember.Update;
public class RemoveFamilyMemberFromHouseCommand : BaseCommand, IRequest<BaseCommandResponse>
{
    internal class RemoveFamilyMemberFromHouseCommandHandler : IRequestHandler<RemoveFamilyMemberFromHouseCommand, BaseCommandResponse>
    {
        private readonly IFamilyMemberRepository _familyMemberRepository;
        private readonly IHouseRepository _houseRepository;

        public RemoveFamilyMemberFromHouseCommandHandler(
            IFamilyMemberRepository familyMemberRepository,
            IHouseRepository houseRepository)
        {
            _familyMemberRepository = familyMemberRepository;
            _houseRepository = houseRepository;
        }

        public async Task<BaseCommandResponse> Handle(RemoveFamilyMemberFromHouseCommand command, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new RemoveFamilyMemberFromHouseCommandValidator();
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

            if (familyMember.HouseId is null)
            {
                response.Success = true;
                response.Message = $"{familyMember.FullName} is not assigned to a house.";

                return response;
            }

            var house = await _houseRepository.GetByIdAsync(familyMember.HouseId, cancellationToken);

            if (house is null)
            {
                throw new NotFoundException(nameof(Core.Entities.House), familyMember.HouseId);
            }

            familyMember.RemoveFromHouse();

            await _familyMemberRepository.UpdateAsync(familyMember, cancellationToken);

            response.Id = familyMember.Id;
            response.Success = true;
            response.Message = $"{familyMember.FullName} has been removed from house: {house.Name}, successfully.";

            return response;
        }
    }
}
