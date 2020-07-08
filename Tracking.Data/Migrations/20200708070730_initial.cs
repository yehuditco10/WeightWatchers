using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tracking.Data.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tracking",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CardId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false, defaultValueSql: "getDate()"),
                    Weight = table.Column<float>(nullable: false),
                    Trand = table.Column<int>(nullable: false),
                    BMI = table.Column<float>(nullable: false, defaultValue: 0f),
                    Comments = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tracking", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tracking");
        }
    }
}
