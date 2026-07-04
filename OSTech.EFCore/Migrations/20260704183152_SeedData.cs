using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OSTech.EFCore.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Technicians",
                columns: new[] { "TechnicianId", "Availability", "Contact", "HiringDate", "Name", "Specialty" },
                values: new object[,]
                {
                    { 1, true, "11-2222-2222", new DateOnly(2023, 2, 11), "Marcelo Silva", "Engenharia de Software" },
                    { 2, true, "11-9999-1111", new DateOnly(2022, 6, 5), "Ana Costa", "Redes de Computadores" },
                    { 3, false, "11-8888-3333", new DateOnly(2021, 9, 20), "Bruno Almeida", "Segurança da Informação" },
                    { 4, true, "11-7777-4444", new DateOnly(2024, 1, 10), "Juliana Ferreira", "Desenvolvimento Web" },
                    { 5, true, "11-6666-5555", new DateOnly(2020, 3, 15), "Ricardo Mendes", "DevOps" }
                });

            migrationBuilder.InsertData(
                table: "WorkOrders",
                columns: new[] { "WorkOrderId", "Amount", "Client", "Deadline", "Description", "OpeningDate", "Status", "TechnicianId", "Title" },
                values: new object[,]
                {
                    { 1, 3500.00m, "Tech Solutions LTDA", new DateOnly(2026, 7, 10), "Instalar e configurar servidor Linux para ambiente corporativo.", new DateOnly(2026, 7, 1), 0, 1, "Instalação de Servidor" },
                    { 2, 980.00m, "Escola Alpha", new DateOnly(2026, 7, 8), "Resolver problemas de conectividade entre os laboratórios.", new DateOnly(2026, 7, 2), 1, 2, "Manutenção da Rede" },
                    { 3, 1450.00m, "Mercado Bom Preço", new DateOnly(2026, 6, 30), "Substituir HD por SSD em cinco computadores.", new DateOnly(2026, 6, 25), 2, 1, "Troca de SSD" },
                    { 4, 2200.00m, "Clínica Vida", new DateOnly(2026, 7, 15), "Criar regras de segurança para acesso remoto.", new DateOnly(2026, 7, 3), 0, 4, "Configuração de Firewall" },
                    { 5, 1800.00m, "Loja Digital", new DateOnly(2026, 7, 5), "Implantar rotina automática de backup diário.", new DateOnly(2026, 6, 28), 3, 3, "Backup Corporativo" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Technicians",
                keyColumn: "TechnicianId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "WorkOrders",
                keyColumn: "WorkOrderId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "WorkOrders",
                keyColumn: "WorkOrderId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "WorkOrders",
                keyColumn: "WorkOrderId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "WorkOrders",
                keyColumn: "WorkOrderId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "WorkOrders",
                keyColumn: "WorkOrderId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Technicians",
                keyColumn: "TechnicianId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Technicians",
                keyColumn: "TechnicianId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Technicians",
                keyColumn: "TechnicianId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Technicians",
                keyColumn: "TechnicianId",
                keyValue: 4);
        }
    }
}
