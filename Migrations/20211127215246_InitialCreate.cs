using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GardenService.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Moistures",
                columns: table => new
                {
                    IX_Moisture = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Value = table.Column<float>(type: "REAL", nullable: false),
                    EnteredDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Moistures", x => x.IX_Moisture);
                });

            migrationBuilder.InsertData(
                table: "Moistures",
                columns: new[] { "IX_Moisture", "EnteredDate", "Value" },
                values: new object[] { 1, new DateTime(2021, 11, 27, 15, 52, 46, 424, DateTimeKind.Local).AddTicks(7247), 1.4f });

            migrationBuilder.InsertData(
                table: "Moistures",
                columns: new[] { "IX_Moisture", "EnteredDate", "Value" },
                values: new object[] { 2, new DateTime(2021, 11, 27, 15, 52, 46, 424, DateTimeKind.Local).AddTicks(7288), 1.5f });

            migrationBuilder.InsertData(
                table: "Moistures",
                columns: new[] { "IX_Moisture", "EnteredDate", "Value" },
                values: new object[] { 3, new DateTime(2021, 11, 27, 15, 52, 46, 424, DateTimeKind.Local).AddTicks(7292), 1.6f });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Moistures");
        }
    }
}
