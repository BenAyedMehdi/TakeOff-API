using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FindYourWayAPI.Migrations
{
    public partial class addContactToCompany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ContactId",
                table: "Companies",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    ContactId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Website = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OwnerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.ContactId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Companies_ContactId",
                table: "Companies",
                column: "ContactId");

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_Contacts_ContactId",
                table: "Companies",
                column: "ContactId",
                principalTable: "Contacts",
                principalColumn: "ContactId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_Contacts_ContactId",
                table: "Companies");

            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropIndex(
                name: "IX_Companies_ContactId",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "ContactId",
                table: "Companies");
        }
    }
}
