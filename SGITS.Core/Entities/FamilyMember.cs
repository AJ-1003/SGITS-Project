using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGITS.Core.Constants;

namespace SGITS.Core.Entities;
public class FamilyMember
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string ContactNumber { get; set; } = string.Empty;
    public MemberAssignment MemberAssignment { get; set; } = MemberAssignment.Member;
    public Guid? HouseId { get; set; } = null;

    private static string BuildFullName(string firstName, string lastName) => $"{firstName} {lastName}";

    public static FamilyMember Create(
        string firstName,
        string lastName,
        string contactNumber)
    {
        var familyMember = new FamilyMember
        {
            FirstName = firstName,
            LastName = lastName,
            FullName = BuildFullName(firstName, lastName),
            ContactNumber = contactNumber,
        };

        return familyMember;
    }

    public void Update(
        string firstName,
        string lastName,
        string contactNumber)
    {

        FirstName = firstName;
        LastName = lastName;
        FullName = BuildFullName(firstName, lastName);
        ContactNumber = contactNumber;
    }

    public void AssignToHouse(Guid houseId)
    {
        HouseId = houseId;
    }

    public void RemoveFromHouse()
    {
        HouseId = null;
    }

    public void UpdateMemberAssignment(MemberAssignment memberAssignment)
    {
        MemberAssignment = memberAssignment;
    }
}
