using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OSTech.EFCore.Migrations
{
    /// <inheritdoc />
    public partial class SeedCustomerDataFixed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 1,
                columns: new[] { "Document", "Email", "Name", "Phone" },
                values: new object[] { "11144477735", "carlos.souza@email.com", "Carlos Souza", "11-91234-5678" });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 2,
                columns: new[] { "Document", "Email", "Name", "Phone" },
                values: new object[] { "12345678909", "fernanda.lima@email.com", "Fernanda Lima", "11-99876-5432" });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 3,
                columns: new[] { "Document", "Email", "Name", "Phone" },
                values: new object[] { "98765432100", "paulo.ribeiro@email.com", "Paulo Ribeiro", "11-98888-1111" });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 4,
                columns: new[] { "Document", "Email", "Name", "Phone" },
                values: new object[] { "52998224725", "juliana.prado@email.com", "Juliana Prado", "11-97777-2222" });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 5,
                columns: new[] { "Document", "Email", "Name", "Phone" },
                values: new object[] { "11122233396", "rafael.nogueira@email.com", "Rafael Nogueira", "11-96666-3333" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 1,
                columns: new[] { "Document", "Email", "Name", "Phone" },
                values: new object[] { "12.345.678/0001-90", "contato@techsolutions.com", "Tech Solutions LTDA", "11-2222-1111" });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 2,
                columns: new[] { "Document", "Email", "Name", "Phone" },
                values: new object[] { "23.456.789/0001-12", "suporte@escolaalpha.com", "Escola Alpha", "11-3333-2222" });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 3,
                columns: new[] { "Document", "Email", "Name", "Phone" },
                values: new object[] { "34.567.890/0001-45", "contato@bompreco.com", "Mercado Bom Preço", "11-4444-3333" });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 4,
                columns: new[] { "Document", "Email", "Name", "Phone" },
                values: new object[] { "45.678.901/0001-67", "ti@clinicavida.com", "Clínica Vida", "11-5555-4444" });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 5,
                columns: new[] { "Document", "Email", "Name", "Phone" },
                values: new object[] { "56.789.012/0001-89", "suporte@lojadigital.com", "Loja Digital", "11-6666-5555" });
        }
    }
}
