using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Apollo.Bp.Net.Card.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cards",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    token = table.Column<Guid>(type: "uuid", nullable: false),
                    party_id = table.Column<Guid>(type: "uuid", nullable: false),
                    card_product_token = table.Column<Guid>(type: "uuid", nullable: false),
                    account_id = table.Column<string>(type: "text", nullable: false),
                    pan = table.Column<string>(type: "text", nullable: false),
                    last_four = table.Column<string>(type: "text", nullable: false),
                    expiration = table.Column<string>(type: "text", nullable: false),
                    expiration_time = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    barcode = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    pin_is_set = table.Column<bool>(type: "boolean", nullable: false),
                    state = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    state_reason = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    fulfillment_status = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    instrument_type = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    shipping_method = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    shipping_care_of_line = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    return_address1 = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    return_address2 = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    return_city = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    return_state = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    return_postal_code = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    return_country = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    return_phone = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    return_first_name = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    return_middle_name = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    return_last_name = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    recipient_address1 = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    recipient_address2 = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    recipient_city = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    recipient_state = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    recipient_postal_code = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    recipient_country = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    recipient_phone = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    recipient_first_name = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    recipient_middle_name = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    recipient_last_name = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    perso_type = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: true),
                    text_name_line1 = table.Column<string>(type: "character varying(21)", maxLength: 21, nullable: true),
                    text_name_line2 = table.Column<string>(type: "character varying(21)", maxLength: 21, nullable: true),
                    text_name_line3 = table.Column<string>(type: "character varying(21)", maxLength: 21, nullable: true),
                    template_id = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: true),
                    logo_file = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: true),
                    logo_thumbnail_file = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: true),
                    message_file = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: true),
                    message_line = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: true),
                    images_card_name = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    images_card_thermal_color = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    images_card_signature = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    carrier_return_window_name = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    metadata = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    expedite = table.Column<bool>(type: "boolean", nullable: false),
                    create_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    update_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cards", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cards");
        }
    }
}
