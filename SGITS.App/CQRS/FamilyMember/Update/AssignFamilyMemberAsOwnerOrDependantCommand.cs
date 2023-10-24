using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using SGITS.App.CQRS.FamilyMember.Get;
using SGITS.App.Exceptions;
using SGITS.App.Interfaces;
using SGITS.Core.Constants;
using SGITS.Core.Entities;
using ValidationException = SGITS.App.Exceptions.ValidationException;

namespace SGITS.App.CQRS.FamilyMember.Update;
public class AssignFamilyMemberAsOwnerOrDependantCommand : BaseCommand, IRequest<BaseCommandResponse>
{
    public MemberAssignment MemberAssignment { get; set; }

    public AssignFamilyMemberAsOwnerOrDependantCommand(MemberAssignment memberAssignment)
    {
        MemberAssignment = memberAssignment;
    }

    internal class AssignFamilyMemberAsOwnerOrDependantCommandHandler : IRequestHandler<AssignFamilyMemberAsOwnerOrDependantCommand, BaseCommandResponse>
    {
        private readonly IFamilyMemberRepository _familyMemberRepository;
        private readonly IHouseRepository _houseRepository;

        public AssignFamilyMemberAsOwnerOrDependantCommandHandler(
            IFamilyMemberRepository familyMemberRepository,
            IHouseRepository houseRepository)
        {
            _familyMemberRepository = familyMemberRepository;
            _houseRepository = houseRepository;
        }

        public async Task<BaseCommandResponse> Handle(AssignFamilyMemberAsOwnerOrDependantCommand command, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new AssignFamilyMemberAsOwnerOrDependantCommandValidator();
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

            var isAssignedToHouse = IsAssignedToHouse(familyMember);

            if (!isAssignedToHouse)
            {
                response.Success = false;
                response.Message = "Family member is not assigned to a house.";

                return response;
            }

            var house = await GetHouse(familyMember.HouseId, cancellationToken);

            if (house.HasOwner && command.MemberAssignment == MemberAssignment.Owner)
            {
                var owner = await HouseOwner(familyMember.HouseId, cancellationToken);

                response.Success = false;
                response.Message = $"{owner.FullName} is already assigned as owner to house: ({owner.HouseId})";

                return response;
            }

            var assignedFamilyMember = AssignMember(familyMember, command.MemberAssignment);

            if (assignedFamilyMember.MemberAssignment == MemberAssignment.Owner)
            {
                house.UpdateOwner();
                await _houseRepository.UpdateAsync(house, cancellationToken);
                await _familyMemberRepository.UpdateAsync(assignedFamilyMember, cancellationToken);

                response.Id = assignedFamilyMember.Id;
                response.Success = true;
                response.Message = $"{assignedFamilyMember.FullName} has been assigned as owner.";

                return response;
            }

            await _familyMemberRepository.UpdateAsync(assignedFamilyMember, cancellationToken);

            response.Id = assignedFamilyMember.Id;
            response.Success = true;
            response.Message = $"{assignedFamilyMember.FullName} has been assigned as dependant.";

            return response;
        }

        private static Core.Entities.FamilyMember AssignMember(Core.Entities.FamilyMember familyMember, MemberAssignment memberAssignment)
        {
            if (memberAssignment == MemberAssignment.Owner)
            {
                familyMember.UpdateMemberAssignment(MemberAssignment.Owner);
                return familyMember;
            }

            if (memberAssignment == MemberAssignment.Dependant)
            {
                familyMember.UpdateMemberAssignment(MemberAssignment.Dependant);
                return familyMember;
            }

            familyMember.UpdateMemberAssignment(MemberAssignment.Member);
            return familyMember;
        }

        private async Task<Core.Entities.House> GetHouse(Guid? houseId, CancellationToken cancellationToken)
        {
            var house = await _houseRepository.GetByIdAsync(houseId, cancellationToken);

            if (house is null)
            {
                throw new NotFoundException(nameof(Core.Entities.House), houseId);
            }

            return house;
        }

        private async Task<FamilyMemberReadModel> HouseOwner(Guid? houseId, CancellationToken cancellationToken)
        {
            var familyMembers = await _familyMemberRepository.GetAllFamilyMembersForHouseAsync(houseId, cancellationToken);

            if (familyMembers is null)
            {
                return default;
            }

            return familyMembers.First(fm => fm.MemberAssignment == MemberAssignment.Owner);
        }

        private static bool IsAssignedToHouse(Core.Entities.FamilyMember familyMember)
        {
            if (familyMember.HouseId == null)
            {
                return false;
            }

            return true;
        }
    }
}
