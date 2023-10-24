using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using SGITS.App.Interfaces;

namespace SGITS.App.CQRS.FamilyMember.Get;
public class GetAllFamilyMembersQuery : IRequest<IReadOnlyList<FamilyMemberReadModel>>
{
    internal class GetAllFamilyMembersQueryHandler : IRequestHandler<GetAllFamilyMembersQuery, IReadOnlyList<FamilyMemberReadModel>>
    {
        private readonly IFamilyMemberRepository _familyMemberRepository;

        public GetAllFamilyMembersQueryHandler(IFamilyMemberRepository familyMemberRepository)
        {
            _familyMemberRepository = familyMemberRepository;
        }

        public async Task<IReadOnlyList<FamilyMemberReadModel>> Handle(GetAllFamilyMembersQuery request, CancellationToken cancellationToken)
        {
            return await _familyMemberRepository.GetAllAsync(cancellationToken);
        }
    }
}
