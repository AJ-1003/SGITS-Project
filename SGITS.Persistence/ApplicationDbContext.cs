using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SGITS.App.CQRS.House.Get;
using SGITS.Core.Entities;

namespace SGITS.Persistence;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<FamilyMember> FamilyMembers { get; set; }
    public DbSet<House> Houses { get; set; }
    public DbSet<HouseholdReport> HouseholdReports { get; set; }
}
