using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGITS.Core.Constants;

namespace SGITS.App.CQRS.FamilyMember.Get;
public class FamilyMemberReadModel
{
    public Guid Id { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string FullName { get; init; }
    public string ContactNumber { get; init; }
    public MemberAssignment MemberAssignment { get; init; }
    public Guid? HouseId { get; init; }

    public FamilyMemberReadModel()
    {
    }

    public FamilyMemberReadModel(
        Guid id,
        string firstName,
        string lastName,
        string fullName,
        string contactNumber,
        MemberAssignment memberAssignment,
        Guid? houseId)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        FullName = fullName;
        ContactNumber = contactNumber;
        MemberAssignment = memberAssignment;
        HouseId = houseId;
    }
}