using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FindYourWayAPI.Migrations
{
    public partial class AddPackageToCompany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PackageId",
                table: "Companies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Companies_PackageId",
                table: "Companies",
                column: "PackageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_Packages_PackageId",
                table: "Companies",
                column: "PackageId",
                principalTable: "Packages",
                principalColumn: "PackageId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_Packages_PackageId",
                table: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_Companies_PackageId",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "PackageId",
                table: "Companies");
        }
    }
}
