using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGITS.App.CQRS.FamilyMember.Get;

namespace SGITS.App.CQRS.House.Get;
public class HouseReadModel
{
    public Guid Id { get; init; }
    public string Name { get; init; } 
    public string Address { get; init; }
    public bool HasOwner { get; init; }
    public List<FamilyMemberReadModel> FamilyMembers { get; init; }
}
