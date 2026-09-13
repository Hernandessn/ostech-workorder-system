using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OSTech.Domain.Entities;
using OSTech.Domain.ValueObjects;

namespace OSTech.EFCore.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.Property(n => n.Name)
               .HasMaxLength(100)
               .IsRequired();

        builder.Property(n => n.Email)
               .HasMaxLength(100)
               .IsRequired()
               .HasConversion(
                   email => email.Address,
                   value => EmailAddress.Create(value)
               );

        builder.Property(n => n.Phone)
               .HasMaxLength(20)
               .IsRequired();

        builder.Property(n => n.Document)
               .HasMaxLength(20)
               .IsRequired()
               .HasConversion(
                   document => document.Number,
                   value => Document.Create(value)
               );

        builder.HasData(
            new
            {
                CustomerId = 1,
                Name = "Carlos Souza",
                Email = EmailAddress.Create("carlos.souza@email.com"),
                Phone = "11-91234-5678",
                Document = Document.Create("111.444.777-35")
            },
            new
            {
                CustomerId = 2,
                Name = "Fernanda Lima",
                Email = EmailAddress.Create("fernanda.lima@email.com"),
                Phone = "11-99876-5432",
                Document = Document.Create("123.456.789-09")
            },
            new
            {
                CustomerId = 3,
                Name = "Paulo Ribeiro",
                Email = EmailAddress.Create("paulo.ribeiro@email.com"),
                Phone = "11-98888-1111",
                Document = Document.Create("987.654.321-00")
            },
            new
            {
                CustomerId = 4,
                Name = "Juliana Prado",
                Email = EmailAddress.Create("juliana.prado@email.com"),
                Phone = "11-97777-2222",
                Document = Document.Create("529.982.247-25")
            },
            new
            {
                CustomerId = 5,
                Name = "Rafael Nogueira",
                Email = EmailAddress.Create("rafael.nogueira@email.com"),
                Phone = "11-96666-3333",
                Document = Document.Create("111.222.333-96")
            });
    }
}