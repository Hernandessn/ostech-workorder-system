using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OSTech.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OSTech.EFCore.Configurations
{
    public class EquipmentConfiguration : IEntityTypeConfiguration<Equipment>
    {
        public void Configure(EntityTypeBuilder<Equipment> builder)
        {
            builder.Property(n => n.Name)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(n => n.Brand)
                   .HasMaxLength(200)
                   .IsRequired();

            builder.Property(n => n.Model)
                   .HasMaxLength(200)
                   .IsRequired();

            builder.Property(n => n.SerialNumber)
                   .HasMaxLength(50)
                   .IsRequired();

            builder.HasData(
                new
                {
                    EquipmentId = 1,
                    Name = "Servidor Dell",
                    Brand = "Dell",
                    Model = "PowerEdge R650",
                    SerialNumber = "SRV001"
                },
                new
                {
                    EquipmentId = 2,
                    Name = "Switch Cisco",
                    Brand = "Cisco",
                    Model = "Catalyst 2960",
                    SerialNumber = "SW002"
                },
                new
                {
                    EquipmentId = 3,
                    Name = "Notebook Lenovo",
                    Brand = "Lenovo",
                    Model = "ThinkPad E14",
                    SerialNumber = "NT003"
                },
                new
                {
                    EquipmentId = 4,
                    Name = "Firewall Fortinet",
                    Brand = "Fortinet",
                    Model = "FortiGate 60F",
                    SerialNumber = "FW004"
                },
                new
                {
                    EquipmentId = 5,
                    Name = "Servidor Backup",
                    Brand = "HP",
                    Model = "ProLiant DL380",
                    SerialNumber = "BK005"
                });
        }
    }
}
