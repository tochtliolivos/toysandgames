using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Unosquare.ToysAndGames.DBService.Migrations
{
    public partial class createTablesAndData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AgeRestriction = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Mattel" });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "Hasbro" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AgeRestriction", "CompanyId", "Description", "Name", "Price" },
                values: new object[] { 1, 12, 1, "lorem ipsum dolor", "Barbie Developer", 300.00m });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AgeRestriction", "CompanyId", "Description", "Name", "Price" },
                values: new object[] { 2, 15, 1, "Pista con dos coches etc", "Pista Hotweels", 650.00m });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AgeRestriction", "CompanyId", "Description", "Name", "Price" },
                values: new object[] { 3, 99, 2, "Juego de mesa para dos personas", "Adivina quien", 250.00m });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CompanyId",
                table: "Products",
                column: "CompanyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Companies");
        }
    }
}
