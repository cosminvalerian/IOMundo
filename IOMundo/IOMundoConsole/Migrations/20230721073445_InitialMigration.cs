using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IOMundoConsole.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "offers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CheckInDate = table.Column<DateTime>(nullable: false),
                    StayDurationNights = table.Column<long>(nullable: false),
                    PersonCombination = table.Column<string>(nullable: true),
                    ServiceCode = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(nullable: false),
                    PricePerAdult = table.Column<decimal>(nullable: false),
                    PricePerChild = table.Column<decimal>(nullable: false),
                    StrikePrice = table.Column<decimal>(nullable: false),
                    StrikePricePerAdult = table.Column<decimal>(nullable: false),
                    StrikePricePerChild = table.Column<decimal>(nullable: false),
                    ShowStrikePrice = table.Column<bool>(nullable: false),
                    LastUpdated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_offers", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "offers");
        }
    }
}
