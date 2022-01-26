using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorApp.Migrations
{
    public partial class Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Packages",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CodPackage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Monto = table.Column<float>(type: "real", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Packages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Transfer",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Afiliado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Monto = table.Column<float>(type: "real", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transfer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserData",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    surname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dateOfbirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    city = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    mobile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    wallet = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserData", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Withdrawal",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Wallet = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Monto = table.Column<float>(type: "real", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Withdrawal", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Packages_Id",
                table: "Packages",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Transfer_Id",
                table: "Transfer",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserData_Id",
                table: "UserData",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Withdrawal_Id",
                table: "Withdrawal",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Packages");

            migrationBuilder.DropTable(
                name: "Transfer");

            migrationBuilder.DropTable(
                name: "UserData");

            migrationBuilder.DropTable(
                name: "Withdrawal");
        }
    }
}
