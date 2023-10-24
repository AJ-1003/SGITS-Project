using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SGITS.Core.Entities;

namespace SGITS.Persistence.Configuration;
public class HouseholdReportConfiguration : IEntityTypeConfiguration<HouseholdReport>
{
    public void Configure(EntityTypeBuilder<HouseholdReport> builder)
    {
        builder.HasNoKey();

        builder.ToTable(nameof(HouseholdReport), t => t.ExcludeFromMigrations());
    }
}
