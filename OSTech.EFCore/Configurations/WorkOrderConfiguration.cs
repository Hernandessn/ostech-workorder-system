using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OSTech.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OSTech.EFCore.Configurations
{
    public class WorkOrderConfiguration : IEntityTypeConfiguration<WorkOrder>
    {
        public void Configure(EntityTypeBuilder<WorkOrder> builder)
        {
            builder.Property(n => n.Client)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(n => n.Description)
                   .HasMaxLength(250)
                   .IsRequired();

            builder.Property(n => n.Amount)
                   .HasPrecision(10, 2);
        }
    }
}
