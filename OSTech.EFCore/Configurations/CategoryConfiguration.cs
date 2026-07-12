using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OSTech.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OSTech.EFCore.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(n => n.Name)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(n => n.Description)
                   .HasMaxLength(500)
                   .IsRequired();

            builder.HasData(
                new
                {
                    CategoryId = 1,
                    Name = "Infraestrutura",
                    Description = "Serviços relacionados à infraestrutura de TI."
                },
                new
                {
                    CategoryId = 2,
                    Name = "Redes",
                    Description = "Serviços de redes e conectividade."
                },
                new
                {
                    CategoryId = 3,
                    Name = "Hardware",
                    Description = "Manutenção e substituição de componentes."
                },
                new
                {
                    CategoryId = 4,
                    Name = "Segurança",
                    Description = "Firewall, antivírus e segurança da informação."
                },
                new
                {
                    CategoryId = 5,
                    Name = "Backup",
                    Description = "Rotinas de backup e recuperação."
                });
        }
    }
}
