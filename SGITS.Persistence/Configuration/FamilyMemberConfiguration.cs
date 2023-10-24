using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SGITS.Core.Entities;

namespace SGITS.Persistence.Configuration;
public class FamilyMemberConfiguration : IEntityTypeConfiguration<FamilyMember>
{
    public void Configure(EntityTypeBuilder<FamilyMember> builder)
    {
        builder
            .Property(fm => fm.FirstName)
            .IsRequired()
            .HasMaxLength(50);

        builder
            .Property(fm => fm.LastName)
            .IsRequired()
            .HasMaxLength(50);

        builder
            .Property(fm => fm.ContactNumber)
            .IsRequired();

        builder
            .Property(fm => fm.MemberAssignment)
            .IsRequired();
    }
}
