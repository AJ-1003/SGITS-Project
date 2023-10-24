using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SGITS.App.CQRS.FamilyMember.Get;
using SGITS.App.CQRS.House.Get;
using SGITS.App.Interfaces;
using SGITS.Core.Entities;

namespace SGITS.Persistence.Repositories;
public class HouseRepository : IHouseRepository
{
    private readonly ApplicationDbContext _context;

    public HouseRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<House> CreateAsync(House house, CancellationToken cancellationToken)
    {
        await _context.Houses.AddAsync(house, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return house;
    }

    public async Task DeleteAsync(House house, CancellationToken cancellationToken)
    {
        _context.Houses.Remove(house);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> Exists(Guid houseId, CancellationToken cancellationToken)
    {
        var house = await _context.Houses.FindAsync(houseId, CancellationToken.None);

        if (house is null)
        {
            return false;
        }

        return true;
    }

    public async Task<IReadOnlyList<HouseReadModel>?> GetAllAsync(CancellationToken cancellationToken)
    {
        var houseQuery = await _context.Houses
            .AsNoTracking()
            .AsQueryable()
            .ToListAsync(cancellationToken);

        if (!houseQuery.Any())
        {
            return null;
        }

        var familyMembers = await _context.FamilyMembers
            .AsNoTracking()
            .AsQueryable()
            .ToListAsync(cancellationToken);

        var houses = houseQuery.Select(h => new HouseReadModel
        {
            Id = h.Id,
            Name = h.Name,
            Address = h.Address,
            HasOwner = h.HasOwner,
            FamilyMembers = familyMembers
                .Where(fm => fm.HouseId == h.Id)
                .Select(fm => new FamilyMemberReadModel
                {
                    Id = fm.Id,
                    FirstName = fm.FirstName,
                    LastName = fm.LastName,
                    FullName = fm.FullName,
                    ContactNumber = fm.ContactNumber,
                    MemberAssignment = fm.MemberAssignment,
                    HouseId = h.Id
                })
                .ToList()
        })
            .ToList();

        return houses;
    }

    public async Task<House?> GetByIdAsync(Guid? houseId, CancellationToken cancellationToken)
    {
        var house = await _context.Houses.FirstOrDefaultAsync(h => h.Id == houseId, cancellationToken);

        if (house is null)
        {
            return null;
        }

        return house;
    }

    public async Task<HouseReadModel?> GetByIdQueryAsync(Guid houseId, CancellationToken cancellationToken)
    {
        var query = await _context.Houses.FirstOrDefaultAsync(h => h.Id == houseId, cancellationToken);

        if (query is null)
        {
            return null;
        }

        var familyMembers = await _context.FamilyMembers
            .AsNoTracking()
            .AsQueryable()
            .ToListAsync(cancellationToken);

        var house = new HouseReadModel
        {
            Id = query.Id,
            Name = query.Name,
            Address = query.Address,
            HasOwner = query.HasOwner,
            FamilyMembers = familyMembers
                .Where(fm => fm.HouseId == query.Id)
                .Select(fm => new FamilyMemberReadModel
                {
                    Id = fm.Id,
                    FirstName = fm.FirstName,
                    LastName = fm.LastName,
                    FullName = fm.FullName,
                    ContactNumber = fm.ContactNumber,
                    MemberAssignment = fm.MemberAssignment,
                    HouseId = query.Id
                })
                .ToList()
        };

        return house;
    }

    public async Task<HouseholdReportReadModel> GetHouseholdReportAsync(Guid ownerId, CancellationToken cancellationToken)
    {
        var houseReport = await _context.HouseholdReports
            .FromSql($"EXEC HouseholdReport {ownerId}")
            .ToListAsync(cancellationToken);

        var hr = houseReport.FirstOrDefault();

        var report = new HouseholdReportReadModel
        {
            HouseAddress = hr.HouseAddress,
            OwnerName = hr.OwnerName,
            ContactNumber = hr.ContactNumber,
            NumberOfDependents = hr.NumberOfDependents
        };

        return report;
    }

    public async Task UpdateAsync(House house, CancellationToken cancellationToken)
    {
        _context.Houses.Update(house);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
