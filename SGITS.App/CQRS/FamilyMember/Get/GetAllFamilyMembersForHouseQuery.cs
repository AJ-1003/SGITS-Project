using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using SGITS.App.Exceptions;
using SGITS.App.Interfaces;
using SGITS.Core.Entities;

namespace SGITS.App.CQRS.FamilyMember.Get;
public class GetAllFamilyMembersForHouseQuery : IRequest<IReadOnlyList<FamilyMemberReadModel>>
{
    public Guid HouseId { get; set; }

    public GetAllFamilyMembersForHouseQuery(Guid houseId)
    {
        HouseId = houseId;
    }

    internal class GetAllFamilyMembersForHouseQueryHandler : IRequestHandler<GetAllFamilyMembersForHouseQuery, IReadOnlyList<FamilyMemberReadModel>>
    {
        private readonly IFamilyMemberRepository _familyMemberRepository;
        private readonly IHouseRepository _houseRepository;

        public GetAllFamilyMembersForHouseQueryHandler(
            IFamilyMemberRepository familyMemberRepository,
            IHouseRepository houseRepository)
        {
            _familyMemberRepository = familyMemberRepository;
            _houseRepository = houseRepository;
        }

        public async Task<IReadOnlyList<FamilyMemberReadModel>> Handle(GetAllFamilyMembersForHouseQuery query, CancellationToken cancellationToken)
        {
            var validator = new GetAllFamilyMembersForHouseQueryValidator();
            var validatorResult = await validator.ValidateAsync(query, cancellationToken);

            if (!validatorResult.IsValid)
            {
                throw new ValidationException(validatorResult.Errors);
            }

            var house = await _houseRepository.GetByIdAsync(query.HouseId, cancellationToken);

            if (house is null)
            {
                throw new NotFoundException(nameof(Core.Entities.House), query.HouseId);
            }

            var familyMembers = await _familyMemberRepository.GetAllFamilyMembersForHouseAsync(query.HouseId, cancellationToken);

            return familyMembers;
        }
    }
}
