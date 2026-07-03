using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OSTech.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OSTech.EFCore.Configurations
{
    public class TechnicianConfiguration : IEntityTypeConfiguration<Technician>
    {
        public void Configure(EntityTypeBuilder<Technician> builder)
        {
            builder.Property(n => n.Name)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(n => n.Specialty)
                   .HasMaxLength(100);

            builder.Property(n => n.Contact)
                   .HasMaxLength(50)
                   .IsRequired();
        }
    }
}
