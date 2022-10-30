using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FindYourWayAPI.Migrations
{
    public partial class AddCompanyTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    CompanyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompnayName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumberOfEmployees = table.Column<int>(type: "int", nullable: false),
                    FiledFieldId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.CompanyId);
                    table.ForeignKey(
                        name: "FK_Companies_Fields_FiledFieldId",
                        column: x => x.FiledFieldId,
                        principalTable: "Fields",
                        principalColumn: "FieldId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Companies_FiledFieldId",
                table: "Companies",
                column: "FiledFieldId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Companies");
        }
    }
}
