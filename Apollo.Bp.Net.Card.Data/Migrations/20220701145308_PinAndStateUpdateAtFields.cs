using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Apollo.Bp.Net.Card.Data.Migrations
{
    public partial class PinAndStateUpdateAtFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "pin_update_at",
                table: "cards",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "state_update_at",
                table: "cards",
                type: "timestamp without time zone",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "pin_update_at",
                table: "cards");

            migrationBuilder.DropColumn(
                name: "state_update_at",
                table: "cards");
        }
    }
}
