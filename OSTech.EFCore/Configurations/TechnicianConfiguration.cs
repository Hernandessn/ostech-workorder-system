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

            builder.HasData(
            new Technician
            {
                TechnicianId = 1,
                Name = "Marcelão Alterado",
                Specialty = "Engenharia de Software",
                Contact = "11-2222-2222",
                Availability = true,
                HiringDate = DateOnly.Parse("2023-02-11")
            },
            new Technician
            {
                TechnicianId = 2,
                Name = "Ana Costa",
                Specialty = "Redes de Computadores",
                Contact = "11-9999-1111",
                Availability = true,
                HiringDate = DateOnly.Parse("2022-06-05")
            },
            new Technician
            {
                TechnicianId = 3,
                Name = "Bruno Almeida",
                Specialty = "Segurança da Informação",
                Contact = "11-8888-3333",
                Availability = false,
                HiringDate = DateOnly.Parse("2021-09-20")
            },
            new Technician
            {
                TechnicianId = 4,
                Name = "Juliana Ferreira",
                Specialty = "Desenvolvimento Web",
                Contact = "11-7777-4444",
                Availability = true,
                HiringDate = DateOnly.Parse("2024-01-10")
            },
            new Technician
            {
                TechnicianId = 5,
                Name = "Ricardo Mendes",
                Specialty = "DevOps",
                Contact = "11-6666-5555",
                Availability = true,
                HiringDate = DateOnly.Parse("2020-03-15")
            });
        }
    }
}
