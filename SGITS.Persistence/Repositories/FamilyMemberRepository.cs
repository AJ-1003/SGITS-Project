using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SGITS.App.CQRS.FamilyMember.Get;
using SGITS.App.Interfaces;
using SGITS.Core.Constants;
using SGITS.Core.Entities;

namespace SGITS.Persistence.Repositories;
public class FamilyMemberRepository : IFamilyMemberRepository
{
    private readonly ApplicationDbContext _context;

    public FamilyMemberRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<FamilyMember?> AssignFamilyMemberAsync(Guid familyMemberId, MemberAssignment memberAssignment, CancellationToken cancellationToken)
    {
        var familyMember = await _context.FamilyMembers
            .FirstOrDefaultAsync(fm => fm.Id == familyMemberId, cancellationToken);

        if (familyMember is null)
        {
            return null;
        }

        if (familyMember.MemberAssignment == memberAssignment)
        {
            return familyMember;
        }

        familyMember.MemberAssignment = memberAssignment;

        _context.FamilyMembers.Update(familyMember);
        await _context.SaveChangesAsync(cancellationToken);

        return familyMember;
    }

    public async Task<FamilyMember> CreateAsync(FamilyMember familyMember, CancellationToken cancellationToken)
    {
        await _context.FamilyMembers.AddAsync(familyMember, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return familyMember;
    }

    public async Task DeleteAsync(FamilyMember familyMember, CancellationToken cancellationToken)
    {
        _context.FamilyMembers.Remove(familyMember);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> Exists(Guid familyMemberId, CancellationToken cancellationToken)
    {
        var familyMember = await _context.FamilyMembers.FindAsync(familyMemberId, cancellationToken);

        if (familyMember is null)
        {
            return false;
        }

        return true;
    }
        
    public async Task UpdateAsync(FamilyMember familyMember, CancellationToken cancellationToken)
    {
        _context.FamilyMembers.Update(familyMember);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<FamilyMemberReadModel>?> GetAllFamilyMembersForHouseAsync(Guid? houseId, CancellationToken cancellationToken)
    {
        var query = await _context.FamilyMembers
            .Where(fm => fm.HouseId == houseId)
            .ToListAsync(cancellationToken);

        var familyMembers = query.Select(fm => new FamilyMemberReadModel(
            fm.Id,
            fm.FirstName,
            fm.LastName,
            fm.FullName,
            fm.ContactNumber,
            fm.MemberAssignment,
            fm.HouseId))
            .ToList();

        return familyMembers;
    }

    public async Task<FamilyMemberReadModel?> GetByIdQueryAsync(Guid familyMemberId, CancellationToken cancellationToken)
    {
        var query = await _context.FamilyMembers
            .FirstOrDefaultAsync(fm => fm.Id == familyMemberId, cancellationToken);

        if (query is null)
        {
            return null;
        }

        var familyMember = new FamilyMemberReadModel
        {
            Id = query.Id,
            FirstName = query.FirstName,
            LastName = query.LastName,
            FullName = query.FullName,
            ContactNumber = query.ContactNumber,
            MemberAssignment = query.MemberAssignment,
            HouseId = query.HouseId
        };

        return familyMember;
    }

    public async Task<FamilyMember?> GetByIdAsync(Guid? familyMemberId, CancellationToken cancellationToken)
    {
        var familyMember = await _context.FamilyMembers
            .FirstOrDefaultAsync(fm => fm.Id == familyMemberId, cancellationToken);

        if (familyMember is null)
        {
            return null;
        }

        return familyMember;
    }

    public async Task<IReadOnlyList<FamilyMemberReadModel>?> GetAllAsync(CancellationToken cancellationToken)
    {
        var query = await _context.FamilyMembers.ToListAsync(cancellationToken);

        if (!query.Any())
        {
            return null;
        }

        var familyMembers = query.Select(fm => new FamilyMemberReadModel
        {
            Id = fm.Id,
            FirstName = fm.FirstName,
            LastName = fm.LastName,
            FullName = fm.FullName,
            ContactNumber = fm.ContactNumber,
            MemberAssignment = fm.MemberAssignment,
            HouseId = fm.HouseId
        }).ToList();

        return familyMembers;
    }
}
