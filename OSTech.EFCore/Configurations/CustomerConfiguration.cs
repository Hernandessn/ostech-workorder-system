using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OSTech.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OSTech.EFCore.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.Property(n => n.Name)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(n => n.Email)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(n => n.Phone)
                   .HasMaxLength(20)
                   .IsRequired();

            builder.Property(n => n.Document)
                   .HasMaxLength(20)
                   .IsRequired();

            builder.HasData(
                new
                {
                    CustomerId = 1,
                    Name = "Tech Solutions LTDA",
                    Email = "contato@techsolutions.com",
                    Phone = "11-2222-1111",
                    Document = "12.345.678/0001-90"
                },
                new
                {
                    CustomerId = 2,
                    Name = "Escola Alpha",
                    Email = "suporte@escolaalpha.com",
                    Phone = "11-3333-2222",
                    Document = "23.456.789/0001-12"
                },
                new
                {
                    CustomerId = 3,
                    Name = "Mercado Bom Preço",
                    Email = "contato@bompreco.com",
                    Phone = "11-4444-3333",
                    Document = "34.567.890/0001-45"
                },
                new
                {
                    CustomerId = 4,
                    Name = "Clínica Vida",
                    Email = "ti@clinicavida.com",
                    Phone = "11-5555-4444",
                    Document = "45.678.901/0001-67"
                },
                new
                {
                    CustomerId = 5,
                    Name = "Loja Digital",
                    Email = "suporte@lojadigital.com",
                    Phone = "11-6666-5555",
                    Document = "56.789.012/0001-89"
                });
        }
    }
}
