using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGITS.App.CQRS.FamilyMember.Get;
using SGITS.App.CQRS.House.Get;
using SGITS.Core.Constants;
using SGITS.Core.Entities;

namespace SGITS.App.Interfaces;
public interface IFamilyMemberRepository : IGenericRepository<FamilyMember>
{
    Task<FamilyMemberReadModel?> GetByIdQueryAsync(Guid familyMemberId, CancellationToken cancellationToken);
    Task<FamilyMember?> AssignFamilyMemberAsync(Guid familyMemberId, MemberAssignment memberAssignment, CancellationToken cancellationToken);
    Task<IReadOnlyList<FamilyMemberReadModel>?> GetAllFamilyMembersForHouseAsync(Guid? houseId, CancellationToken cancellationToken);
    Task<IReadOnlyList<FamilyMemberReadModel>?> GetAllAsync(CancellationToken cancellationToken);
}
