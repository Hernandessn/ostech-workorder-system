using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OSTech.EFCore.Migrations
{
    /// <inheritdoc />
    public partial class AddCustomerCategoryEquipment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Client",
                table: "WorkOrders");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "WorkOrders",
                type: "varchar(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "WorkOrders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "WorkOrders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EquipmentId",
                table: "WorkOrders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Phone = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Document = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Equipments",
                columns: table => new
                {
                    EquipmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Brand = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Model = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SerialNumber = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipments", x => x.EquipmentId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Serviços relacionados à infraestrutura de TI.", "Infraestrutura" },
                    { 2, "Serviços de redes e conectividade.", "Redes" },
                    { 3, "Manutenção e substituição de componentes.", "Hardware" },
                    { 4, "Firewall, antivírus e segurança da informação.", "Segurança" },
                    { 5, "Rotinas de backup e recuperação.", "Backup" }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "Document", "Email", "Name", "Phone" },
                values: new object[,]
                {
                    { 1, "12.345.678/0001-90", "contato@techsolutions.com", "Tech Solutions LTDA", "11-2222-1111" },
                    { 2, "23.456.789/0001-12", "suporte@escolaalpha.com", "Escola Alpha", "11-3333-2222" },
                    { 3, "34.567.890/0001-45", "contato@bompreco.com", "Mercado Bom Preço", "11-4444-3333" },
                    { 4, "45.678.901/0001-67", "ti@clinicavida.com", "Clínica Vida", "11-5555-4444" },
                    { 5, "56.789.012/0001-89", "suporte@lojadigital.com", "Loja Digital", "11-6666-5555" }
                });

            migrationBuilder.InsertData(
                table: "Equipments",
                columns: new[] { "EquipmentId", "Brand", "Model", "Name", "SerialNumber" },
                values: new object[,]
                {
                    { 1, "Dell", "PowerEdge R650", "Servidor Dell", "SRV001" },
                    { 2, "Cisco", "Catalyst 2960", "Switch Cisco", "SW002" },
                    { 3, "Lenovo", "ThinkPad E14", "Notebook Lenovo", "NT003" },
                    { 4, "Fortinet", "FortiGate 60F", "Firewall Fortinet", "FW004" },
                    { 5, "HP", "ProLiant DL380", "Servidor Backup", "BK005" }
                });

            migrationBuilder.UpdateData(
                table: "WorkOrders",
                keyColumn: "WorkOrderId",
                keyValue: 1,
                columns: new[] { "CategoryId", "CustomerId", "EquipmentId" },
                values: new object[] { 1, 1, 1 });

            migrationBuilder.UpdateData(
                table: "WorkOrders",
                keyColumn: "WorkOrderId",
                keyValue: 2,
                columns: new[] { "CategoryId", "CustomerId", "EquipmentId" },
                values: new object[] { 2, 2, 2 });

            migrationBuilder.UpdateData(
                table: "WorkOrders",
                keyColumn: "WorkOrderId",
                keyValue: 3,
                columns: new[] { "CategoryId", "CustomerId", "EquipmentId" },
                values: new object[] { 3, 3, 3 });

            migrationBuilder.UpdateData(
                table: "WorkOrders",
                keyColumn: "WorkOrderId",
                keyValue: 4,
                columns: new[] { "CategoryId", "CustomerId", "EquipmentId" },
                values: new object[] { 4, 4, 4 });

            migrationBuilder.UpdateData(
                table: "WorkOrders",
                keyColumn: "WorkOrderId",
                keyValue: 5,
                columns: new[] { "CategoryId", "CustomerId", "EquipmentId" },
                values: new object[] { 5, 5, 5 });

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrders_CategoryId",
                table: "WorkOrders",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrders_CustomerId",
                table: "WorkOrders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrders_EquipmentId",
                table: "WorkOrders",
                column: "EquipmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkOrders_Categories_CategoryId",
                table: "WorkOrders",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkOrders_Customers_CustomerId",
                table: "WorkOrders",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkOrders_Equipments_EquipmentId",
                table: "WorkOrders",
                column: "EquipmentId",
                principalTable: "Equipments",
                principalColumn: "EquipmentId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkOrders_Categories_CategoryId",
                table: "WorkOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkOrders_Customers_CustomerId",
                table: "WorkOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkOrders_Equipments_EquipmentId",
                table: "WorkOrders");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Equipments");

            migrationBuilder.DropIndex(
                name: "IX_WorkOrders_CategoryId",
                table: "WorkOrders");

            migrationBuilder.DropIndex(
                name: "IX_WorkOrders_CustomerId",
                table: "WorkOrders");

            migrationBuilder.DropIndex(
                name: "IX_WorkOrders_EquipmentId",
                table: "WorkOrders");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "WorkOrders");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "WorkOrders");

            migrationBuilder.DropColumn(
                name: "EquipmentId",
                table: "WorkOrders");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "WorkOrders",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(150)",
                oldMaxLength: 150)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Client",
                table: "WorkOrders",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "WorkOrders",
                keyColumn: "WorkOrderId",
                keyValue: 1,
                column: "Client",
                value: "Tech Solutions LTDA");

            migrationBuilder.UpdateData(
                table: "WorkOrders",
                keyColumn: "WorkOrderId",
                keyValue: 2,
                column: "Client",
                value: "Escola Alpha");

            migrationBuilder.UpdateData(
                table: "WorkOrders",
                keyColumn: "WorkOrderId",
                keyValue: 3,
                column: "Client",
                value: "Mercado Bom Preço");

            migrationBuilder.UpdateData(
                table: "WorkOrders",
                keyColumn: "WorkOrderId",
                keyValue: 4,
                column: "Client",
                value: "Clínica Vida");

            migrationBuilder.UpdateData(
                table: "WorkOrders",
                keyColumn: "WorkOrderId",
                keyValue: 5,
                column: "Client",
                value: "Loja Digital");
        }
    }
}
