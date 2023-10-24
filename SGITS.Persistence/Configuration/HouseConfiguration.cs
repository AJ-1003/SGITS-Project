using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SGITS.Core.Entities;

namespace SGITS.Persistence.Configuration;
public class HouseConfiguration : IEntityTypeConfiguration<House>
{
    public void Configure(EntityTypeBuilder<House> builder)
    {
        builder
            .Property(h => h.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder
            .Property(h => h.Address)
            .IsRequired()
            .HasMaxLength(250);

        builder
            .HasMany<FamilyMember>();
    }
}
