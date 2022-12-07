using Microsoft.EntityFrameworkCore.Migrations;

namespace Apollo.Bp.Net.Card.Data.Migrations
{
    public partial class AddReasonCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "reason_code",
                table: "cards",
                type: "character varying(2)",
                maxLength: 2,
                nullable: false,
                defaultValue: string.Empty);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "reason_code",
                table: "cards");
        }
    }
}
