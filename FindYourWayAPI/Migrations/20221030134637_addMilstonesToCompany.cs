using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FindYourWayAPI.Migrations
{
    public partial class addMilstonesToCompany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Milestones",
                columns: table => new
                {
                    MilestoneId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MilestoneName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Milestones", x => x.MilestoneId);
                    table.ForeignKey(
                        name: "FK_Milestones_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "CompanyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Milestones_CompanyId",
                table: "Milestones",
                column: "CompanyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Milestones");
        }
    }
}
