using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FindYourWayAPI.Migrations
{
    public partial class CorrectFieldSpellingInCompnayTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_Fields_FiledFieldId",
                table: "Companies");

            migrationBuilder.RenameColumn(
                name: "FiledFieldId",
                table: "Companies",
                newName: "FieldId");

            migrationBuilder.RenameIndex(
                name: "IX_Companies_FiledFieldId",
                table: "Companies",
                newName: "IX_Companies_FieldId");

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_Fields_FieldId",
                table: "Companies",
                column: "FieldId",
                principalTable: "Fields",
                principalColumn: "FieldId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_Fields_FieldId",
                table: "Companies");

            migrationBuilder.RenameColumn(
                name: "FieldId",
                table: "Companies",
                newName: "FiledFieldId");

            migrationBuilder.RenameIndex(
                name: "IX_Companies_FieldId",
                table: "Companies",
                newName: "IX_Companies_FiledFieldId");

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_Fields_FiledFieldId",
                table: "Companies",
                column: "FiledFieldId",
                principalTable: "Fields",
                principalColumn: "FieldId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
