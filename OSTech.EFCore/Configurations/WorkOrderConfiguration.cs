using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OSTech.Domain.Entities;
using OSTech.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace OSTech.EFCore.Configurations
{
    public class WorkOrderConfiguration : IEntityTypeConfiguration<WorkOrder>
    {
        public void Configure(EntityTypeBuilder<WorkOrder> builder)
        {
            builder.HasOne(n => n.Technician)
                   .WithMany(n => n.WorkOrders)
                   .HasForeignKey(n => n.TechnicianId);

            builder.Property(n => n.Client)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(n => n.Description)
                   .HasMaxLength(250)
                   .IsRequired();

            builder.Property(n => n.Amount)
                   .HasPrecision(10, 2);

            builder.HasData(
                new WorkOrder
                {
                    WorkOrderId = 1,
                    Client = "Tech Solutions LTDA",
                    Title = "Instalação de Servidor",
                    Description = "Instalar e configurar servidor Linux para ambiente corporativo.",
                    Amount = 3500.00m,
                    OpeningDate = DateOnly.Parse("2026-07-01"),
                    Deadline = DateOnly.Parse("2026-07-10"),
                    Status = StatusWorkOrder.Open,
                    TechnicianId = 1
                },
                new WorkOrder
                {
                    WorkOrderId = 2,
                    Client = "Escola Alpha",
                    Title = "Manutenção da Rede",
                    Description = "Resolver problemas de conectividade entre os laboratórios.",
                    Amount = 980.00m,
                    OpeningDate = DateOnly.Parse("2026-07-02"),
                    Deadline = DateOnly.Parse("2026-07-08"),
                    Status = StatusWorkOrder.InProgress,
                    TechnicianId = 2
                },
                new WorkOrder
                {
                    WorkOrderId = 3,
                    Client = "Mercado Bom Preço",
                    Title = "Troca de SSD",
                    Description = "Substituir HD por SSD em cinco computadores.",
                    Amount = 1450.00m,
                    OpeningDate = DateOnly.Parse("2026-06-25"),
                    Deadline = DateOnly.Parse("2026-06-30"),
                    Status = StatusWorkOrder.Completed,
                    TechnicianId = 1
                },
            new WorkOrder
            {
                WorkOrderId = 4,
                Client = "Clínica Vida",
                Title = "Configuração de Firewall",
                Description = "Criar regras de segurança para acesso remoto.",
                Amount = 2200.00m,
                OpeningDate = DateOnly.Parse("2026-07-03"),
                Deadline = DateOnly.Parse("2026-07-15"),
                Status = StatusWorkOrder.Open,
                TechnicianId = 4
            },
            new WorkOrder
            {
                WorkOrderId = 5,
                Client = "Loja Digital",
                Title = "Backup Corporativo",
                Description = "Implantar rotina automática de backup diário.",
                Amount = 1800.00m,
                OpeningDate = DateOnly.Parse("2026-06-28"),
                Deadline = DateOnly.Parse("2026-07-05"),
                Status = StatusWorkOrder.Canceled,
                TechnicianId = 3
            });
        }
    }
}
